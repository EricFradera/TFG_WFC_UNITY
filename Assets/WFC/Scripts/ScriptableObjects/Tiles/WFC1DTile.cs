using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(menuName = "WFC components/WFC2DTile", order = 1, fileName = "WFC2dTile"), Serializable]
public class WFC1DTile : WFCTile
{
    [JsonIgnore] public Texture2D[] tileTexture;

    public AssetType assetType;

    public enum AssetType
    {
        useTexture,
        useGameObject
    }


    public WFC1DTile()
    {
        dim = 2;
        this.adjacencyCodes = new InputCodeData[dim];
        this.adjacencyPairs = new List<WFCTile>[dim];
        this.GeneratedAdjacencyPairs = new List<WFCTile>[dim];
    }

    private enum IndexDirection
    {
        RIGHT,
        LEFT
    }

    public override int GetInverse(int indexDirection)
    {
        return (IndexDirection)indexDirection switch
        {
            IndexDirection.RIGHT => (int)IndexDirection.LEFT,
            IndexDirection.LEFT => (int)IndexDirection.RIGHT,
            _ => -1
        };
    }

    public override void InitDataStructures()
    {
        if (adjacencyCodes is null) adjacencyCodes = new InputCodeData[dim];
        if (adjacencyPairs is null)
        {
            adjacencyPairs = new List<WFCTile>[dim];
            for (int i = 0; i < dim; i++) adjacencyPairs[i] = new List<WFCTile>();
        }

        if (GeneratedAdjacencyPairs is null)
        {
            GeneratedAdjacencyPairs = new List<WFCTile>[dim];
            for (int i = 0; i < dim; i++) GeneratedAdjacencyPairs[i] = new List<WFCTile>();
        }

        if (tileVisuals is null || tileVisuals.Length == 0) tileVisuals = new GameObject[1];
        if (tileTexture is null || tileTexture.Length == 0) tileTexture = new Texture2D[1];
    }


    public override List<WFCTile> getRotationTiles()
    {
        throw new NotImplementedException();
    }

    protected override WFCTile copyForRotation(int rot, int axis)
    {
        throw new NotImplementedException();
    }
}