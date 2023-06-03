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
        XPLUS,
        XMINUS,
        YMINUS,
        YPLUS,
        ZPLUS,
        ZMINUS,
    }

    public override int GetInverse(int indexDirection)
    {
        return (IndexDirection)indexDirection switch
        {
            IndexDirection.YMINUS => (int)IndexDirection.YPLUS,
            IndexDirection.XPLUS => (int)IndexDirection.XMINUS,
            IndexDirection.ZMINUS => (int)IndexDirection.ZPLUS,
            IndexDirection.YPLUS => (int)IndexDirection.YMINUS,
            IndexDirection.XMINUS => (int)IndexDirection.XPLUS,
            IndexDirection.ZPLUS => (int)IndexDirection.ZMINUS,
            _ => -1
        };
    }


    public override List<WFCTile> getRotationTiles()
    {
        throw new Exception("This not currently supported");
    }

    protected override WFCTile copyForRotation(int rot)
    {
        throw new Exception("This not currently supported");
    }
}