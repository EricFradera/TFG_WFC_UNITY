using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFC3DTile", order = 2, fileName = "WFC3dTile"), Serializable]
    public class WFC3DTile : WFCTile
    {
        public WFC3DTile()
        {
            this.adjacencyCodes = new InputCodeData[6];
            this.adjacencyPairs = new List<WFCTile>[6];
        }

        public void InitDataStructures()
        {
            this.adjacencyCodes = new InputCodeData[6];
            for (int i = 0; i < 6; i++) this.adjacencyPairs[i] = new List<WFCTile>();
        }

        private enum IndexDirection
        {
            UP,
            RIGHT,
            DOWN,
            LEFT,
            ZUP,
            ZDOWN
        }

        public override int GetInverse(int indexDirection)
        {
            return (IndexDirection)indexDirection switch
            {
                IndexDirection.UP => (int)IndexDirection.DOWN,
                IndexDirection.RIGHT => (int)IndexDirection.LEFT,
                IndexDirection.DOWN => (int)IndexDirection.UP,
                IndexDirection.LEFT => (int)IndexDirection.RIGHT,
                IndexDirection.ZUP => (int)IndexDirection.ZDOWN,
                IndexDirection.ZDOWN => (int)IndexDirection.ZUP,
                _ => -1
            };
        }

        public override List<bool> GetListOfRotations()
        {
            //TODO fill with rotations
            var listOfRotations = new List<bool>()
            {
            };
            return listOfRotations;
        }

        public override WFCTile fillData(WFCTile data, int rot)
        {
            throw new NotImplementedException();
        }
    }
}