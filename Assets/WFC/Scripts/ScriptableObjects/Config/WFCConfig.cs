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

        //maybe create here the node???
        public List<Node> nodeHelpers;

        public abstract WFCTile CreateNodeTile();

        public Node createNodeHelper()
        {
            // Helper node have to be created here
            return new Node();
        }

        public void deleteNodeHelper(Node nodeToDelete)
        {
            //Helper node is created here
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