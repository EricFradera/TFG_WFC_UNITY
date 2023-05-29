using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;

namespace WFC
{
    [System.Serializable]
    public abstract class WFCConfig : ScriptableObject
    {
        public string configurationName;
        public int configurationID;
        public List<WFCTile> wfcTilesList = new List<WFCTile>();
        public List<InputCodeData> nodeHelpers;
        [Rename("Use rotations with tiles")]
        public bool useRotations;

        public abstract WFCManager createWFCManager();
        public abstract WFCSpawnerAbstact CreateSpawner(Transform transform, int lineCount, float m_gridSize, float m_gridExtent);
    }
}