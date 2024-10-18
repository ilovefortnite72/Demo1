using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;

    }


    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
