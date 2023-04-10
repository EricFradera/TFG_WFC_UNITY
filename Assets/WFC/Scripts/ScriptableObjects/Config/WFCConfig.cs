using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;
using UnityEditor.Rendering;

namespace WFC
{
    [System.Serializable]
    public abstract class WFCConfig : ScriptableObject
    {
        public String configurationName;
        public int configurationID;
        public List<WFCTile> wfcTilesList = new List<WFCTile>();

        public abstract WFCTile CreateNodeTile();

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
            parent.nodeData.outputConnections[dirParent].Add(child);
            child.adjacencyPairs[dirChild].Add(parent);
            child.nodeData.inputConnections[dirChild].Add(parent);
        }

        public void RemoveChild(WFCTile parent, WFCTile child, int dirParent, int dirChild)
        {
            parent.adjacencyPairs[dirParent].Remove(child);
            child.adjacencyPairs[dirChild].Remove(parent);
            parent.nodeData.outputConnections[dirParent].Remove(child);
            child.nodeData.inputConnections[dirChild].Remove(parent);
        }

        public List<WFCTile> GetChildren(WFCTile parent, int dir)
        {
            return parent.adjacencyPairs[dir] ?? new List<WFCTile>();
        }
    }
}