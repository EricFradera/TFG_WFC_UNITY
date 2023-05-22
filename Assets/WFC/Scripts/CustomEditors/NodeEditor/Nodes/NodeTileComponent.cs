using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = System.Numerics.Vector2;

public class NodeTileComponent : NodeComponent
{
    public WFCTile tile;

    protected NodeTileComponent(WFCTile tile)
    {
        this.tile = tile;
        this.viewDataKey = tile.tileId;
        this.title = tile.tileName;
        input = new Port[tile.dim];
        output = new Port[tile.dim];
        style.left = tile.nodeData.position.X;
        style.top = tile.nodeData.position.Y;
        Label titleLabel = this.Q<Label>("title-label");
        titleLabel.bindingPath = "tileName";
        titleLabel.Bind(new SerializedObject(tile));
    }

    protected override void setNodePos(float x, float y) => this.tile.nodeData.position = new Vector2(x, y);
}