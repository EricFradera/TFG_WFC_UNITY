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

    public Generate_Adjacency(List<WFCTile> adjacencyGen)
    {
        this._adjacencyGen = adjacencyGen;
    }

    public void match_Tiles()
    {
        cleanUp();
        foreach (var tileOrigin in _adjacencyGen)
        {
            foreach (var tileDest in _adjacencyGen)
            {
                for (int i = 0; i < tileOrigin.adjacencyCodes.Length; i++)
                {
                    if (tileOrigin.adjacencyCodes[i] == tileDest.adjacencyCodes[tileDest.GetInverse(i)])
                        tileOrigin.adjacencyPairs[i].Add(tileDest);
                }
            }
        }
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