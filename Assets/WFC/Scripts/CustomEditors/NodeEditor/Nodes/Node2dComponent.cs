using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;
using Vector2 = System.Numerics.Vector2;

public class Node2dComponent : NodeTileComponent
{
    public Node2dComponent(WFC2DTile tile) : base(tile)
    {
        portNames = new[] { "up", "right", "down", "left" };
        for (int i = 0; i < tile.Getdim(); i++)
        {
            CreateOutputPort(i);
        }

        CreateInputPort();

        ImageView(0);
    }

    // to update the image https://docs.unity3d.com/Manual/UIE-bind-custom-control.html
    private void ImageView(int tileSetIndex)
    {
        WFC2DTile imageTile = (WFC2DTile)this.tile;
        if (imageTile.tileTexture.Length==0)  imageTile.InitDataStructures();
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
            image = imageTile.tileTexture[tileSetIndex]
        };
        container.style.height = new StyleLength(120);
        previewImage.StretchToParentSize();
        container.contentContainer.Add(previewImage);
        Add(container);
    }
}