using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] string messageEnabled;
    [SerializeField] string messageDisabled;
    [SerializeField] KeyCode keyCode;
    [SerializeField] int maxTimesPressed;
    [SerializeField] UnityEvent buttonPressed;
    int timesPressed;
    bool enabled = true;

    public bool isEnabled()
    {
        return timesPressed < maxTimesPressed;
    }

    public void press()
    {
        timesPressed++;
        Debug.Log("Pressed!");
        if(isEnabled())
            buttonPressed.Invoke();
    }

    public string GetMessage()
    {
        return isEnabled() ? messageEnabled : messageDisabled;
    }

    public KeyCode GetKeyCode()
    {
        return keyCode;
    }
}
