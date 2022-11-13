using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserButtonScript : MonoBehaviour
{
    [SerializeField] UnityEvent buttonPressed;

    public void Pressed()
    {
        Debug.Log("BUTTON PRESSED!");
        buttonPressed.Invoke();
    }
}
