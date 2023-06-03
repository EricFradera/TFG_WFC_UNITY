using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;

public class WFCSpawnerHex : WFCSpawnerAbstact
{
    private Dictionary<string, Material> materials = new Dictionary<string, Material>();
    private GameObject[,] gameObjectArray;

    public WFCSpawnerHex(Transform transform, int lineCount, float m_gridSize, float m_gridExtent) : base(transform,
        lineCount, m_gridSize, m_gridExtent)
    {
        gameObjectArray = new GameObject[base.lineCount, lineCount];
    }

    public override void spawnTiles(ITopoArray<WFCTile> result, bool useRotations)
    {
        GameObject tempGameObject;
        for (var x = 0; x < lineCount; x++)
        for (var z = 0; z < lineCount; z++)
        {
            tempGameObject =  GameObject.CreatePrimitive(PrimitiveType.Quad);
            tempGameObject.transform.Rotate(new Vector3(90f, 0, 0));
            tempGameObject.GetComponent<MeshRenderer>().material = genMat((WFCHEXTile)result.Get(x, z));
            tempGameObject.transform.position = z % 2 == 0
                ? new Vector3(x * 1, 0, z * 1)
                : new Vector3(x * 1 + 1 / 2, 0, 1);
        }
    }

    private Material genMat(WFCHEXTile tile)
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