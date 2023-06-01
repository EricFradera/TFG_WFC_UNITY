using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;

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

    public override void spawnTiles(ITopoArray<WFCTile> result, bool useRotations)
    {
        var halfCount = m_gridExtent / m_gridSize;
        for (int i = 0; i < lineCount; i++)
        {
            var primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
            primitive.transform.position = new Vector3((i - lineCount / 2) * halfCount + (halfCount / 2), 0, 0);
            primitive.transform.Rotate(new Vector3(90f, 0, 0));
            primitive.GetComponent<MeshRenderer>().material = genMat((WFC1DTile)result.Get(i, 0));
            primitive.transform.localScale = new Vector3(m_gridExtent / m_gridSize, m_gridExtent / m_gridSize, 1);
            primitive.transform.parent = this.transform;
        }
    }
    
    private Material genMat(WFC1DTile tile)
    {
        if (materials.ContainsKey(tile.tileId)) return materials[tile.tileId];
        var mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"))
        {
            mainTexture = tile.tileTexture
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