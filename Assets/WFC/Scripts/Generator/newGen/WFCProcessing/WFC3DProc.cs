using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;
using UnityEngine;
using WFC;
using Resolution = DeBroglie.Resolution;

public class WFC3DProc : WFCAbstractProc
{
    private Direction[] direction =
        { Direction.YPlus, Direction.XPlus, Direction.YMinus, Direction.XMinus, Direction.ZPlus, Direction.ZMinus };

    public WFC3DProc(List<WFCTile> listOfTiles, WFCManager manager) : base(listOfTiles, manager)
    {
    }

    public override ITopoArray<WFCTile> RunWFC(int size)
    {
        if (listOfTiles is null) throw new Exception("List of tiles is Empty");
        var model = RunModel();
        var topology = new GridTopology(size, size, size, periodic: false);
        var propagator = new TilePropagator(model, topology, false);
        var status = propagator.Run();
        if (status != Resolution.Decided) throw new Exception("The WFC resulted as undecided");
        var output = propagator.ToValueArray<WFCTile>();
        return output;
    }

    public override AdjacentModel RunModel()
    {
        adjacency.match_Tiles(listOfTiles);
        var model = new AdjacentModel(DirectionSet.Cartesian3d);
        Dictionary<WFCTile, Tile> tileMap = new Dictionary<WFCTile, Tile>();

        foreach (WFC3DTile tile in listOfTiles)
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

    public override void clearRotationList()
    {
        throw new System.NotImplementedException();
    }
}