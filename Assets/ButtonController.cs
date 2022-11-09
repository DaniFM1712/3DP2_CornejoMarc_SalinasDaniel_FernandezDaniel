using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] List<GameObject> buttonsToActive;
    [SerializeField] GameObject door;
    private void Update()
    {
        if (buttonsToActive.Count == 0)
        {
            //Activar animación puerta.
            Destroy(gameObject);
        }
    }

    public void activateButton(GameObject button)
    {
        buttonsToActive.Remove(button);
    }
}
