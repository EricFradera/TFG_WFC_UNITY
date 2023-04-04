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
                /*for (int i = 0; i < tileOrigin.adjacencyCodes.Length; i++)S
                {
                    if (tileOrigin.adjacencyCodes[i] == tileDest.adjacencyCodes[tileDest.GetInverse(i)])
                    {
                        if (tileOrigin.adjacencyPairs[i] is null) tileOrigin.adjacencyPairs[i] = new List<int>();
                        tileOrigin.adjacencyPairs[i].Add(tileDest.tileId);
                    }

                    //tileOrigin.up.Add(tileDest.tileId);
                }*/

                for (int i = 0; i < tileOrigin.adjacencyCodes.Length; i++)
                {
                    if (tileOrigin.adjacencyCodes[i] == tileDest.adjacencyCodes[tileDest.GetInverse(i)])
                        tileOrigin.adjacencyPairs[i].Add(tileDest.tileId);
                }
                
                /*if (tileOrigin.adjacencyCodes[0] == tileDest.adjacencyCodes[2])
                {
                    //tileOrigin.up.Add(tileDest.tileId);
                    tileOrigin.adjacencyPairs[0].Add(tileDest.tileId);
                }

                if (tileOrigin.adjacencyCodes[1] == tileDest.adjacencyCodes[3])
                {
                    //tileOrigin.right.Add(tileDest.tileId);
                    tileOrigin.adjacencyPairs[1].Add(tileDest.tileId);
                }

                if (tileOrigin.adjacencyCodes[2] == tileDest.adjacencyCodes[0])
                {
                    // tileOrigin.down.Add(tileDest.tileId);
                    tileOrigin.adjacencyPairs[2].Add(tileDest.tileId);
                }

                if (tileOrigin.adjacencyCodes[3] == tileDest.adjacencyCodes[1])
                {
                    //  tileOrigin.left.Add(tileDest.tileId);
                    tileOrigin.adjacencyPairs[3].Add(tileDest.tileId);
                }*/

                /*if (tileOrigin.id_up == tileDest.id_down) tileOrigin.up.Add(tileDest.tileId);
                if (tileOrigin.id_right == tileDest.id_left) tileOrigin.right.Add(tileDest.tileId);
                if (tileOrigin.id_down == tileDest.id_up) tileOrigin.down.Add(tileDest.tileId);
                if (tileOrigin.id_left == tileDest.id_right) tileOrigin.left.Add(tileDest.tileId);
                */
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

            /*tile.up.Clear();
            tile.right.Clear();
            tile.down.Clear();
            tile.left.Clear();*/
        }
    }
}