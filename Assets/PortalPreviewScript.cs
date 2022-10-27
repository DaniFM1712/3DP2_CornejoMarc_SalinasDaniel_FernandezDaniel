using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPreviewScript : MonoBehaviour
{

    [SerializeField] Transform previewPointsParent;
    [SerializeField] LayerMask portalMask;
    [SerializeField] float maxPortalDist = float.MaxValue;
    [SerializeField] float maxPointDist;
    List<Transform> previewPoints = new List<Transform>();

    public bool isInvalidPosition(Camera cam)
    {
        Transform prevPoint = previewPointsParent.GetChild(0);
        foreach(Transform point in previewPointsParent)
        {
            Ray r = new Ray(cam.transform.position, point.position - cam.transform.position);
            if (Physics.Raycast(r, out RaycastHit hitInfo, maxPortalDist, portalMask))
            {
                if (!hitInfo.collider.gameObject.CompareTag("PortalEnabled"))
                {
                    return false;
                }
                //if((prevPoint.position - hitInfo.collider.gameObject.transform.position).magnitude > maxPointDist)
                /*if ((hitInfo.collider.gameObject.transform.position-point.position).magnitude > maxPointDist)

                {
                   return false;
                }

                if()*/
            }
            else {
                return false;
            }

            prevPoint = hitInfo.collider.gameObject.transform;
        }
        return true;
    }
}
