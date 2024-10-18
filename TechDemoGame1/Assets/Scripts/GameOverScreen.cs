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
        
    }


    public void Setup()
    {
        gameObject.SetActive(true);
    }
}
