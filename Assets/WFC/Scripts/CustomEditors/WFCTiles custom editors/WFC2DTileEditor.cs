using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;

#if false
namespace WFCEditor
{
    //[CustomEditor(typeof(WFC2DTile))]
    public class Wfc2DTileEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var foldout = new Foldout()
            {
                viewDataKey = "WFC2DTileFullInspector", text = "Full inspector"
            };
            InspectorElement.FillDefaultInspector(foldout, serializedObject, this);
            root.Add(foldout);
            return root;
        }
    }
}
#endif