using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionScript : MonoBehaviour
{
    [SerializeField] LaserScript laser;
    [SerializeField] Transform origin;
    bool wasActivated = false;

    public void ActivateReflection(bool active)
    {
        Debug.Log("PORTAL LASER");
        wasActivated = true;
        
    }

    private void LateUpdate()
    {
        laser.activate(wasActivated);
        laser.transform.position = origin.position;
        laser.transform.rotation = origin.rotation;
        wasActivated = false;
        
    }
}
