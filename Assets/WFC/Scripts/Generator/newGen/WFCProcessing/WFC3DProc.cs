using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;
using UnityEngine;
using WFC;
using Object = UnityEngine.Object;
using Resolution = DeBroglie.Resolution;

public class WFC3DProc : WFCAbstractProc
{
    private Direction[] direction =
        { Direction.YPlus, Direction.YMinus, Direction.XPlus, Direction.XMinus, Direction.ZPlus, Direction.ZMinus };

    private List<WFCTile> listOfRotatedTiles;
    private bool useRotation;

    public WFC3DProc(List<WFCTile> listOfTiles, WFCManager manager) : base(listOfTiles, manager)
    {
    }

    public override ITopoArray<WFCTile> RunWFC(float m_gridExtent, float m_gridSize)
    {
        var size = Mathf.RoundToInt((m_gridExtent * 2) / m_gridSize);
        if (size % 2 == 0) size++;
        size--;

        if (listOfTiles is null) throw new Exception("List of tiles is Empty");
        var model = RunModel();
        var topology = new GridTopology(size, size, size, periodic: false);
        var propagator = new TilePropagator(model, topology, true);
        var status = propagator.Run();
        if (status != Resolution.Decided) throw new Exception("The WFC resulted as undecided");
        var output = propagator.ToValueArray<WFCTile>();
        return output;
    }

    public override AdjacentModel RunModel()
    {
        List<WFCTile> genList = new List<WFCTile>();
        genList.AddRange(listOfTiles);
        if (useRotation)
        {
            AddRotations();
            genList.AddRange(listOfRotatedTiles);
        }


        adjacency.match_Tiles(genList);
        var model = new AdjacentModel(DirectionSet.Cartesian3d);
        Dictionary<WFCTile, Tile> tileMap = new Dictionary<WFCTile, Tile>();

        foreach (WFC3DTile tile in genList)
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
                        direction[dir]);
                }
            }
        }

        return model;
    }

    private void AddRotations()
    {
        listOfRotatedTiles = new List<WFCTile>();
        foreach (var tile in listOfTiles)
        {
            listOfRotatedTiles.AddRange(tile.getRotationTiles());
        }
    }

    public override void clearRotationList()
    {
        foreach (var tile in listOfRotatedTiles)
        {
            Object.DestroyImmediate(tile);
        }

        listOfRotatedTiles.Clear();
    }

    public void setUseRotations(bool useRotations)
    {
        this.useRotation = useRotations;
    }
}