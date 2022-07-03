using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D triggerInfo)
    {
        FindObjectOfType<AudioManager>().Play("LevelComplete");
        if (triggerInfo.tag == "Player")
        {
            // StartCoroutine(LoadNextLevel());
            LoadNextLevel();
        }
    }

    
    // IEnumerator LoadNextLevel()
    public void LoadNextLevel()
    {
        // StartCoroutine(Player.Instance.Finish(transform.position));
        // yield return new WaitForSeconds(1);
        // SceneTransition.Instance.Fade();
        // yield return new WaitForSeconds(1);
 
        
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