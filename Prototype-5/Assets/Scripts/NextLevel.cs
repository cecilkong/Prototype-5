using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{
    public GameObject debuffsScreen;
    void OnTriggerEnter2D(Collider2D triggerInfo)
    {
        FindObjectOfType<AudioManager>().Play("LevelComplete");
        if (triggerInfo.tag == "Player")
        {
            debuffsScreen.SetActive(true);
            Time.timeScale = 0f; 
            // LoadNextLevel();
        }
    }


    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Level_1");
        }
    }
}