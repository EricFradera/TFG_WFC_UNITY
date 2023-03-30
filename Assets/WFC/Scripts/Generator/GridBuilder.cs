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
        private ITopoArray<int> result;
        public List<WFC2DTile> adjacencyData;


        public void Genarate()
        {
            destroyOldIteration();
            test = new debro_test(adjacencyData);
            result = test.runWFC(size);
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    gameObjectArray[i, j] = Instantiate(circuitComponents[(result.Get(i, j))], new Vector3(i, 0, j),
                        transform.rotation);
                   // Debug.Log(result.Get(i,j));
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
    }
}