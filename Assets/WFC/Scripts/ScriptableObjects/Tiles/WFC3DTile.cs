using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFC3DTile", order = 2, fileName = "WFC3dTile"), Serializable]
    public class WFC3DTile : WFCTile
    {
        [JsonIgnore] public Texture2D previewTexture2D;
        
        /*[Serializable]
        public struct rotationValues
        {
            public string RotationName;
            public rotation x;
            public rotation y;
            public rotation z;
        }

        [Header(("Rotations"))] public rotationValues[] rotationList;

        public enum rotation
        {
            NoRotation,
            Degrees90,
            Degrees180,
            Degrees270
        };*/

        public WFC3DTile()
        {
            dim = 6;
            this.adjacencyCodes = new InputCodeData[dim];
            this.adjacencyPairs = new List<WFCTile>[dim];
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

        public override List<WFCTile> getRotationTiles()
        {
            throw new NotImplementedException();
        }

        protected override WFCTile copyForRotation(int rot)
        {
            throw new NotImplementedException();
        }


       
    }
}