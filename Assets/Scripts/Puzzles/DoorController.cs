using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public void ActivateDoor(bool opened)
    {
        if (opened)
        {
            Debug.Log("OPEN DOOR");
            //activate open animation
        }

        else
        {
            Debug.Log("CLOSE DOOR");
            //activate close animation
        }

    }
}
