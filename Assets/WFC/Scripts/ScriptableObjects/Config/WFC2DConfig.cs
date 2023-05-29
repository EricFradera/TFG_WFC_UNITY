using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFC2DConfig", order = 2, fileName = "WFC2DConfig"), Serializable]
public class WFC2DConfig : WFCConfig
{
    public override WFCManager createWFCManager()
    {
        return new WFC2DManager(this);
    }

    public override WFCSpawnerAbstact CreateSpawner(Transform transform, int lineCount, float m_gridSize, float m_gridExtent)
    {
        
        return new WFCSpawner2D(transform,
            lineCount, m_gridSize, m_gridExtent);
    }
}