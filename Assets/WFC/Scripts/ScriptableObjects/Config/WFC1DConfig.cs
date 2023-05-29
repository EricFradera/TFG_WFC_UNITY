using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFC1DConfig", order = 1, fileName = "WFC1DConfig"), Serializable]

public class WFC1DConfig :WFCConfig
{
    public override WFCManager createWFCManager()
    {
        return new WFC1DManager(this);
    }

    public override WFCSpawnerAbstact CreateSpawner(Transform transform, int lineCount, float m_gridSize, float m_gridExtent)
    {
        throw new NotImplementedException();
    }

    
}
