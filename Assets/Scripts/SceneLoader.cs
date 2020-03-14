using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    
    }
     public void ReloadFirstScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(0);
    }

    public void LoadLoseScene()
    {
        StartCoroutine(WaitForSceneLoad());
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }

    public void quit()
    {
        Application.Quit();
    }
}
