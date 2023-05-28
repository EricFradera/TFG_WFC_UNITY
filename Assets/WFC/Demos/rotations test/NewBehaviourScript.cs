using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<int> adjacencyCodes = new List<int>();
        adjacencyCodes.Add(0);
        adjacencyCodes.Add(1);
        adjacencyCodes.Add(2);
        adjacencyCodes.Add(3);
        rotate(adjacencyCodes.ToArray(), numOfRot);
    }

    public int numOfRot;

    private void rotate(int[] adjacencyCodes, int rotation)
    {
        var listLenght = adjacencyCodes.Length;
        rotation = rotation % listLenght;

        int[] tempArray = new int[listLenght];
        for (int i = 0; i < listLenght; i++)
        {
            tempArray[(i + rotation) % listLenght] = adjacencyCodes[i];
        }

        Debug.Log("Result for " + numOfRot + " is " + tempArray[0] + "," + tempArray[1] + "," + tempArray[2] +
                  "," + tempArray[3]);
    }
}