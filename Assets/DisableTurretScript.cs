using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTurretScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Turret") || collision.gameObject.CompareTag("Cube"))
        {
            if(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 5f)
            {
                gameObject.GetComponentInChildren<LaserScript>().PermanentDisable();
            }
        }
    }
}
