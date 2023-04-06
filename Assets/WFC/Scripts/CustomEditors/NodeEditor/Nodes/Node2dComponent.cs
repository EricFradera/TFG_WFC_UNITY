using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;

public class Node2dComponent : NodeComponent
{
    public Node2dComponent(WFC2DTile tile)
    {
        this.tile = tile;
        this.viewDataKey = tile.tileId;
        this.title = tile.tileName;
        portNames = new[] { "up", "right", "down", "left" };
        input = new Port[4];
        output = new Port[4];
        style.left = tile.nodeData.position.x;
        style.top = tile.nodeData.position.y;
        for (int i = 0; i < 4; i++)
        {
            CreateInputPort(i);
            CreateOutputPort(i);
        }

        ImageView();
        Label titleLabel = this.Q<Label>("title-label");
        titleLabel.bindingPath = "tileName";
        titleLabel.Bind(new SerializedObject(tile));
    }

    // to update the image https://docs.unity3d.com/Manual/UIE-bind-custom-control.html
    private void ImageView()
    {
        WFC2DTile textureSource = (WFC2DTile)this.tile;

        textureSource.tileTexture ??= Texture2D.whiteTexture;
        var container = new VisualElement
        {
            name = " Parent Container",
            pickingMode = PickingMode.Position,
            style = { overflow = Overflow.Visible },
        };
        var previewImage = new Image
        {
            name = "Preview",
            pickingMode = PickingMode.Ignore,
            image = textureSource.tileTexture
        };

        container.style.height = new StyleLength(120);
        previewImage.StretchToParentSize();
        container.contentContainer.Add(previewImage);
        Add(container);
    }
}