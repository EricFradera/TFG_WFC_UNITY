using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WFCTile : ScriptableObject
{
    private string tilename;

    public int tileId;

    //Each tile has its own adjacency codes (ej: up,right,down,left)
    public String[] adjacencyCodes;

    public List<int>[] adjacencyPairs;

    // Node data
    public Vector2 position;

    //always have to be an inverse function for tile matching
    public abstract int GetInverse(int indexDirection);
}