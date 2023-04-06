using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

public class WFCGenerator : MonoBehaviour
{
    //Maybe it's useless.
    public GeneratorModes mode;
    private WFCConfig config;


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