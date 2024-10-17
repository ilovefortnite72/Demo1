using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    public void Restart()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Setup()
    {
        gameObject.SetActive(true);
    }
}
