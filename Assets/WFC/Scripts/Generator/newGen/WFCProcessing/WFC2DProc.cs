using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;
using UnityEngine;
using WFC;

public class WFC2DProc : WFCAbstractProc
{
    private Direction[] direction = { Direction.YPlus, Direction.XPlus, Direction.YMinus, Direction.XMinus };
    private List<WFCTile> listOfRotatedTiles;
    private bool useRotation;


    public WFC2DProc(List<WFCTile> listOfTiles, WFCManager manager) : base(listOfTiles, manager)
    {
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
        var model = new AdjacentModel(DirectionSet.Cartesian2d);
        Dictionary<WFCTile, Tile> tileMap = new Dictionary<WFCTile, Tile>();
        foreach (WFC2DTile tile in genList)
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