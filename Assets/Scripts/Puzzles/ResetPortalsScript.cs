using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetPortalsScript : MonoBehaviour
{
    [SerializeField] UnityEvent<bool> resetPortals;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            resetPortals.Invoke(false);
        }
    }
}
