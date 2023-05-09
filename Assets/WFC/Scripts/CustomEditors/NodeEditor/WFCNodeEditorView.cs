using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Linq;
using WFC;
using Object = UnityEngine.Object;

public class WFCNodeEditorView : GraphView
{
    public new class UxmlFactory : UxmlFactory<WFCNodeEditorView, GraphView.UxmlTraits>
    {
    }

    public Action<NodeComponent> onNodeSelected;
    private WFCConfig config;

    public WFCNodeEditorView()
    {
        Insert(0, new GridBackground());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        var styleSheet =
            AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/WFC/Scripts/CustomEditors/NodeEditor/WFCNodeEditor.uss");
        styleSheets.Add(styleSheet);
    }


    public void PopulateView(WFCConfig config)
    {
        this.config = config;
        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;
        this.config.wfcTilesList.ForEach(CreateNodeView);
        this.config.nodeHelpers.ForEach(CreateNodeView);

        config.wfcTilesList.ForEach(tile =>
        {
            var parentComponent = FindNodeComponent(tile);
            foreach (var relation in tile.nodeData.relationShips)
            {
                switch (relation.inputTile)
                {
                    case WFC2DTile relationInputTile:
                        var childTileComponent = FindNodeComponent(relationInputTile);
                        Edge edgeTile = parentComponent.output[relation.indexOutput]
                            .ConnectTo(childTileComponent.input[relation.indexInput]);
                        AddElement(edgeTile);
                        break;
                    case InputCodeData relationInputTile:
                        var childHelperComponent = FindHelperNode(relationInputTile);
                        Edge edgeHelper = parentComponent.output[relation.indexOutput] //problems with edges
                            .ConnectTo(childHelperComponent.input[0]);
                        AddElement(edgeHelper);
                        break;
                }
            }
        });
    }

    private NodeComponent FindNodeComponent(WFCTile tile)
    {
        return GetNodeByGuid(tile.tileId) as Node2dComponent;
    }

    private NodeComponent FindHelperNode(InputCodeData codeData)
    {
        return GetNodeByGuid(codeData.uid) as StringCodeNode;
    }

    public override List<Port>
        GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) // Here we have to indicate which ports are allowed.
    {
        return ports.ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
    {
        graphviewchange.elementsToRemove?.ForEach(el =>
        {
            switch (el)
            {
                case Node2dComponent nodeComponent:
                    config.DeleteNodeTile(nodeComponent.tile);
                    break;
                case StringCodeNode colorNode:
                    config.DeleteNodeHelper(colorNode.codeData);
                    break;
                case Edge edge:
                {
                    
                    switch (edge.input.node)
                    {
                        case Node2dComponent inputNode2dComponent:
                            if (edge.output.node is Node2dComponent outputNode)
                                config.RemoveChild(outputNode.tile, inputNode2dComponent.tile,
                                    dirHelper(edge.output.portName),
                                    dirHelper(edge.input.portName));
                            break;
                        case StringCodeNode inputStringCode:
                            if (edge.output.node is Node2dComponent output2DNode)
                            {
                                config.removeHelper(inputStringCode.codeData, output2DNode.tile,
                                    dirHelper(edge.output.portName));
                            }

                            break;
                    }

                    break;

                    /*OLD VER
                     if (edge.input.node is Node2dComponent inputNode && edge.output.node is Node2dComponent outputNode)
                        config.RemoveChild(outputNode.tile, inputNode.tile, dirHelper(edge.output.portName),
                            dirHelper(edge.input.portName));
                    if (edge.input.node is StringCodeNode inputNode && edge.output.node is Node2dComponent outputNode)
                        break;*/
                }
            }
        });

        graphviewchange.edgesToCreate?.ForEach(edge =>
        {
            switch (edge.input.node)
            {
                case Node2dComponent inputNode when edge.output.node is Node2dComponent outputNode:
                    config.AddChild(outputNode.tile, inputNode.tile, dirHelper(edge.output.portName),
                        dirHelper(edge.input.portName));
                    break;
                case StringCodeNode inputNode when edge.output.node is Node2dComponent outputNode:
                    config.AddHelper(inputNode.codeData, outputNode.tile, dirHelper(edge.output.portName));
                    break;
            }
        });

        return graphviewchange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        {
            //This will makes sense once we change how the Tile object works. It needs an abstraction
            evt.menu.AppendAction("Create WFC2DTile", (a) => CreateNode());
            evt.menu.AppendAction("Create Code Node", (a) => CreateNodeHelper(typeof(StringCodeData)));
            /*var types = TypeCache.GetTypesDerivedFrom<WFC2DTile>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}]{type.Name}", (a) => CreateNode(type));
                Debug.Log("Do we reach here????");
            }*/
        }
    }

    private void CreateNode() => CreateNodeView(config.CreateNodeTile());
    private void CreateNodeHelper(Type type) => CreateNodeView(config.CreateNodeHelper(type));


    private void CreateNodeView(Object obj)
    {
        switch (obj)
        {
            case WFC2DTile wfc2DTile:
            {
                var nodeComponent = new Node2dComponent(wfc2DTile);
                nodeComponent.OnNodeSelection = onNodeSelected;
                AddElement(nodeComponent);
                break;
            }
            case InputCodeData inputCodeData:
            {
                var helperNode = new StringCodeNode(inputCodeData);
                helperNode.OnNodeSelection = onNodeSelected;
                AddElement(helperNode);
                break;
            }
        }
    }

    private int dirHelper(string dirName)
    {
        return dirName switch
        {
            "up" => 0,
            "right" => 1,
            "down" => 2,
            "left" => 3,
            _ => -1
        };
    }
}