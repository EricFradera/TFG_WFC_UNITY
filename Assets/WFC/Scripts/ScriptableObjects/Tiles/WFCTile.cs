using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class WFCTile : ScriptableObject
{
    public string tileName;

    public string tileId;

    //Each tile has its own adjacency codes (ej: up,right,down,left)
    public InputCodeData[] adjacencyCodes;

    public List<WFCTile>[] adjacencyPairs;

    // Node data
    public nodeData nodeData;

    //Asset data
    public GameObject tileVisuals;

    //always have to be an inverse function for tile matching
    public abstract int GetInverse(int indexDirection);
}