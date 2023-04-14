using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class InputCodeData : ScriptableObject
{
    public string uid;
    public nodeData nodeData;

    public abstract void Init();

    protected void GenerateUid() => uid = GUID.Generate().ToString();
}