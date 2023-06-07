using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;
using Vector2 = System.Numerics.Vector2;

public class Node2dComponent : NodeTileComponent
{
    VisualElement container;
    Image previewImage;

    public Node2dComponent(WFC2DTile tile) : base(tile)
    {
        portNames = new[] { "up", "right", "down", "left" };
        for (int i = 0; i < tile.Getdim(); i++)
        {
            CreateOutputPort(i);
        }

        CreateInputPort();

        ImageView();
    }
    

    private void ImageView()
    {
        WFC2DTile imageTile = (WFC2DTile)tile;
        if (imageTile.tileTexture.Length == 0) imageTile.InitDataStructures();
        imageTile.tileTexture[0] ??= Texture2D.whiteTexture;

        container = new VisualElement
        {
            name = "Parent Container",
            pickingMode = PickingMode.Position,
            style = { overflow = Overflow.Visible },
        };
        previewImage = new Image
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