using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCodeData : InputCodeData
{
    public Color color;

    public override void Init()
    {
        GenerateUid();
        nodeData = new nodeData();
    }
}