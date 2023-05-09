using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StringCodeData : InputCodeData
{
    
    public override void Init()
    {
        code="String code";
        GenerateUid();
        nodeData = new nodeData();
    }
}