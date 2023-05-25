using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StringCodeData : InputCodeData
{
    public override nodeData Init()
    {
        code="String code";
        GenerateUid();
        nodeData = ScriptableObject.CreateInstance<nodeData>();
        return nodeData;
    }
}