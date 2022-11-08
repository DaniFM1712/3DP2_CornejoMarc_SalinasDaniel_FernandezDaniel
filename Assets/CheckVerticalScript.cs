using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CheckVerticalScript : MonoBehaviour
{
    [SerializeField] float angleThreshhold;
    [SerializeField] UnityEvent<bool> isUpChanged;
    bool wasUp = false;

    // Update is called once per frame
    void Update()
    {
        bool isUp = Mathf.Abs(Vector3.Angle(Vector3.up, transform.up)) < angleThreshhold;
        if(isUp != wasUp)
        {
            isUpChanged.Invoke(isUp);
        }
        wasUp = isUp;

         

    }
}
