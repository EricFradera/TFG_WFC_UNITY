using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFC2DTile", order = 1, fileName = "WFC2dTile"), Serializable]
    public class WFC2DTile : WFCTile
    {
        // Tile id
        //public int tileId;
        //public Vector2 position;

        //Texture itself
        //public Texture2D texture2D;

        //Adjacency codes
        //public String id_up, id_right, id_down, id_left;


        public WFC2DTile()
        {
            this.adjacencyCodes = new String[4];
            this.adjacencyPairs = new List<int>[4];
        }

        //Adjacency arrays
        //public int[][] adjacency = new int[4][];
        //public List<int>[] arrayofValues=new List<int>[4];
        private enum IndexDirection
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

        //public List<int> up;
        //public List<int> right;
        //public List<int> down;
        //public List<int> left;
        
        public int GetInverse(int indexDirection)
        {
            switch ((IndexDirection)indexDirection)
            {
                case IndexDirection.UP:
                    return (int)IndexDirection.DOWN;
                case IndexDirection.RIGHT:
                    return (int)IndexDirection.LEFT;
                case IndexDirection.DOWN:
                    return (int)IndexDirection.UP;
                case IndexDirection.LEFT:
                    return (int)IndexDirection.RIGHT;
                default: return -1;
            }
        }
    }
}