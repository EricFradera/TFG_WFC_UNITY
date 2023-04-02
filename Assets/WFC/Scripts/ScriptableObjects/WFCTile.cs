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

    /// <summary>
    /// Ther has to be another structure that
    /// stores the adjacency data ina different way it is now
    /// For now its 4 list in the case of the 2d tile
    /// In the end a dingle structure should be able to store everything
    /// probably in a jagged array.
    /// </summary>
    // Node data
    public Vector2 position;
}