using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 1f;
    public void LoadNextScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene(){
        SceneManager.LoadScene(0);
    }

    public void LoadBreakerBlock(){
        SceneManager.LoadScene("01_Level01");
        FindObjectOfType<GameStatus>().ResetGame();
    }

    public void LoadSpaceShooter(){
        SceneManager.LoadScene("01LevelSS");
        FindObjectOfType<GameStatusSS>().ResetGame();
    }

    public void LoadGameOver(){
        StartCoroutine(WaitAndLoad());
    }
    public void QuitGame(){
        Application.Quit();
    }

    IEnumerator WaitAndLoad(){
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("GameOver");
    }


}
