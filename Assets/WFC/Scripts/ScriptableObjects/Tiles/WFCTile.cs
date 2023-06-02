using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

[Serializable]
public abstract class WFCTile : ScriptableObject
{
    public string tileName;

    public string tileId;

    //Sides
    protected int dim;
    public float frequency = 1f;

    //rotation ver
    public int rotationModule = 0;

    //Each tile has its own adjacency codes (ej: up,right,down,left)
    public InputCodeData[] adjacencyCodes;

    //this is not getting serialised, so it won't save between sessions
    public List<WFCTile>[] adjacencyPairs;

    //I need a second structure here
    public List<WFCTile>[] GeneratedAdjacencyPairs;


    // Node data
    public nodeData nodeData;

    //Asset data
    [JsonIgnore] public GameObject tileVisuals;


    //always have to be an inverse function for tile matching
    public abstract int GetInverse(int indexDirection);

    public void saveData()
    {
        nodeData.saveData();
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void InitDataStructures()
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
    }

    public void deleteNodeData()
    {
        nodeData.deleteAllRelFromTile();
        AssetDatabase.RemoveObjectFromAsset(nodeData);
    }

    public void MixAdj()
    {
        for (int i = 0; i < dim; i++)
        {
            if (adjacencyPairs[i] is null) continue;
            if (adjacencyPairs[i].Count != 0)
                GeneratedAdjacencyPairs[i].AddRange(adjacencyPairs[i]);
        }
    }

    public void genWithRelAdjacency()
    {
        if (adjacencyPairs is null) InitDataStructures();
        foreach (var rel in nodeData.relationShips)
        {
            if (rel.inputTile is null) continue;
            adjacencyPairs[rel.indexOutput].Add(rel.inputTile);
            rel.inputTile.addRel(this, this.GetInverse(rel.indexOutput));
        }
    }

    public void addRel(WFCTile parent, int index)
    {
        if (adjacencyPairs is null) InitDataStructures();
        adjacencyPairs[index].Add(parent);
    }


    public int Getdim()
    {
        return dim;
    }

    public abstract List<WFCTile> getRotationTiles();

    protected abstract WFCTile copyForRotation(int rot);
}