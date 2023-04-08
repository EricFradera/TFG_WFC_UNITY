using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

public class WFCGenerator : MonoBehaviour
{
    //Maybe it's useless.
    public GeneratorModes mode;
    private WFCConfig config;
    public float m_gridSize;
    public int m_gridExtent;
    public Color lineColor;


    public enum GeneratorModes
    {
        WFC2DMODE,
        WFC3DMODE,
        WFCHEXMODE,
        WFCGRAPHMODE
    }

    private void Generate()
    {
        ClearPreviousIteration();
    }

    private void ClearPreviousIteration()
    {
    }
}