using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#if false


[CustomPropertyDrawer(typeof(WFCTile))]
public class TileManagerEditor : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var root = new VisualElement();
        //WFCTile tile = property.objectReferenceValue as object as WFCTile;
        //Debug.Log(tile.tileId);
        
        return root;
    }
}

/*
 public void UpdateSelection(NodeComponent component)
    {
        Clear();
        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(component.tile);
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }
*/
#endif