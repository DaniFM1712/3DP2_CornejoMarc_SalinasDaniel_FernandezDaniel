using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPresser : MonoBehaviour
{
    [SerializeField] Event buttonEvent;
    List<ButtonTrigger> triggers = new List<ButtonTrigger>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out ButtonTrigger buttonTrigger))
        {
            triggers.Add(buttonTrigger);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out ButtonTrigger buttonTrigger))
        {
            triggers.Remove(buttonTrigger);
        }
    }

    private void Update()
    {
        foreach(ButtonTrigger buttonTrigger in triggers)
        {
            Debug.Log(buttonTrigger.GetMessage());
            if (Input.GetKeyDown(buttonTrigger.GetKeyCode()))
            {
                buttonTrigger.press();
            }

        }
    }
}
