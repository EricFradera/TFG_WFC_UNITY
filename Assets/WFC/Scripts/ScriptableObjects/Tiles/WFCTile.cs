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

    //Each tile has its own adjacency codes (ej: up,right,down,left)
    public InputCodeData[] adjacencyCodes;

    public void genRotation()
    {
        List<bool> rotations = GetListOfRotations();
        for (int i = 1; i <= rotations.Count; i++)
        {
            //foreach
            //degree val = i
            if (rotations[i - 1])
            {
                InputCodeData[] tempList = new InputCodeData[adjacencyCodes.Length];
                for (int j = 0; j < adjacencyCodes.Length; j++)
                {
                    tempList[j] =
                        adjacencyCodes[(i + j) >= adjacencyCodes.Length ? (i + j) - adjacencyCodes.Length : (i + j)];
                }
            }
        }
    }
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
    
    public abstract List<WFCTile> generateTilesFromRotations();

    public abstract List<bool> GetListOfRotations();

    public abstract WFCTile fillData(WFCTile data, int rot);
}