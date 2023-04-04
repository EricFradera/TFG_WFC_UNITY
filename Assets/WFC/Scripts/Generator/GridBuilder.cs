using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;

namespace WFC
{
    [ExecuteInEditMode]
    public class GridBuilder : MonoBehaviour
    {
        //GameObject Have to be 1x1x1
        [SerializeField] public int size;
        private GameObject[,] gameObjectArray;
        [SerializeField] private List<GameObject> circuitComponents;
        private debro_test test;
        private ITopoArray<string> result;
        private WFCConfig wfcConfig;
        private Dictionary<string, GameObject> gameObjectsDictionary;

        public void Genarate(WFCConfig wfcConfig)
        {
            destroyOldIteration();
            genDict();
            this.wfcConfig = wfcConfig;
            test = new debro_test(wfcConfig.wfcTilesList);
            result = test.runWFC(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    gameObjectArray[i, j] = Instantiate(getObj(i, j), new Vector3(i, 0, j),
                        transform.rotation);
                    gameObjectArray[i, j].transform.parent = gameObject.transform;
                }
            }
        }

        private void destroyOldIteration()
        {
            if (gameObjectArray != null)
            {
                foreach (var tile in gameObjectArray)
                {
                    DestroyImmediate(tile);
                }
            }

            gameObjectArray = new GameObject[size, size];
        }

        private void genDict()
        {
            gameObjectsDictionary = new Dictionary<string, GameObject>();
            for (int k = 0; k < wfcConfig.wfcTilesList.Count; k++)
            {
                gameObjectsDictionary.Add(wfcConfig.wfcTilesList[k].tileId, circuitComponents[k]);
            }
        }

        private GameObject getObj(int i, int j)
        {
            string res = result.Get(i, j);
            return gameObjectsDictionary[res];
        }
    }
}