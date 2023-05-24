using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFC3DConfig", order = 3, fileName = "WFC3DConfig"), Serializable]
public class WFC3DConfig : WFCConfig
{
    public override WFCManager createWFCManager()
    {
        return new WFC3DManager(this);
    }
}