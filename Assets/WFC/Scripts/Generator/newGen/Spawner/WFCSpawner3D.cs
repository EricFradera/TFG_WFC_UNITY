using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;

public class WFCSpawner3D : WFCSpawnerAbstact
{
    private GameObject[,] gameObjectArray;


    public WFCSpawner3D(Transform transform, int lineCount, float m_gridSize, float m_gridExtent) : base(transform,
        lineCount, m_gridSize, m_gridExtent)
    {
        gameObjectArray = new GameObject[base.lineCount, lineCount];
    }

    public override void spawnTiles(ITopoArray<WFCTile> res, bool useRotations)
    {
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
                    gameObjectArray[i, j] = Object.Instantiate(res.Get(i, k,j).tileVisuals,
                        new Vector3(xCoord, yCoord, zCoord),
                        transform.rotation);
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