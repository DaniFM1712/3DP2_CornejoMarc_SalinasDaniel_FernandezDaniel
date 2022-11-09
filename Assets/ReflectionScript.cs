using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionScript : MonoBehaviour
{
    [SerializeField] LaserScript laser;
    bool wasActivated = false;

    public void ActivateReflection(bool active)
    {
        wasActivated = true;
        
    }

    private void LateUpdate()
    {
        laser.activate(wasActivated);
        wasActivated = false;
        
    }
}
