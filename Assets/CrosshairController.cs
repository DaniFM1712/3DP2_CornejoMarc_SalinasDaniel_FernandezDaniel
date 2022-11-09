using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CrosshairController : MonoBehaviour
{
    [SerializeField] Image emptyCrosshair;
    [SerializeField] Image fullCrosshair;
    [SerializeField] Image blueCrosshair;
    [SerializeField] Image orangeCrosshair;
    bool shotBluePortal;
    bool shotOrangePortal;
    // Start is called before the first frame update
    void Start()
    {
        shotBluePortal = false;
        shotOrangePortal = false;
        ChangeCrosshair();
        
    }

    public void BluePortalActivated(bool isActive)
    {
        shotBluePortal = isActive;
        ChangeCrosshair();
    }

    public void OrangePortalActivated(bool isActive)
    {
        shotOrangePortal = isActive;
        ChangeCrosshair();

    }

    private void ChangeCrosshair()
    {
        DisableCrosshairs();

        if (shotBluePortal && shotOrangePortal)
        {
            fullCrosshair.enabled = true;
        }
        else if (!shotBluePortal && shotOrangePortal)
        {
            orangeCrosshair.enabled = true;
        }
        else if (shotBluePortal && !shotOrangePortal)
        {
            blueCrosshair.enabled = true;
        }
        else
        {
            emptyCrosshair.enabled = true;
        }

    }

    private void DisableCrosshairs()
    {
        emptyCrosshair.enabled = false;
        fullCrosshair.enabled = false;
        blueCrosshair.enabled = false;
        orangeCrosshair.enabled = false;
    }
}
