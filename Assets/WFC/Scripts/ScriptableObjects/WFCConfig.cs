using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;

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
            WFC2DTile nodeTile = ScriptableObject.CreateInstance<WFC2DTile>();
            nodeTile.tileId = 44;
            //GUID.Generate.ToString --> generate unique ID
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

        public void AddChild(WFCTile parent, WFCTile child, int dir)
        {
            parent.adjacencyPairs[dir].Add(child);
        }

        public void RemoveChild(WFC2DTile parent, WFC2DTile child, int dir)
        {
            //parent.adjacencyPairs[dir].Remove(child.tileId);
        }

        /*public List<int> GetChildren(WFCTile parent, int dir)
        {
            return parent.adjacencyPairs[dir];
        }*/
    }
}