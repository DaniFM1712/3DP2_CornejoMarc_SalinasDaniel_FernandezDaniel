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
    [SerializeField] float moveSpeed;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float throwForce;

    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            Debug.Log("GravityShoot");
            takenObject = gravityShoot();
            if(takenObject != null)
            {
                currentStatus = Status.taking;
                initialRotation = takenObject.rotation;
                initialPosition = takenObject.position;
            }

        }
        if (takenObject != null)
        {
            if (Input.GetMouseButton(2) && takenObject != null)
            {
                takenObject.isKinematic = true;
                switch (currentStatus)
                {
                    case Status.taking:
                        updateTaking();
                        break;
                    case Status.taken:
                        updateTaken();
                        break;
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    detachObject(throwForce);
                }

            }
            else
            {
                detachObject(0);
            }
        }
    }

    private Rigidbody gravityShoot()
    {
        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(r, out RaycastHit hitInfo, float.MaxValue, layerMask))
        {
            return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
        }
        return null;
    }

    private void updateTaking()
    {
        Vector3 dir = (attachPosition.position - takenObject.position).normalized;
        takenObject.MovePosition(takenObject.position + (dir * Time.deltaTime * moveSpeed));

        float totalDist = (attachPosition.position - initialPosition).magnitude;
        float currentDist = (takenObject.position - initialPosition).magnitude;
        float distPercentage = currentDist / totalDist;
        takenObject.rotation = Quaternion.Lerp(initialRotation, attachPosition.rotation, distPercentage);
        if((attachPosition.position - takenObject.position).magnitude < (Time.deltaTime * moveSpeed))
        {
            currentStatus = Status.taken;
            takenObject.transform.parent = attachPosition;
        }
    }

    private void updateTaken()
    {
        takenObject.position = attachPosition.position;
        takenObject.rotation = attachPosition.rotation;
    }

    private void detachObject(float force)
    {
        takenObject.isKinematic = false;
        takenObject.velocity = force*attachPosition.forward;
        takenObject.transform.parent = null;
        takenObject = null;
    }
}
