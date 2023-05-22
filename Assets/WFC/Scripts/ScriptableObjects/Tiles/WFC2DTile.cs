using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFC2DTile", order = 1, fileName = "WFC2dTile"), Serializable]
    public class WFC2DTile : WFCTile
    {
        public List<string> test;
        [JsonIgnore] public Texture2D tileTexture;
        public string testName;

        [Header(("Rotations"))] [Rename("90 degrees rotation")]
        public bool oneRotation;

        [Rename("180 degrees rotation")] public bool twoRotations;
        [Rename("270 degrees rotation")] public bool threeRotations;


        public WFC2DTile()
        {
            this.adjacencyCodes = new InputCodeData[4];
            this.adjacencyPairs = new List<WFCTile>[4];
            test = new List<string>();
        }


        public void InitDataStructures()
        {
            this.adjacencyCodes = new InputCodeData[4];
            for (int i = 0; i < 4; i++) this.adjacencyPairs[i] = new List<WFCTile>();
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
            var listOfRotations = new List<bool>()
            {
                oneRotation, twoRotations, threeRotations
            };
            return listOfRotations;
        }

        public override WFCTile fillData(WFCTile data, int rot)
        {
            data.tileName = this.tileName + (90 * rot);
            data.tileId = this.tileId + (90 * rot);
            //
            InputCodeData[] tempCodes = this.adjacencyCodes;
            tempCodes = new[]
                { this.adjacencyCodes[4], this.adjacencyCodes[1], this.adjacencyCodes[2], this.adjacencyCodes[3] };


            return data;
        }
    }
}