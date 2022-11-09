using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunScript : MonoBehaviour
{
    [SerializeField] GameObject previewPortal;
    [SerializeField] GameObject bluePortal;
    [SerializeField] GameObject orangePortal;
    [SerializeField] Camera cam;
    [SerializeField] float maxShootDistance = float.MaxValue;
    [SerializeField] LayerMask portalMask;
    bool previewActive = false;
    bool portalToActivate = false;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            portalToActivate = true;
            previewActive = MovePreview();
        }
        else if (Input.GetMouseButton(1))
        {
            portalToActivate = false;
            previewActive = MovePreview();
        }
        else
        {
            if (previewActive)
            {
                if (portalToActivate)
                {
                    bluePortal.SetActive(true);
                    bluePortal.transform.position = previewPortal.transform.position;
                    bluePortal.transform.rotation = previewPortal.transform.rotation;
                }
                else
                {
                    orangePortal.SetActive(true);
                    orangePortal.transform.position = previewPortal.transform.position;
                    orangePortal.transform.rotation = previewPortal.transform.rotation;
                }


            }
            previewActive = false;
        }

        previewPortal.SetActive(previewActive);
    }

    private bool MovePreview()
    {
        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if(Physics.Raycast(r, out RaycastHit hitInfo, maxShootDistance, portalMask))
        {
            if(hitInfo.collider.gameObject.CompareTag("PortalEnabled"))
            {
                previewPortal.transform.position = hitInfo.point;
                previewPortal.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
                return previewPortal.GetComponent<PortalPreviewScript>().isValidPosition(cam);
            }

            return false;
        }
        return false;
    }
}
