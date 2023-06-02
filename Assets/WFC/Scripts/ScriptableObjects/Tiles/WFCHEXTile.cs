using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class WFCHEXTile : WFCTile
{
    [JsonIgnore] public Texture2D tileTexture;
    public WFCHEXTile()
    {
        dim = 6;
        adjacencyCodes = new InputCodeData[dim];
        adjacencyPairs = new List<WFCTile>[dim];
        GeneratedAdjacencyPairs = new List<WFCTile>[dim];
    }

    private enum IndexDirection
    {
        UP,
        RIGHTUP,
        RIGHTDOWN,
        DOWN,
        LEFTDOWN,
        LEFTUP
    }

    public override int GetInverse(int indexDirection)
    {
        return (IndexDirection)indexDirection switch
        {
            IndexDirection.UP => (int)IndexDirection.DOWN,
            IndexDirection.RIGHTUP => (int)IndexDirection.LEFTDOWN,
            IndexDirection.RIGHTDOWN => (int)IndexDirection.LEFTUP,
            IndexDirection.DOWN => (int)IndexDirection.UP,
            IndexDirection.LEFTDOWN => (int)IndexDirection.RIGHTUP,
            IndexDirection.LEFTUP => (int)IndexDirection.RIGHTDOWN,
            _ => -1
        };
    }


    public override List<WFCTile> getRotationTiles()
    {
        throw new Exception("This not currently supported");
    }

    protected override WFCTile copyForRotation( int rot)
    {
        throw new Exception("This not currently supported");
    }
}