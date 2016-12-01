using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject Player, GamePanel, EnemyAttackZoneRight, EnemyAttackZoneLeft;

    private int _currentScore, _highScore;

    private bool _isGameOver = false;

	void Start ()
    {
        //Instantiate(Player, Vector2.zero, Quaternion.identity);
	}
	
    void Update ()
    {
        if (_isGameOver)
        {
            // Replay.
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Start new scene or just reset values to default?
            }
            // Quit to Main Menu.
            else if (Input.GetKeyDown(KeyCode.Q)) { }
        }
	}

    public void UpdateScore()
    {
        _currentScore++;
        GamePanel.transform.GetChild(1).GetComponent<Text>().text = _currentScore.ToString();

        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            GamePanel.transform.GetChild(3).GetComponent<Text>().text = _highScore.ToString();
        }
    }

    public void PrepareForGameOver()
    {
        // Stop spawning enemies.
        gameObject.GetComponent<EnemySpawnerController>().CanSpawnEnemies = false;
        // Destroy all currently spawned enemies.
        GameObject[] spawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in spawnedEnemies)
        {
            Destroy(g);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
        // Display Game Over GUI text and prompts (R to replay, Q to quit to menu)
        GamePanel.transform.GetChild(4).gameObject.SetActive(true);
        GamePanel.transform.GetChild(5).gameObject.SetActive(true);
    }
}

// have enemies pull their speed from here?
