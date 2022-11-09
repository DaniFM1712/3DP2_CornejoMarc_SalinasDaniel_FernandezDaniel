using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonActived : MonoBehaviour
{
    [SerializeField] UnityEvent<GameObject>buttonActived;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Cube"))
        buttonActived.Invoke(gameObject);
    }
}
