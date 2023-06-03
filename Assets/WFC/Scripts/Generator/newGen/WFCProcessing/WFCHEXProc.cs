using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;
using UnityEngine;
using Resolution = UnityEngine.Resolution;

public class WFCHEXProc : WFCAbstractProc
{
    
    private List<WFCTile> listOfRotatedTiles;
    private bool useRotation;

    public WFCHEXProc(List<WFCTile> listOfTiles, WFCManager manager) : base(listOfTiles, manager)
    {
    }

    public override ITopoArray<WFCTile> RunWFC(float m_gridExtent, float m_gridSize)
    {
        //This count is probably wrong
        var size = Mathf.RoundToInt((m_gridExtent * 2) / m_gridSize);
        if (size % 2 == 0) size++;
        size--;


        if (listOfTiles is null) throw new Exception("List of tiles is Empty");
        var model = RunModel();
        //var topology = new GridTopology(size, size, periodic: false);
        var topology = new GridTopology(DirectionSet.Hexagonal2d, size, size, periodicX: false, periodicY: false);
        var propagator = new TilePropagator(model, topology, true); //backtrackinh need s tp be able to turn off
        var status = propagator.Run();
        if (status != DeBroglie.Resolution.Decided) throw new Exception("The WFC resulted as undecided");
        var output = propagator.ToValueArray<WFCTile>();
        return output;
    }

    public override AdjacentModel RunModel()
    {
        var dirHelper = DirectionSet.Hexagonal2d;
        List<WFCTile> genList = new List<WFCTile>();
        genList.AddRange(listOfTiles);
        adjacency.match_Tiles(genList);
        var model = new AdjacentModel(dirHelper);
        Dictionary<WFCTile, Tile> tileMap = new Dictionary<WFCTile, Tile>();
        foreach (WFCHEXTile tile in genList)
        {
            tileMap.Add(tile, new Tile(tile));
            model.SetFrequency(tileMap[tile], 1);
        }


        for (int i = 0; i < genList.Count; i++)
        {
            for (int dir = 0; dir < genList[i].GeneratedAdjacencyPairs.Length; dir++)
            {
                for (int j = 0; j < genList[i].GeneratedAdjacencyPairs[dir].Count; j++)
                {
                    model.AddAdjacency(tileMap[genList[i]], tileMap[genList[i].GeneratedAdjacencyPairs[dir][j]],
                        dirHelper.GetDirection(dirHelper.DX[dir], dirHelper.DY[dir],
                            dirHelper.DZ[dir]));
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