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
    [CreateAssetMenu(menuName = "WFC components/WFCConfig", order = 3, fileName = "WFCConfig"), Serializable]
    public class WFCConfig : ScriptableObject
    {
        public String configurationName;

        public int configurationID;

        //Eventually should be changed for a List<WFCTile>
        public List<WFCTile> wfcTilesList = new List<WFCTile>();

        // WFC2DTile should be an abstraction so its more flexible, such as pattern or tile
        public WFCTile CreateNodeTile()
        {
            WFC2DTile nodeTile = CreateInstance<WFC2DTile>();
            nodeTile.InitDataStructures();
            //nodeTile.tileId = GUID.Generate().ToString();
            wfcTilesList.Add(nodeTile);
            AssetDatabase.AddObjectToAsset(nodeTile, this);
            AssetDatabase.SaveAssets();
            return nodeTile;
        }

        public void DeleteNodeTile(WFCTile tile)
        {
            wfcTilesList.Remove(tile);
            AssetDatabase.RemoveObjectFromAsset(tile);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(WFC2DTile parent, WFC2DTile child, int dirParent, int dirChild)
        {
            if (parent is null) Debug.Log("PARENT IS NULL");
            if (child is null) Debug.Log("CHILD IS NULL");
            if (parent.adjacencyPairs[dirParent] is null) Debug.Log("STORAGE NOT INITIALISED");
            parent.adjacencyPairs[dirParent].Add(child);
            //parent.test.Add(child.tileId);
            //child.adjacencyPairs[dirChild].Add(parent);
            //child.test.Add(parent.tileId);
            //printList(parent, dirParent);
        }

        public void RemoveChild(WFC2DTile parent, WFC2DTile child, int dirParent, int dirChild)
        {
            parent.adjacencyPairs[dirParent].Remove(child);
            //parent.test.Remove(child.tileId);
            //child.adjacencyPairs[dirChild].Remove(parent);
            //child.test.Remove(parent.tileId);
            //printList(parent, dirParent);
        }

        public List<WFCTile> GetChildren(WFCTile parent, int dir)
        {
            return parent.adjacencyPairs[dir] ?? new List<WFCTile>();
        }
    }
}