using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WFC components/WFC2DTile", order = 1, fileName = "WFC2dTile"), Serializable]
public class WFC1DTile : WFCTile
{
    [Rename("Flip tile")] public bool flip;


    public WFC1DTile()
    {
        dim = 2;
        this.adjacencyCodes = new InputCodeData[dim];
        this.adjacencyPairs = new List<WFCTile>[dim];
    }

    private enum IndexDirection
    {
        RIGHT,
        LEFT
    }

    public override int GetInverse(int indexDirection)
    {
        return (IndexDirection)indexDirection switch
        {
            IndexDirection.RIGHT => (int)IndexDirection.LEFT,
            IndexDirection.LEFT => (int)IndexDirection.RIGHT,
            _ => -1
        };
    }


    public override List<WFCTile> getRotationTiles()
    {
        throw new NotImplementedException();
    }

    protected override WFCTile copyForRotation(int rot)
    {
        throw new NotImplementedException();
    }
}