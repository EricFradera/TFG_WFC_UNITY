using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFCConfig", order = 3, fileName = "WFCConfig"), Serializable]
    public class WFCConfig : ScriptableObject
    {
        public String configurationName;
        public int configurationID;
        public List<WFC2DTile> wfc2DTilesList;
    }
}