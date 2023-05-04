using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;
using Object = System.Object;

public class testscript : MonoBehaviour
{
    public void testRel(Object test)
    {
        WFCConfig config = test as WFCConfig;
        foreach (var tile in config.wfcTilesList)
        {
            Debug.Log("test");
            for (int i = 0; i < 4; i++)
            {
                if (tile.adjacencyCodes[i] is null)
                    Debug.Log("In space " + i + " the realationships havent been created yet");
                else
                    Debug.Log("In space " + i + " there's:" + tile.adjacencyCodes[i].uid);
            }
        }
    }
}