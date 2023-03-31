using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFC2DTile",order = 1,fileName = "WFC2dTile"),Serializable]
    public class WFC2DTile : ScriptableObject
    {
        // Tile id
        public int tileId;
        //Texture itself
        public Texture2D texture2D;
        //Adjacency codes
        public String id_up, id_right,id_down ,id_left;
        //Adjacency arrays
        //public int[][] adjacency = new int[4][];
        //public List<int>[] arrayofValues=new List<int>[4];
        
        public List<int> up;
        public List<int> right;
        public List<int> down;
        public List<int> left;

        public WFC2DTile(int tileID, Texture2D texture, int adjUp,int adjRight,int adjDown,int adjLeft)
        {
            this.tileId = tileID;
            this.texture2D = texture;
        }
    }
}
