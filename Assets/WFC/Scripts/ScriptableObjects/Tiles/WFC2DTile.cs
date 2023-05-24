using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFC2DTile", order = 1, fileName = "WFC2dTile"), Serializable]
    public class WFC2DTile : WFCTile
    {
        [JsonIgnore] public Texture2D tileTexture;

        [Serializable]
        public struct rotationValues
        {
            public string RotationName;
            public rotation degrees;
        }

        [Header(("Rotations"))] public rotationValues[] rotationList;

        public enum rotation
        {
            Degrees90,
            Degrees180,
            Degrees270
        };

        public WFC2DTile()
        {
            dim = 4;
            this.adjacencyCodes = new InputCodeData[dim];
            this.adjacencyPairs = new List<WFCTile>[dim];
        }

        private enum IndexDirection
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

        public override int GetInverse(int indexDirection)
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

        public override List<bool> GetListOfRotations()
        {
            var tempList = rotationList.DistinctBy(item => item.degrees);
            var listOfRotations = new List<bool>()
            {
                false,false,false
            };
            foreach (var item in tempList)
            {
                listOfRotations[(int)item.degrees] = true;
            }
            return listOfRotations;
        }

        public override List<WFCTile> generateTilesFromRotations()
        {
            List<WFCTile> rotationTiles = new List<WFCTile>();
            
            
            
            
            
            
            
            return rotationTiles;
        }

        public override WFCTile fillData(WFCTile data, int rot)
        {
            data.tileName = this.tileName + (90 * rot);
            data.tileId = this.tileId + (90 * rot);

            InputCodeData[] tempCodes = this.adjacencyCodes;
            tempCodes = new[]
                { this.adjacencyCodes[4], this.adjacencyCodes[1], this.adjacencyCodes[2], this.adjacencyCodes[3] };


            return data;
        }
    }
}