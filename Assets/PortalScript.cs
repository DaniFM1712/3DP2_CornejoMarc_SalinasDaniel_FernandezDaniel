using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] PortalScript otherPortal;
    [SerializeField] Transform virtualPortal;
    [SerializeField] Camera cam;
    [SerializeField] Transform player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerLocalPos = virtualPortal.InverseTransformPoint(player.position);
        Vector3 otherCameraLocalPos = playerLocalPos;
        Vector3 otherCameraWorldPos = otherPortal.transform.TransformPoint(otherCameraLocalPos);

        Vector3 playerLocalForward = virtualPortal.InverseTransformDirection(player.forward);
        Vector3 otherCameraLocalForward = playerLocalForward;
        Vector3 otherCameraWorldForward = otherPortal.transform.TransformDirection(otherCameraLocalForward);


        otherPortal.cam.transform.position = otherCameraWorldPos;
        otherPortal.cam.transform.forward = otherCameraWorldForward;

        float distCameraPortal = (otherPortal.transform.position - otherPortal.cam.transform.position).magnitude;
        otherPortal.cam.nearClipPlane = distCameraPortal;
    }
}
