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
public class nodeData : ScriptableObject
{
    public float x;
    public float y;
    public int num;
    public List<nodeRelation> relationShips = new List<nodeRelation>();

    [Serializable]
    public struct nodeRelation
    {
        public int indexOutput;
        public WFCTile inputTile;
        public int indexInput;
        public InputCodeData inputCodeData;

        public void setTileRel(int indexOutput, WFCTile inputTile, int indexInput)
        {
            this.indexOutput = indexOutput;
            this.inputTile = inputTile;
            this.indexInput = indexInput;
            this.inputCodeData = null;
        }

        public void setHelperRel(int indexOutput, InputCodeData inputTile)
        {
            this.indexOutput = indexOutput;
            this.inputCodeData = inputTile;
            this.indexInput = 0;
            this.inputTile = null;
        }

        public object getInput()
        {
            if (inputTile is null) return inputCodeData;
            return inputTile;
        }
    }

    public void deleteAllRelFromHelper()
    {
        foreach (var relation in relationShips)
        {
            var tile = relation.inputTile;
            if (tile != null) tile.adjacencyCodes[relation.indexOutput] = null;
        }
    }

    public void deleteAllRelFromTile()
    {
        relationShips.Clear();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
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
        var newRel = new nodeRelation();
        newRel.setHelperRel(dir, data);
        relationShips.Add(newRel);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void addNewRel(int dirParent, WFCTile child, int dirChild)
    {
        var newRel = new nodeRelation();
        newRel.setTileRel(dirParent, child, dirChild);
        relationShips.Add(newRel);
        num++;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void saveData()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public Rect getPosition()
    {
        var pos = new Rect();
        pos.xMin = x;
        pos.yMin = y;
        return pos;
    }
}