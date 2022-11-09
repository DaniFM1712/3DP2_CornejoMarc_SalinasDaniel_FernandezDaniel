using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxRayDist = float.MaxValue;
    bool isEnabled = false;

    public void activate(bool enable)
    {
        isEnabled = enable;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.enabled = isEnabled;
        if (!lineRenderer.enabled)
            return;

        Ray r = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, maxRayDist, layerMask))
        {
            lineRenderer.SetPosition(1 ,new Vector3(0f, 0f, hitInfo.distance));
            Debug.Log("HIT: " + hitInfo.collider.gameObject.name);
            if(hitInfo.collider.gameObject.TryGetComponent(out LaserButtonScript laserButton))
            {
                laserButton.Pressed();
            }
            else if (hitInfo.collider.gameObject.TryGetComponent(out ReflectionScript reflection))
            {
                reflection.ActivateReflection(true);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, new Vector3(0f, 0f, maxRayDist));

        }
    }
}
