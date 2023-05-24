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
public class nodeData: ScriptableObject
{
    public Vector2 position;
    public List<nodeRelation> relationShips = new List<nodeRelation>();


    public void addNewRel()
    {
        
    }

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


    public void addNewRel(int dir, InputCodeData data)
    {
        var newRel = ScriptableObject.CreateInstance<nodeRelation>();
    }

    public void addNewRel(int dir, WFCTile tile)
    {
        throw new NotImplementedException();
    }
}