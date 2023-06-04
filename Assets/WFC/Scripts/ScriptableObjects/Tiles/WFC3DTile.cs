using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace WFC
{
    public class WFC3DTile : WFCTile
    {
        [JsonIgnore] public Texture2D previewTexture2D;

        [Serializable]
        public struct rotationValues
        {
            public rotation degrees;
            public Axis axisOfRotation;
        }

        [Header(("Rotations"))] public rotationValues[] rotationList;
        public int rotationAxis = 0;

        public enum rotation
        {
            Degrees90,
            Degrees180,
            Degrees270
        };

        public enum Axis
        {
            X,
            Y,
            Z
        }

        private IndexDirection[,] rotationOrder = new IndexDirection[,]
        {
            { IndexDirection.ZPLUS, IndexDirection.XPLUS, IndexDirection.ZMINUS, IndexDirection.XMINUS }, // X Rotation
            { IndexDirection.YPLUS, IndexDirection.ZPLUS, IndexDirection.YMINUS, IndexDirection.ZMINUS }, // Y Rotation
            { IndexDirection.YPLUS, IndexDirection.XMINUS, IndexDirection.YMINUS, IndexDirection.XPLUS } // Z rotation
        };

        public WFC3DTile()
        {
            dim = 6;
            this.adjacencyCodes = new InputCodeData[dim];
            this.adjacencyPairs = new List<WFCTile>[dim];
            this.GeneratedAdjacencyPairs = new List<WFCTile>[dim];
        }

        private enum IndexDirection
        {
            XPLUS,
            XMINUS,
            YPLUS,
            YMINUS,
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
            List<WFCTile> res = new List<WFCTile>();
            if (rotationList.Length == 0) return res;
            foreach (var rotation in rotationList)
            {
                res.Add(copyForRotation((int)rotation.degrees + 1, (int)rotation.axisOfRotation + 1)); //prone to error
            }

            return res;
        }

        protected override WFCTile copyForRotation(int rot, int axis)
        {
            var tempTile = CreateInstance<WFC3DTile>();
            tempTile.tileName = tileName + "_" + axis + "_" + (90 * rot);
            tempTile.tileId = tileId + "_" + axis + (90 * rot);
            tempTile.adjacencyCodes = rotationHelper(rot, axis);
            tempTile.adjacencyPairs = new List<WFCTile>[dim];
            tempTile.nodeData = nodeData;
            tempTile.tileVisuals = tileVisuals;
            tempTile.rotationModule = rot;
            tempTile.rotationAxis = axis;
            return tempTile;
        }

        private InputCodeData[] rotationHelper(int rotation, int axis) //Most important thing
        {
            var listLenght = 4;
            rotation = rotation % listLenght;
            InputCodeData[] rotationArray = new InputCodeData[listLenght];
            for (int i = 0; i < listLenght; i++) rotationArray[i] = adjacencyCodes[(int)rotationOrder[axis - 1, i]];


            InputCodeData[] tempArray = new InputCodeData[listLenght];
            for (int i = 0; i < listLenght; i++)
            {
                tempArray[(i + rotation) % listLenght] = rotationArray[i];
            }

            rotationArray = new InputCodeData[adjacencyCodes.Length];
            bool found;
            for (int i = 0; i < adjacencyCodes.Length; i++)
            {
                found = false;
                for (int j = 0; j < tempArray.Length; j++)
                {
                    if (i == (int)rotationOrder[axis - 1, j])
                    {
                        rotationArray[i] = tempArray[j];
                        found = true;
                    }
                }

                if (!found) rotationArray[i] = adjacencyCodes[i];
            }

            return rotationArray;
        }
        
    }
}