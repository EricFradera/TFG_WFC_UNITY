using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class nodeData
{
    public Vector2 position;
    public List<nodeRelation> relationShips = new List<nodeRelation>();

    public void deleteRelFromHelper()
    {
        foreach (var relation in relationShips)
        {
            Debug.Log("we reach here");
            var tile = relation.inputTile as WFCTile;
            if (tile != null) tile.adjacencyCodes[relation.indexOutput] = null;
        }
    }
}