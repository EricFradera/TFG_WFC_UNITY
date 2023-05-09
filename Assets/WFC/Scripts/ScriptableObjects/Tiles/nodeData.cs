using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Object = System.Object;
using Vector2 = System.Numerics.Vector2;

[Serializable]
public class nodeData
{
    public Vector2 position;
    public List<nodeRelation> relationShips = new List<nodeRelation>();

    public void deleteRelFromHelper()
    {
        foreach (var relation in relationShips)
        {
            var tile = relation.inputTile as WFCTile;
            if (tile != null) tile.adjacencyCodes[relation.indexOutput] = null;
        }
    }

    public void removeRel(int dirParent, object child, int dirChild)
    {
        foreach (var relation in relationShips.Where(relation =>
                     relation.indexOutput == dirParent && relation.inputTile == child &&
                     relation.indexInput == dirChild))
        {
            relationShips.Remove(relation);
            return;
        }
    }

    
}