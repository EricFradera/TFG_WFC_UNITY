using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Linq;
using WFC;

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
        config.wfcTilesList.ForEach(CreateNodeView);

        config.wfcTilesList.ForEach(tile =>
        {
            var parentComponent = FindNodeComponent(tile);
            foreach (var relation in tile.nodeData.relationShips)
            {
                var childComponent = FindNodeComponent(relation.inputTile);
                Edge edge = parentComponent.output[relation.indexOutput]
                    .ConnectTo(childComponent.input[relation.indexInput]);
                AddElement(edge);
            }
        });
    }

    private NodeComponent FindNodeComponent(WFCTile tile)
    {
        return GetNodeByGuid(tile.tileId) as NodeComponent;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
    {
        graphviewchange.elementsToRemove?.ForEach(el =>
        {
            switch (el)
            {
                case NodeComponent nodeComponent:
                    config.DeleteNodeTile(nodeComponent.tile);
                    break;
                case Edge edge:
                {
                    if (edge.input.node is NodeComponent inputNode && edge.output.node is NodeComponent outputNode)
                        config.RemoveChild(outputNode.tile, inputNode.tile, dirHelper(edge.output.portName),
                            dirHelper(edge.input.portName));
                    break;
                }
            }
        });

        graphviewchange.edgesToCreate?.ForEach(edge =>
        {
            if (edge.input.node is NodeComponent inputNode && edge.output.node is NodeComponent outputNode)
                config.AddChild(outputNode.tile, inputNode.tile, dirHelper(edge.output.portName),
                    dirHelper(edge.input.portName));
        });

        return graphviewchange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        {
            //This will makes sense once we change how the Tile object works. It needs an abstraction
            evt.menu.AppendAction("Create WFC2DTile", (a) => CreateNode());
            /*var types = TypeCache.GetTypesDerivedFrom<WFC2DTile>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}]{type.Name}", (a) => CreateNode(type));
                Debug.Log("Do we reach here????");
            }*/
        }
    }

    private void CreateNode()
    {
        WFCTile node = config.CreateNodeTile();
        CreateNodeView(node);
    }

    void CreateNodeView(WFCTile tile)
    {
        if (tile is WFC2DTile)
        {
            var nodeComponent = new Node2dComponent(tile as WFC2DTile);
            nodeComponent.OnNodeSelection = onNodeSelected;
            AddElement(nodeComponent);
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