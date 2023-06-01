using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;


public class WFC1DProc : WFCAbstractProc
{
    private Direction[] direction = { Direction.XPlus, Direction.XMinus };


    public WFC1DProc(List<WFCTile> listOfTiles, WFCManager manager) : base(listOfTiles, manager)
    {
    }

    public override ITopoArray<WFCTile> RunWFC(int size)
    {
        if (listOfTiles is null) throw new Exception("List of tiles is Empty");
        var model = RunModel();
        var topology = new GridTopology(size, 1, periodic: false);
        var propagator = new TilePropagator(model, topology, true); //backtracking need s tp be able to turn off
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
            model.SetFrequency(tileMap[tile], 1);
        }

        for (int i = 0; i < genList.Count; i++)
        {
            for (int dir = 0; dir < genList[i].adjacencyPairs.Length; dir++)
            {
                for (int j = 0; j < genList[i].adjacencyPairs[dir].Count; j++)
                {
                    model.AddAdjacency(tileMap[genList[i]], tileMap[genList[i].adjacencyPairs[dir][j]],
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