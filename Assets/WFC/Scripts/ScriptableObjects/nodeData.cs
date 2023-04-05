using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class nodeData
{
    private nodeData()
    {
        for (int i = 0; i < 4; i++)
        {
            inputConnections[i] = new List<WFCTile>();
            outputConnections[i] = new List<WFCTile>();
        }
    }

    public Vector2 position;
    public List<WFCTile>[] inputConnections = new List<WFCTile>[4];
    public List<WFCTile>[] outputConnections = new List<WFCTile>[4];
}