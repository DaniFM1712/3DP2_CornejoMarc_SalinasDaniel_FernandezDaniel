using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun__ : MonoBehaviour
{

    Rigidbody takenObject;
    enum Status { taking, taken }
    Status currentStatus;

    [SerializeField] Transform attachPosition;
    Vector3 initialPosition;
    Quaternion initialRotation;
    [SerializeField]
    float moveSpeed;

    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            Debug.Log("GravityShoot");
            //takenObject = gravityShoot();
        }
        if (takenObject != null)
        {
            if (Input.GetMouseButton(2) && takenObject != null)
            {
                takenObject.isKinematic = true;
                switch (currentStatus)
                {
                    case Status.taking:
                        //updateTaking();
                        break;
                    case Status.taken:
                        //updateTaken();
                        break;
                }

            }
            else
            {
                //detachObject();
            }
        }
    }
}
