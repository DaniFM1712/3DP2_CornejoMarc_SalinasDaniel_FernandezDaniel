using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserButtonScript : MonoBehaviour
{
    [SerializeField] UnityEvent buttonPressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed()
    {
        Debug.Log("BUTTON PRESSED!");
        buttonPressed.Invoke();
    }
}
