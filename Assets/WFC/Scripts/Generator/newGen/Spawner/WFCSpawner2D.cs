using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;
using WFC;
using Object = UnityEngine.Object;

public class WFCSpawner2D : WFCSpawnerAbstact
{
    private Dictionary<string, Material> materials = new Dictionary<string, Material>();
    private GameObject[,] gameObjectArray;

    public WFCSpawner2D(Transform transform, int lineCount, float m_gridSize, float m_gridExtent) : base(transform,
        lineCount, m_gridSize, m_gridExtent)
    {
        gameObjectArray = new GameObject[base.lineCount, lineCount];
    }

    public override void spawnTiles(ITopoArray<WFCTile> result, bool useRotations, int tileSetIndex)
    {
        if (lineCount % 2 == 0) lineCount++;
        lineCount--;
        var halfLines = lineCount / 2;
        GameObject primitive;
        WFC2DTile wfc2DTile;

        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < lineCount; j++)
            {
                float xCoord = (i - halfLines) * m_gridSize + (m_gridSize / 2);
                float zCoord = (j - halfLines) * m_gridSize + (m_gridSize / 2);
                wfc2DTile = (WFC2DTile)result.Get(i, j);
                if (wfc2DTile.assetType == WFC2DTile.AssetType.useGameObject)
                {
                    if (wfc2DTile.tileVisuals.Length <= tileSetIndex || tileSetIndex < 0)
                    {
                        throw new Exception("Index provided is not contained in the array");
                    }

                    if (wfc2DTile.tileVisuals[tileSetIndex] is null)
                    {
                        throw new Exception("GameObject is not set");
                    }

                    primitive = Object.Instantiate(wfc2DTile.tileVisuals[tileSetIndex],
                        new Vector3(xCoord, 0, zCoord),
                        wfc2DTile.tileVisuals[tileSetIndex].transform.rotation);
                    primitive.transform.Rotate(new Vector3(0, (wfc2DTile.rotationModule) * -90, 0));
                    gameObjectArray[i, j] = primitive;
                    gameObjectArray[i, j].transform.localScale = new Vector3(m_gridSize, m_gridSize, m_gridSize);
                }
                else
                {
                    if (wfc2DTile.tileTexture.Length <= tileSetIndex || tileSetIndex < 0)
                    {
                        throw new Exception("Index provided is not contained in the array");
                    }

                    if (wfc2DTile.tileTexture[tileSetIndex] is null)
                    {
                        throw new Exception("GameObject is not set");
                    }

                    primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    primitive.transform.position = new Vector3(xCoord, 0, zCoord);
                    primitive.transform.Rotate(new Vector3(90f, 0, (wfc2DTile.rotationModule) * -90));
                    primitive.GetComponent<MeshRenderer>().material = genMat(wfc2DTile, tileSetIndex);
                    gameObjectArray[i, j] = primitive;
                    gameObjectArray[i, j].transform.localScale = new Vector3(m_gridSize, m_gridSize, 1);
                }


                gameObjectArray[i, j].transform.parent = this.transform;
            }
        }
    }

    private Material genMat(WFC2DTile tile, int tileSetIndex)
    {
        if (materials.ContainsKey(tile.tileId)) return materials[tile.tileId];
        var mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"))
        {
            mainTexture = tile.tileTexture[tileSetIndex]
        };
        materials.Add(tile.tileId, mat);
        return mat;
    }


    public override void ClearPreviousIteration()
    {
        materials.Clear();

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