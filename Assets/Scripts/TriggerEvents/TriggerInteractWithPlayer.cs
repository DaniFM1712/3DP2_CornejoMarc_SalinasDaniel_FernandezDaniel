using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInteractWithPlayer : MonoBehaviour
{
    [SerializeField] UnityEvent<GameObject> trigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            trigger.Invoke(gameObject);
    }
}
