using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//class with singlton. for data that  should not be reset on each scene
public class GameSession : MonoBehaviour
{
    private int score = 0;
    private int sceneId = 0;
    

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public int GetSceneIndex()
    {
        return sceneId;
    }

    public void SetSceneIndex()
    {
        sceneId = SceneManager.GetActiveScene().buildIndex;
    }

    //reset score to 0
    public void ResetGame()
    {
        Destroy(gameObject);
    }


}
