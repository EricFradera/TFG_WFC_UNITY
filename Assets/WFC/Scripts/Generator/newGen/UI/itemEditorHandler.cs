using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//[CustomPropertyDrawer(typeof(WFCTile))]
public class itemEditorHandler : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var root = new VisualElement();
        root.Add(new PropertyField(property.serializedObject.FindProperty("tileName")));

        // Create property fields.

        //var nameField = new PropertyField(property.FindPropertyRelative("tileName"), "This is the tile Name");

        // Add fields to the container.
        //container.Add(nameField);
        
        
        
        Debug.Log("lol");
        

        return new PropertyField(property);
    }
}
