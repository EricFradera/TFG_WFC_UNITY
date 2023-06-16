using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;
using Object = UnityEngine.Object;

public class WFCSpawner1D : WFCSpawnerAbstact
{
    private Dictionary<string, Material> materials = new Dictionary<string, Material>();
    private GameObject[,] gameObjectArray;

    public WFCSpawner1D(Transform transform, int lineCount, float m_gridSize, float m_gridExtent) : base(transform,
        lineCount, m_gridSize, m_gridExtent)
    {
        this.lineCount = Mathf.RoundToInt(m_gridSize);
        gameObjectArray = new GameObject[this.lineCount, 0];
    }

    public override void spawnTiles(ITopoArray<WFCTile> result, bool useRotations, int tileSetIndex)
    {
        var halfCount = m_gridExtent / m_gridSize;
        WFC1DTile wfc1DTile;
        GameObject primitive;
        for (int i = 0; i < lineCount; i++)
        {
            wfc1DTile = (WFC1DTile)result.Get(i, 0);
            if (wfc1DTile.assetType == WFC1DTile.AssetType.useGameObject)
            {
                if (wfc1DTile.tileVisuals.Length <= tileSetIndex || tileSetIndex < 0)
                {
                    throw new Exception("Index provided is not contained in the array");
                }

                if (wfc1DTile.tileVisuals[tileSetIndex] is null)
                {
                    throw new Exception("GameObject is not set");
                }

                primitive = Object.Instantiate(wfc1DTile.tileVisuals[tileSetIndex],
                    new Vector3((i - lineCount / 2) * halfCount + (halfCount / 2), 0, 0),
                    wfc1DTile.tileVisuals[tileSetIndex].transform.rotation);
            }
            else
            {
                if (wfc1DTile.tileTexture.Length <= tileSetIndex || tileSetIndex < 0)
                {
                    throw new Exception("Index provided is not contained in the array");
                }

                if (wfc1DTile.tileTexture[tileSetIndex] is null)
                {
                    throw new Exception("GameObject is not set");
                }

                primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
                primitive.transform.position = new Vector3((i - lineCount / 2) * halfCount + (halfCount / 2), 0, 0);
                primitive.transform.Rotate(new Vector3(90f, 0, 0));
                primitive.GetComponent<MeshRenderer>().material = genMat(wfc1DTile, tileSetIndex);
            }

            primitive.transform.localScale = new Vector3(m_gridExtent / m_gridSize, m_gridExtent / m_gridSize, 1);
            primitive.transform.parent = this.transform;
        }
    }

    private Material genMat(WFC1DTile tile, int tileSetIndex)
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