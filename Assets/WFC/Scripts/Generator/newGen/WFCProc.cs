using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Resolution = DeBroglie.Resolution;

[ExecuteInEditMode]
public class WFCProc
{
    private Generate_Adjacency adjacency;
    private List<WFCTile> listOfTiles;
    private Direction[] direction = { Direction.YPlus, Direction.XPlus, Direction.YMinus, Direction.XMinus };

    public WFCProc(List<WFCTile> listOfTiles)
    {
        this.listOfTiles = listOfTiles;
        adjacency = new Generate_Adjacency(this.listOfTiles);
    }

    public ITopoArray<WFCTile> runWFC(int size)
    {
        if (listOfTiles is null) throw new Exception("List of tiles is Empty");
        var model = run2DModel();
        var topology = new GridTopology(size, size, periodic: false);
        var propagator = new TilePropagator(model, topology);
        var status = propagator.Run();
        if (status != Resolution.Decided) throw new Exception("The WFC resulted as undecided");
        var output = propagator.ToValueArray<WFCTile>();
        return output;
    }

    private AdjacentModel run2DModel()
    {
        adjacency.match_Tiles();
        var model = new AdjacentModel(DirectionSet.Cartesian2d);
        Dictionary<WFCTile, Tile> tileMap = new Dictionary<WFCTile, Tile>();
        foreach (var tile in listOfTiles)
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


    //Setters
    public void SetList(List<WFCTile> newList) => this.listOfTiles = newList;
}