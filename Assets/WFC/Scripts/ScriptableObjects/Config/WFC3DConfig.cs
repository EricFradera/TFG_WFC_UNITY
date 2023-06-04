using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFC3DConfig", order = 3, fileName = "WFC3DConfig"), Serializable]
public class WFC3DConfig : WFCConfig
{
    public override WFCManager createWFCManager()
    {
        return new WFC3DManager(this);
    }

    public override WFCSpawnerAbstact CreateSpawner(Transform transform, int lineCount, float m_gridSize,
        float m_gridExtent)
    {
        return new WFCSpawner3D(transform, lineCount, m_gridSize, m_gridExtent);
    }

    public override WFCAbstractProc CreateProcessor(List<WFCTile> listOfTiles, WFCManager manager)
    {
        var processor = new WFC3DProc(listOfTiles, manager);
        processor.setUseRotations(useRotations);
        return processor;
    }
}