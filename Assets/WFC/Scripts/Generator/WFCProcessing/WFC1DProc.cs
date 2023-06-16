using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;
using UnityEngine;
using Resolution = DeBroglie.Resolution;


public class WFC1DProc : WFCAbstractProc
{
    private Direction[] direction = { Direction.XPlus, Direction.XMinus };


    public WFC1DProc(List<WFCTile> listOfTiles, WFCManager manager) : base(listOfTiles, manager)
    {
    }

    public override ITopoArray<WFCTile> RunWFC(float m_gridExtent, float m_gridSize, bool useBacktracking)
    {
        if (listOfTiles is null) throw new Exception("List of tiles is Empty");
        var model = RunModel();
        var topology = new GridTopology(Mathf.RoundToInt(m_gridSize), 1, periodic: false);
        var propagator = new TilePropagator(model, topology, useBacktracking); 
        var status = propagator.Run();
        if (status != Resolution.Decided) throw new Exception("The WFC resulted as undecided");
        var output = propagator.ToValueArray<WFCTile>();
        return output;
    }

    public override AdjacentModel RunModel()
    {
        List<WFCTile> genList = new List<WFCTile>();
        genList.AddRange(listOfTiles);

        adjacency.match_Tiles(genList);
        var model = new AdjacentModel(DirectionSet.Cartesian2d);
        Dictionary<WFCTile, Tile> tileMap = new Dictionary<WFCTile, Tile>();
        foreach (WFC1DTile tile in genList)
        {
            tileMap.Add(tile, new Tile(tile));
            model.SetFrequency(tileMap[tile], tile.frequency);
        }

        for (int i = 0; i < genList.Count; i++)
        {
            for (int dir = 0; dir < genList[i].GeneratedAdjacencyPairs.Length; dir++)
            {
                for (int j = 0; j < genList[i].GeneratedAdjacencyPairs[dir].Count; j++)
                {
                    model.AddAdjacency(tileMap[genList[i]], tileMap[genList[i].GeneratedAdjacencyPairs[dir][j]],
                        direction[dir]);
                }
            }
        }

        return model;
    }

    public override void clearRotationList()
    {
        throw new Exception("Rotations not supported in this version");
    }
}