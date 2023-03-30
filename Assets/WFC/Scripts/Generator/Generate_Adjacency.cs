using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using WFC;

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
                if (tileOrigin.id_up == tileDest.id_down) tileOrigin.up.Add(tileDest.tileId);
                if(tileOrigin.id_right==tileDest.id_left)tileOrigin.right.Add(tileDest.tileId);
                if(tileOrigin.id_down==tileDest.id_up)tileOrigin.down.Add(tileDest.tileId);
                if(tileOrigin.id_left==tileDest.id_right)tileOrigin.left.Add(tileDest.tileId);
            }
        }
    }

    private void cleanUp()
    {
        foreach (var tile in _adjacencyGen)
        {
            tile.up.Clear();
            tile.right.Clear();
            tile.down.Clear();
            tile.left.Clear();
        }
    }

    

}

