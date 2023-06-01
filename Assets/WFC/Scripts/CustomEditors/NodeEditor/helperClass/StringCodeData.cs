using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StringCodeData : InputCodeData
{
    public override nodeData Init()
    {
        socketName = "String code";
        socketCodes = new List<string>();
        GenerateUid();
        nodeData = ScriptableObject.CreateInstance<nodeData>();
        return nodeData;
    }

    public override string getCode(bool inverse)
    {
        if (!inverse) return String.Join("_", this.socketCodes);
        var copyString = Enumerable.Reverse(socketCodes).ToList();
        return String.Join("_", copyString);

    }
}