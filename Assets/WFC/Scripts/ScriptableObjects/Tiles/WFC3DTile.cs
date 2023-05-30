using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace WFC
{
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
            YPLUS,
            YMINUS,
            XPLUS,
            XMINUS,
            ZPLUS,
            ZMINUS
        }

        public override int GetInverse(int indexDirection)
        {
            return (IndexDirection)indexDirection switch
            {
                IndexDirection.YPLUS => (int)IndexDirection.YMINUS,
                IndexDirection.YMINUS => (int)IndexDirection.YPLUS,
                IndexDirection.XPLUS => (int)IndexDirection.XMINUS,
                IndexDirection.XMINUS => (int)IndexDirection.XPLUS,
                IndexDirection.ZPLUS => (int)IndexDirection.ZMINUS,
                IndexDirection.ZMINUS => (int)IndexDirection.ZPLUS,
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