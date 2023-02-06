using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScoreDisplay : MonoBehaviour
{

	TextMeshProUGUI scoreText;
	GameSession gameSession;

	// Use this for initialization
	void Start()
	{
		scoreText = GetComponent<TextMeshProUGUI>();
		gameSession = FindObjectOfType<GameSession>();
	}

	// Update is called once per frame
	void Update()
	{
		//different formats to display score on different scenes
		if(SceneManager.GetActiveScene().name == "Game Over" || SceneManager.GetActiveScene().name == "Win")
        {
			scoreText.text = "Your score: " + gameSession.GetScore().ToString();
		}
		else
        {
			scoreText.text = "Score: " + gameSession.GetScore().ToString();
		}
	}
}
