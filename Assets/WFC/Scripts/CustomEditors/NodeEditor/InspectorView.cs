using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>
    {
    }

    private Editor editor;

    public InspectorView()
    {
    }

    public void UpdateSelection(NodeComponent component)
    {
        Clear();
        UnityEngine.Object.DestroyImmediate(editor);
        if (component is Node2dComponent comp)
        {
            editor = Editor.CreateEditor(comp.tile);
        }

        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }
}