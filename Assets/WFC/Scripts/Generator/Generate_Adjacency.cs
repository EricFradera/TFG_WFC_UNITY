using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        foreach (var tileOrigin in _adjacencyGen)
        {
            foreach (var tileDest in _adjacencyGen)
            {
                for (int i = 0; i < tileOrigin.adjacencyCodes.Length; i++)
                {
                    if (match(tileOrigin, tileDest, i))
                        tileOrigin.adjacencyPairs[i].Add(tileDest);
                }
            }
        }
    }

    private bool match(WFCTile tileOrigin, WFCTile tileDest, int i)
    {
        if (tileOrigin.adjacencyCodes[i].code is not null &&
            tileDest.adjacencyCodes[tileDest.GetInverse(i)].code is not null)
        {
            if (tileOrigin.adjacencyCodes[i].code == tileDest.adjacencyCodes[tileDest.GetInverse(i)].code)
                return true;
        }

        return false;
    }

    private void cleanUp()
    {
        foreach (var tile in _adjacencyGen)
        {
            for (int i = 0; i < tile.adjacencyPairs.Length; i++)
            {
                if (tile.adjacencyPairs[i] is null) tile.adjacencyPairs[i] = new List<WFCTile>();
                else tile.adjacencyPairs[i].Clear();
            }
        }
    }
}