using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class NodeTileComponent : NodeComponent
{
    public WFCTile tile;
    protected override void setNodePos(float x, float y) => this.tile.nodeData.position = new Vector2(x, y);

}
