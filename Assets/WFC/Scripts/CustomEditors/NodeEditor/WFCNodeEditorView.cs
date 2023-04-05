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
            for (int i = 0; i < 4; i++)
            {
                var child = tile.nodeData.outputConnections[i];
                child.ForEach(c =>
                {
                    try
                    {
                        NodeComponent parentComponent = FindNodeComponent(tile);
                        if (child.Contains(c))
                        {
                            foreach (var index in getNodeInput(c, tile))
                            {
                                NodeComponent childComponent = FindNodeComponent(c);
                                Edge edge = parentComponent.output[i].ConnectTo(childComponent.input[index]);
                                AddElement(edge);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log("not found :(((");
                    }
                });
            }
        });
    }

    private NodeComponent FindNodeComponent(WFCTile tile)
    {
        return GetNodeByGuid(tile.tileId) as NodeComponent;
    }

    private List<int> getNodeInput(WFCTile origin, WFCTile dest)
    {
        List<int> components = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if (origin.nodeData.inputConnections[i].Contains(dest))
            {
                components.Add(i);
            }
        }

        return components;
    }


    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList()
            .Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
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
            evt.menu.AppendAction("Create WFC2DTyle", (a) => CreateNode());
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
        var nodeComponent = new NodeComponent(tile);
        nodeComponent.OnNodeSelection = onNodeSelected;
        AddElement(nodeComponent);
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