using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //delat for gameover scene appereance
    [SerializeField] float delayInSeconds = 2f;

    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        //stop game if press escape
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel1()
    {
        //reset all game params before start from the beggining
        gameSession.ResetGame();
        SceneManager.LoadScene("Level 1");
    }

    public void LoadNextScene()
    {
        StartCoroutine(WaitAndLoadNextScene());
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoadGameOver());
    }

    public void LoadWin()
    {
        StartCoroutine(WaitAndLoadEWin());
    }

    public void LoadExcelent()
    {
        StartCoroutine(WaitAndLoadExcelent());

    }

    //load excelent with delay
    IEnumerator WaitAndLoadEWin()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Win");
    }

    //load excelent with delay
    IEnumerator WaitAndLoadExcelent()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Excelent");
    }

    //load next scene with delay
    IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(delayInSeconds);
        int currentSceneIndex = gameSession.GetSceneIndex();
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    //load game over screen with delay
    IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        //Stop play mode (comment line before build game)
        //UnityEditor.EditorApplication.isPlaying = false;
        //stop game not in editor
        Application.Quit();
    }
}
