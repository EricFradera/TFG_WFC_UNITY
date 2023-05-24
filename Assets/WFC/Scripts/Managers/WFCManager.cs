using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

public abstract class WFCManager
{
    private WFCConfig wfcConfig;
    
    
    //abstract methods
    public abstract WFCTile CreateNodeTile();
    public abstract EditorManager getEditorManager();
    public abstract IWFCSpawner GetWfcSpawner();
}
