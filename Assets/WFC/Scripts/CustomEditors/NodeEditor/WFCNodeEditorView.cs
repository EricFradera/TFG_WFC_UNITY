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
    private EditorManager manager;

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
        createEditorManager();
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
                    case WFCTile relationInputTile:
                        var childTileComponent = FindNodeComponent(relationInputTile);
                        Edge edgeTile = parentComponent.output[relation.indexOutput]
                            .ConnectTo(childTileComponent.input[relation.indexInput]);
                        AddElement(edgeTile);
                        break;
                    case InputCodeData relationInputTile:
                        var childHelperComponent = FindHelperNode(relationInputTile);
                        Edge edgeHelper = parentComponent.output[relation.indexOutput]
                            .ConnectTo(childHelperComponent.input[0]);
                        AddElement(edgeHelper);
                        break;
                }
            }
        });
    }

    private void createEditorManager()
    {
        switch (config)
        {
            case WFC1DConfig config1D:
                manager = new Editor1DManager(config1D);
                break;
            case WFC2DConfig config2D:
                manager = new Editor2DManager(config2D);
                break;
            case WFC3DConfig config3D:
                manager = new Editor3DManager(config3D);
                break;
            case WFCHexConfig configHex:
                manager = new EditorHexManager(configHex);
                break;
            
        }
    }

    private NodeComponent FindNodeComponent(WFCTile tile)
    {
        return GetNodeByGuid(tile.tileId) as NodeComponent;
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
        graphviewchange.elementsToRemove?.ForEach(el => { manager.ElementsToRemove(el); });

        graphviewchange.edgesToCreate?.ForEach(edge => { manager.EdgesToCreate(edge); });

        return graphviewchange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        evt.menu.AppendAction("Create node", (a) => CreateNode());
        evt.menu.AppendAction("Create code Node", (a) => CreateNodeHelper(typeof(StringCodeData)));
    }

    private void CreateNode() => CreateNodeView(config.CreateNodeTile());
    private void CreateNodeHelper(Type type) => CreateNodeView(config.CreateNodeHelper(type));


    private void CreateNodeView(Object obj)
    {
        var nodeComponent = manager.createNodeView(obj);
        if (nodeComponent is null)
        {
            throw new Exception("The node creation resulted in a null node");
        }

        nodeComponent.OnNodeSelection = onNodeSelected;
        AddElement(nodeComponent);
    }
}