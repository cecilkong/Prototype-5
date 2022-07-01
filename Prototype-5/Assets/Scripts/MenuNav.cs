using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNav : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    public GameObject musicPlayer;
    
    void Start()
    {
        //DontDestroyOnLoad(musicPlayer);
        Time.timeScale = 1f;
        //FindObjectOfType<AudioManager>().Play("Background");
    }

    public void Pause () {
        //FindObjectOfType<AudioManager>().Play("Click");
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; 
    }
    
    public void Resume () {
        //FindObjectOfType<AudioManager>().Play("Click");
        pauseScreen.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void RestartLevel()
    {
        //FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}