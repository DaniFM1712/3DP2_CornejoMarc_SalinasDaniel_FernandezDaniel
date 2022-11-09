using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PortalGunScript : MonoBehaviour
{
    [SerializeField] GameObject previewPortal;
    [SerializeField] GameObject bluePortal;
    [SerializeField] GameObject orangePortal;
    [SerializeField] Camera cam;
    [SerializeField] float maxShootDistance = float.MaxValue;
    [SerializeField] LayerMask portalMask;
    [SerializeField] UnityEvent<bool> bluePortalShot;
    [SerializeField] UnityEvent<bool> orangePortalShot;
    bool previewActive = false;
    bool portalToActivate = false;

    Vector2 mousescroll;
    float xAxis;
    float yAxis;
    float zAxis;
    [Header ("Scale Portals")]
    [SerializeField]float maxScaleX = 2f;
    [SerializeField] float minScaleX = 0.5f;
    [SerializeField] float maxScaleY = 4f;
    [SerializeField] float minScaleY = 1f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            portalToActivate = true;
            previewActive = MovePreview();
            //scalePortal();

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

                    //bluePortal.transform.localScale = previewPortal.transform.localScale;

                    bluePortalShot.Invoke(true);
                }
                else
                {
                    orangePortal.SetActive(true);
                    orangePortal.transform.position = previewPortal.transform.position;
                    orangePortal.transform.rotation = previewPortal.transform.rotation;
                    //orangePortal.transform.localScale = previewPortal.transform.localScale;

                    orangePortalShot.Invoke(true);
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


    void scalePortal()
    {
        mousescroll = Input.mouseScrollDelta;
        xAxis += mousescroll.y;
        yAxis += mousescroll.y;
        previewPortal.transform.localScale = new Vector3(xAxis, yAxis, zAxis);


        if (xAxis > maxScaleX)
        {
            xAxis = 2;
        }
        if (xAxis < minScaleX)
        {
            xAxis = 0.5f;
        }
        if (yAxis > maxScaleY)
        {
            yAxis = 4;
        }
        if (yAxis < minScaleY)
        {
            yAxis = 1f;
        }
    }

    public void DisablePortals()
    {
        bluePortal.SetActive(false);
        orangePortal.SetActive(false);
    }
}
