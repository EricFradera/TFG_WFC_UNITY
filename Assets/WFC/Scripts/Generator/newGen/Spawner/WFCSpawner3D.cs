using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;
using WFC;
using Object = UnityEngine.Object;

public class WFCSpawner3D : WFCSpawnerAbstact
{
    private GameObject[,] gameObjectArray;


    public WFCSpawner3D(Transform transform, int lineCount, float m_gridSize, float m_gridExtent) : base(transform,
        lineCount, m_gridSize, m_gridExtent)
    {
        gameObjectArray = new GameObject[base.lineCount, lineCount];
    }

    public override void spawnTiles(ITopoArray<WFCTile> res, bool useRotations, int tileSetIndex)
    {
        WFC3DTile tempTile;
        if (lineCount % 2 == 0) lineCount++;
        lineCount--;
        var halfLines = lineCount / 2;

        for (int k = 0; k < lineCount; k++)
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < lineCount; j++)
                {
                    float xCoord = (i - halfLines) * m_gridSize + (m_gridSize / 2);
                    float zCoord = (j - halfLines) * m_gridSize + (m_gridSize / 2);
                    float yCoord = (k - halfLines) * m_gridSize + (m_gridSize / 2);
                    tempTile = (WFC3DTile)res.Get(i, k, j);
                    if (tempTile.tileVisuals[tileSetIndex] is null)
                    {
                        throw new Exception("GameObject is not set");
                    }

                    gameObjectArray[i, j] = Object.Instantiate(tempTile.tileVisuals[tileSetIndex],
                        new Vector3(xCoord, yCoord, zCoord),
                        transform.rotation);

                    switch (tempTile.rotationAxis)
                    {
                        case 1:
                            gameObjectArray[i, j].transform
                                .Rotate(new Vector3(tempTile.rotationModule * -90, 0, 0)); 
                            break;
                        case 2:
                            gameObjectArray[i, j].transform.Rotate(new Vector3(0, tempTile.rotationModule * -90, 0));
                            break;
                        case 3:
                            gameObjectArray[i, j].transform.Rotate(new Vector3(0, 0, tempTile.rotationModule * -90));
                            break;
                    }

                    gameObjectArray[i, j].transform.localScale = new Vector3(m_gridSize, m_gridSize, m_gridSize);
                    gameObjectArray[i, j].transform.parent = transform;
                }
            }
        }
    }

    public override void ClearPreviousIteration()
    {
        if (gameObjectArray == null)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
        else
        {
            foreach (var tile in gameObjectArray)
            {
                Object.DestroyImmediate(tile);
            }
        }
    }
}