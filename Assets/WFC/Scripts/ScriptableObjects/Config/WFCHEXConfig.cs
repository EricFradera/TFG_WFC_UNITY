using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFCHEXConfig", order = 4, fileName = "WFCHEXConfig"),
 Serializable]
public class WFCHexConfig : WFCConfig
{
    public override WFCManager createWFCManager()
    {
        return new WFCHEXManager(this);
    }
}