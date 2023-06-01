using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using WFC;
using Debug = UnityEngine.Debug;

[ExecuteInEditMode]
public class Generate_Adjacency
{
    private List<WFCTile> _adjacencyGen;

    public void match_Tiles(List<WFCTile> newList)
    {
        _adjacencyGen = newList;
        cleanUp();
        foreach (var tile in _adjacencyGen)tile.genWithRelAdjacency();
        
        foreach (var tileOrigin in _adjacencyGen)
        {
            foreach (var tileDest in _adjacencyGen)
            {
                for (int i = 0; i < tileOrigin.adjacencyCodes.Length; i++)
                {
                    if (match(tileOrigin, tileDest, i))
                        tileOrigin.GeneratedAdjacencyPairs[i].Add(tileDest);
                }
            }
            tileOrigin.MixAdj();
        }
    }

    private bool match(WFCTile tileOrigin, WFCTile tileDest, int i)
    {
        if (tileOrigin.adjacencyCodes[i] is null || tileDest.adjacencyCodes[tileDest.GetInverse(i)] is null)
            return false;
        
        var tileOriginSize = tileOrigin.adjacencyCodes[i].socketCodes.Count;
        var tileDestSize = tileDest.adjacencyCodes[tileDest.GetInverse(i)].socketCodes.Count;

        if (tileOriginSize == 0 || tileDestSize == 0) return false;
        if (tileOriginSize != tileDestSize) return false;
        for (int j = 0; j < tileOrigin.adjacencyCodes[i].socketCodes.Count(); j++)
        {
            if (tileOrigin.adjacencyCodes[i].getCode(false) !=
                tileDest.adjacencyCodes[tileDest.GetInverse(i)].getCode(true))
                return false;
        }

        return true;
    }


    private void cleanUp()
    {
        foreach (var tile in _adjacencyGen)
        {
            for (int i = 0; i < tile.GeneratedAdjacencyPairs.Length; i++)
            {
                if (tile.GeneratedAdjacencyPairs[i] is null) tile.GeneratedAdjacencyPairs[i] = new List<WFCTile>();
                else tile.GeneratedAdjacencyPairs[i].Clear();
            }
        }
    }
}