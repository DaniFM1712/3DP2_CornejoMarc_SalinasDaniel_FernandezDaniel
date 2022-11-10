using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractionTriggers : MonoBehaviour
{
    
    
    public void resetGame(GameObject triggered)
    {
        if (triggered.CompareTag("Lava"))
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(1);
            }
    }

    public void endGame(GameObject triggered)
    {
        if (triggered.CompareTag("EndZone"))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(2);
        }
    }

    
}
