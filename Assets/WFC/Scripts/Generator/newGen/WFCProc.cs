using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Rot;
using DeBroglie.Topo;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using WFC;
using Resolution = DeBroglie.Resolution;

[ExecuteInEditMode]
public class WFCProc
{
    private Generate_Adjacency adjacency;
    private List<WFCTile> listOfTiles;
    private WFCManager configManager;
    private Direction[] direction = { Direction.YPlus, Direction.XPlus, Direction.YMinus, Direction.XMinus };
    private bool useRotation;

    public WFCProc(List<WFCTile> listOfTiles, WFCManager manager)
    {
        this.listOfTiles = listOfTiles;
        adjacency = new Generate_Adjacency(this.listOfTiles);
        this.configManager = manager;
        this.useRotation = manager.getUseRotations();
    }

    public ITopoArray<WFCTile> runWFC(int size)
    {
        if (listOfTiles is null) throw new Exception("List of tiles is Empty");
        var model = run2DModel();
        var topology = new GridTopology(size, size, periodic: false);
        var propagator = new TilePropagator(model, topology, true);
        var status = propagator.Run();
        if (status != Resolution.Decided) throw new Exception("The WFC resulted as undecided");
        var output = propagator.ToValueArray<WFCTile>();
        Debug.Log("good");
        return output;
    }

    private AdjacentModel run2DModel()
    {
        //AddRotations();
        adjacency.match_Tiles();
        var model = new AdjacentModel(DirectionSet.Cartesian2d);
        Dictionary<WFCTile, Tile> tileMap = new Dictionary<WFCTile, Tile>();
        foreach (WFC2DTile tile in listOfTiles)
        {
            tileMap.Add(tile, new Tile(tile));
            model.SetFrequency(tileMap[tile], 1);
        }

        for (int i = 0; i < listOfTiles.Count; i++)
        {
            for (int dir = 0; dir < listOfTiles[i].adjacencyPairs.Length; dir++)
            {
                for (int j = 0; j < listOfTiles[i].adjacencyPairs[dir].Count; j++)
                {
                    model.AddAdjacency(tileMap[listOfTiles[i]], tileMap[listOfTiles[i].adjacencyPairs[dir][j]],
                        direction[dir]);
                }
            }
        }

        return model;
    }


    private void AddRotations()
    {
        List<WFCTile> newRotations = new List<WFCTile>();
        foreach (var tile in listOfTiles)
        {
            newRotations.AddRange(tile.getRotationTiles());
        }
        listOfTiles.AddRange(newRotations);
    }

    //Setters
    public void SetList(List<WFCTile> newList) => this.listOfTiles = newList;
}