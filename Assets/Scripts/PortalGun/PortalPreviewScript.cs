using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPreviewScript : MonoBehaviour
{

    [SerializeField] Transform previewPointsParent;
    [SerializeField] LayerMask portalMask;
    [SerializeField] float maxPortalDist = float.MaxValue;
    [SerializeField] float maxPointDist;
    [SerializeField] float maxAngleValue;
    List<Transform> previewPoints = new List<Transform>();

    public bool isValidPosition(Camera cam)
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
                if ((hitInfo.collider.gameObject.transform.position-point.position).magnitude > maxPointDist)

                {
                   return false;
                }

                if(Vector3.Angle(hitInfo.normal, point.transform.forward)>maxAngleValue)
                {
                    return false;
                }
            }
            else {
                return false;
            }

            prevPoint = hitInfo.collider.gameObject.transform;
        }
        return true;
    }
}
