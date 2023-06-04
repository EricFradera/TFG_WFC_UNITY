using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StringCodeData : InputCodeData
{
    //[Rename("No symmetric")]
    //public bool Nonsymmetric;
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

    //Non-working asymetric rotation system
    public override InputCodeData genRotCode(int rot,int axis)
    {
        var tempCode = CreateInstance<StringCodeData>();
        tempCode.uid = this.uid + "_" + (rot * 90);
        tempCode.socketName = this.socketName + "_" + (rot * 90);
        tempCode.nodeData = CreateInstance<nodeData>();
        tempCode.socketCodes = new List<string>();
        foreach (var code in socketCodes)
        {
            tempCode.socketCodes.Add(code);
        }

        //if (Nonsymmetric) tempCode.socketCodes.Add("AXIS"+axis+"_v"+rot);

        /*var listLenght = this.socketCodes.Count;
        rot = rot % listLenght;
        List<string> tempSocketCodes = new List<string>(listLenght);
        tempSocketCodes.AddRange(this.socketCodes);
        for (int i = 0; i < listLenght; i++)
        {
            tempSocketCodes[(i + rot) % listLenght] = this.socketCodes[i];
        }

        tempCode.socketCodes = tempSocketCodes;*/

        return tempCode;
    }
}