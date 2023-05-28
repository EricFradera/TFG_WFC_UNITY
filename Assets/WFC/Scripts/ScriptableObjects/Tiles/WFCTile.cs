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
    public float frequency=1f;

    //rotation ver
    public int rotationModule = 0;

    //Each tile has its own adjacency codes (ej: up,right,down,left)
    public InputCodeData[] adjacencyCodes;

    //this is not getting serialised, so it won't save between sessions
    public List<WFCTile>[] adjacencyPairs;

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
        adjacencyCodes = new InputCodeData[dim];
        for (int i = 0; i < dim; i++) this.adjacencyPairs[i] = new List<WFCTile>();
    }

    public void deleteNodeData()
    {
        nodeData.deleteAllRelFromTile();
        AssetDatabase.RemoveObjectFromAsset(nodeData);
    }


    public int Getdim()
    {
        return dim;
    }

    public abstract List<WFCTile> getRotationTiles();

    protected abstract WFCTile copyForRotation(int rot);
}