using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;

public class Node1dComponent : NodeTileComponent
{
    public Node1dComponent(WFC1DTile tile) : base(tile)
    {
        portNames = new[] { "right", "left" };
        for (int i = 0; i < 2; i++)
        {
            CreateOutputPort(i);
        }

        CreateInputPort();
        ImageView();
    }

    // to update the image https://docs.unity3d.com/Manual/UIE-bind-custom-control.html
    private void ImageView()
    {
        WFC1DTile imageTile = (WFC1DTile)this.tile;
        if (imageTile.tileTexture.Length == 0) imageTile.InitDataStructures();
        imageTile.tileTexture[0] ??= Texture2D.whiteTexture;

        var container = new VisualElement
        {
            name = " Parent Container",
            pickingMode = PickingMode.Position,
            style = { overflow = Overflow.Visible },
        };
        var previewImage = new Image
        {
            name = "tileTexture",
            pickingMode = PickingMode.Ignore,
            image = imageTile.tileTexture[0]
        };
        container.style.height = new StyleLength(120);
        previewImage.StretchToParentSize();
        container.contentContainer.Add(previewImage);
        Add(container);
    }
}