using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFC2DTile", order = 1, fileName = "WFC2dTile"), Serializable]
    public class WFC2DTile : WFCTile
    {
        public WFC2DTile()
        {
            this.adjacencyCodes = new String[4];
            this.adjacencyPairs = new List<int>[4];
        }

        private enum IndexDirection
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

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