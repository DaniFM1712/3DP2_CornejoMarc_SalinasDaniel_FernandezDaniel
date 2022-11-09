using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [SerializeField] float offsetAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PortalScript portal))
        {
            Vector3 l_position = portal.virtualPortal.InverseTransformPoint(transform.position);
            l_position.z += offsetAmount;
            transform.position = portal.otherPortal.transform.TransformPoint(l_position);

            Vector3 l_direction = portal.virtualPortal.InverseTransformDirection(transform.forward);
            transform.forward = portal.otherPortal.transform.TransformDirection(l_direction);

            if(gameObject.TryGetComponent(out FpsController fpsController))
            {
                fpsController.RecalculateOrientation();
            }
            if (gameObject.TryGetComponent(out Rigidbody rb))
            {
                Vector3 l_velocity = portal.virtualPortal.InverseTransformDirection(rb.velocity);
                rb.velocity = portal.otherPortal.transform.TransformDirection(l_velocity); ;
            }

            if (gameObject.layer.CompareTo("Player") != 0)
            {
                transform.localScale = portal.transform.localScale;
            }
        }       
    }
}
