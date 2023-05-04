using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;

namespace WFC
{
    [System.Serializable]
    public abstract class WFCConfig : ScriptableObject
    {
        public String configurationName;
        public int configurationID;

        public List<WFCTile> wfcTilesList = new List<WFCTile>();

        public List<InputCodeData> nodeHelpers;

        public abstract WFCTile CreateNodeTile();

        public InputCodeData CreateNodeHelper(Type type)
        {
            InputCodeData codeData;
            if (type == typeof(ColorCodeData)) codeData = ScriptableObject.CreateInstance<ColorCodeData>();
            else codeData = ScriptableObject.CreateInstance<InputCodeData>();
            codeData.Init();
            nodeHelpers.Add(codeData);
            AssetDatabase.AddObjectToAsset(codeData, this);
            AssetDatabase.SaveAssets();
            return codeData;
        }

        public void DeleteNodeHelper(InputCodeData data)
        {
            data.nodeData.deleteRelFromHelper();
            nodeHelpers.Remove(data);
            AssetDatabase.RemoveObjectFromAsset(data);
            AssetDatabase.SaveAssets();
        }

        public void DeleteNodeTile(WFCTile tile)
        {
            wfcTilesList.Remove(tile);
            AssetDatabase.RemoveObjectFromAsset(tile);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(WFCTile parent, WFCTile child, int dirParent, int dirChild)
        {
            if (parent == null || child == null) return;
            parent.adjacencyPairs[dirParent].Add(child);
            parent.nodeData.relationShips.Add(new nodeRelation(dirChild, child, dirChild));
            child.adjacencyPairs[dirChild].Add(parent);
        }

        public void AddHelper(InputCodeData data, WFCTile tile, int dir)
        {
            if (data == null || tile == null) return;
            tile.nodeData.relationShips.Add(new nodeRelation(dir, data));
            data.nodeData.relationShips.Add(new nodeRelation(dir, tile));
            tile.adjacencyCodes[dir] = data;
        }

        public void RemoveChild(WFCTile parent, WFCTile child, int dirParent, int dirChild)
        {
            parent.adjacencyPairs[dirParent].Remove(child);
            child.adjacencyPairs[dirChild].Remove(parent);
            parent.nodeData.relationShips.Remove(new nodeRelation(dirChild, child, dirChild));
        }

        public List<WFCTile> GetChildren(WFCTile parent, int dir)
        {
            return parent.adjacencyPairs[dir] ?? new List<WFCTile>();
        }
    }
}