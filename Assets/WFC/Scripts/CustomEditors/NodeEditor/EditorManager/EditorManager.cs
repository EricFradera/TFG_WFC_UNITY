using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using WFC;
using Object = System.Object;

public abstract class EditorManager
{
    protected WFCConfig config;

    public void ElementsToRemove(GraphElement el)
    {
        switch (el)
        {
            case NodeTileComponent nodeComponent:
                config.DeleteNodeTile(nodeComponent.tile);
                break;
            case StringCodeNode colorNode:
                config.DeleteNodeHelper(colorNode.codeData);
                break;
            case Edge edge:
            {
                switch (edge.input.node)
                {
                    case NodeTileComponent inputNodeComponent:
                        if (edge.output.node is NodeTileComponent outputNode)
                            config.RemoveChild(outputNode.tile, inputNodeComponent.tile,
                                dirHelper(edge.output.portName),
                                dirHelper(edge.input.portName));
                        break;
                    case StringCodeNode inputStringCode:
                        if (edge.output.node is NodeTileComponent output2DNode)
                        {
                            config.removeHelper(inputStringCode.codeData, output2DNode.tile,
                                dirHelper(edge.output.portName));
                        }

                        break;
                }

                break;
            }
        }
    }

    public void EdgesToCreate(Edge edge)
    {
        switch (edge.input.node)
        {
            case NodeTileComponent inputNode when edge.output.node is NodeTileComponent outputNode:
                config.AddChild(outputNode.tile, inputNode.tile, dirHelper(edge.output.portName),
                    dirHelper(edge.input.portName));
                break;
            case StringCodeNode inputNode when edge.output.node is NodeTileComponent outputNode:
                config.AddHelper(inputNode.codeData, outputNode.tile, dirHelper(edge.output.portName));
                break;
        }
    }

    public abstract NodeComponent createNodeView(Object obj);

    protected abstract int dirHelper(string dirName);
}