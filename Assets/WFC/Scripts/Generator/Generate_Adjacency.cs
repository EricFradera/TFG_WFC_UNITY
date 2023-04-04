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
    private List<WFC2DTile> _adjacencyGen;

    public Generate_Adjacency(List<WFC2DTile> adjacencyGen)
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
                        tileOrigin.adjacencyPairs[i].Add(tileDest.tileId);
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
                if (tile.adjacencyPairs[i] is null) tile.adjacencyPairs[i] = new List<int>();
                else tile.adjacencyPairs[i].Clear();
            }
        }
    }
}