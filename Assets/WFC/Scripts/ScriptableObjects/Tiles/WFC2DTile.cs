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
    public class WFC2DTile : WFCTile
    {
        [JsonIgnore] public Texture2D tileTexture;

        [Serializable]
        public struct rotationValues
        {
            public rotation degrees;
        }

        [Header(("Rotations"))] public rotationValues[] rotationList;

        public enum rotation
        {
            Degrees90,
            Degrees180,
            Degrees270,
        };

        public WFC2DTile()
        {
            dim = 4;
            this.adjacencyCodes = new InputCodeData[dim];
            this.adjacencyPairs = new List<WFCTile>[dim];
            this.GeneratedAdjacencyPairs = new List<WFCTile>[dim];
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


        public override List<WFCTile> getRotationTiles()
        {
            List<WFCTile> res = new List<WFCTile>();
            if (rotationList.Length == 0) return res;
            foreach (var rotation in rotationList)
            {
                res.Add(copyForRotation((int)rotation.degrees + 1));
            }

            return res;
        }

        protected override WFCTile copyForRotation(int rot)
        {
            var tempTile = CreateInstance<WFC2DTile>();
            tempTile.tileName = tileName + "_" + (90 * rot);
            tempTile.tileId = tileId + "_" + (90 * rot);
            tempTile.adjacencyCodes = rotationHelper(rot);
            tempTile.adjacencyPairs = new List<WFCTile>[dim];
            tempTile.nodeData = nodeData;
            tempTile.tileVisuals = tileVisuals;
            tempTile.tileTexture = tileTexture;
            tempTile.rotationModule = rot;
            return tempTile;
        }

        private InputCodeData[] rotationHelper(int rotation)
        {
            var listLenght = adjacencyCodes.Length;
            rotation = rotation % listLenght;

            InputCodeData[] tempArray = new InputCodeData[listLenght];
            for (int i = 0; i < listLenght; i++)
            {
                tempArray[(i + rotation) % listLenght] = adjacencyCodes[i];
            }


            return tempArray;
        }
    }
}