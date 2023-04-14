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
        editor = component switch
        {
            Node2dComponent node2dComponent => Editor.CreateEditor(node2dComponent.tile),
            ColorNode colorNode => Editor.CreateEditor(colorNode.codeData),
            _ => editor
        };
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }
}