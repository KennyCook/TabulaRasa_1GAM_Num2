using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject GamePanel;

    public static int CurrentLevel;

    private bool _isGameOver = false,
        _playedLevel1,
        _playedLevel2,
        _playedLevel3,
        _playedLevel4;
    private static int _currentScore, _highScore;

    private void Awake()
    {
        GamePanel = GameObject.Find("Game Panel");
    }

	void Start ()
    {
        _playedLevel1 = _playedLevel2 = _playedLevel3 = _playedLevel4 = false;
        InitializeGame();
    }

    void Update()
    {
        if (_isGameOver)
        {
            HandleGameOverInput();
        }
        else
        {
            HandleLevels();
        }
    }

    public void UpdateScore()
    {
        _currentScore++;
        GamePanel.transform.GetChild(1).GetComponent<Text>().text = _currentScore.ToString();

        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            GameValues.HighScore = _highScore;
            GamePanel.transform.GetChild(3).GetComponent<Text>().text = _highScore.ToString();
        }
    }

    public void HandleLevels()
    {
        if (_currentScore >= 120 && CurrentLevel == 3)
        {
            GamePanel.transform.GetChild(7).GetComponent<Text>().text =
                "It is a shame... You were truly a worthy master...";
            GamePanel.transform.GetChild(7).GetComponent<Animator>().Play(null);
            CurrentLevel = 4;
            gameObject.GetComponent<EnemySpawnerController>().LevelIncreased = true;
            _playedLevel4 = true;
        }
        else if (_currentScore >= 60 && CurrentLevel == 2)
        {
            if (!_playedLevel3)
            {
                GamePanel.transform.GetChild(7).GetComponent<Text>().text =
                    "Run... Fight... It only emboldens their pursuit.\n\nYour end approaches...";
            }
            else
            {

                GamePanel.transform.GetChild(7).GetComponent<Text>().text =
                    "Few have held the hunters at bay this long...\n\nNone have beaten them back...\n\nNone none ever shall...";
            }
            GamePanel.transform.GetChild(7).GetComponent<Animator>().Play(null);
            CurrentLevel = 3;
            gameObject.GetComponent<EnemySpawnerController>().LevelIncreased = true;
            _playedLevel3 = true;
        }
        else if (_currentScore >= 20 && CurrentLevel == 1)
        {
            if (!_playedLevel2)
            {
                GamePanel.transform.GetChild(7).GetComponent<Text>().text =
                    "You show some skill...\n\nBut you too will fall...";
            }
            else
            {
                GamePanel.transform.GetChild(7).GetComponent<Text>().text =
                    "They will never stop...\n\nThe curse holds them to their duty...\n\nAnd seals your doom...";
            }
            GamePanel.transform.GetChild(7).GetComponent<Animator>().Play(null);
            CurrentLevel = 2;
            gameObject.GetComponent<EnemySpawnerController>().LevelIncreased = true;
            _playedLevel2 = true;
        }
        else if (_currentScore < 20 && CurrentLevel!= 1)
        {
            if (!_playedLevel1)
            {
                GamePanel.transform.GetChild(7).GetComponent<Text>().text =
                    "They are coming for you...";
            }
            else
            {
                GamePanel.transform.GetChild(7).GetComponent<Text>().text =
                    "Another fool seeking power...\n\nYour fate will be the same...";
            }
            GamePanel.transform.GetChild(7).GetComponent<Animator>().Play(null);
            CurrentLevel = 1;
            gameObject.GetComponent<EnemySpawnerController>().LevelIncreased = true;
            _playedLevel1 = true;
        }
    }

    public void PrepareForGameOver()
    {
        // Stop spawning enemies.
        //gameObject.GetComponent<EnemySpawnerController>().CanSpawnEnemies = false;
        EnemySpawnerController.CanSpawnEnemies = false;
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
        // Display Game Over GUI text and prompts (R to replay, Q to quit to menu).
        GamePanel.transform.GetChild(4).gameObject.SetActive(true);
        GamePanel.transform.GetChild(5).gameObject.SetActive(true);
    }

    private void HandleGameOverInput()
    {
        // Replay.
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Disable Game Over UI text
            GamePanel.transform.GetChild(4).gameObject.SetActive(false);
            GamePanel.transform.GetChild(5).gameObject.SetActive(false);
            InitializeGame();
        }
        // Quit to Main Menu.
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Main Menu Scene", LoadSceneMode.Single);
        }
    }

    private void InitializeGame()
    {
        _isGameOver = false;
        // Set current score text and value to 0, high score text and variable to GameValues.HighScore
        _currentScore = 0;
        GamePanel.transform.GetChild(1).GetComponent<Text>().text = _currentScore.ToString();
        _highScore = GameValues.HighScore;
        GamePanel.transform.GetChild(3).GetComponent<Text>().text = _highScore.ToString();

        // Set high score to gv.highscore
        _highScore = GameValues.HighScore;
        
        // enable player's collider 
        Player.GetComponent<BoxCollider2D>().enabled = true;
        
        // enable all player's child GOs and disable all child SRs except the top 2 (running ones)
        // *** [LPT: this is why you should avoid using more than one Animator per GO] ***
        Player.GetComponent<Animator>().speed = 1;
        for (int i = 0; i < Player.transform.childCount; i++)
        {
            Player.transform.GetChild(i).gameObject.SetActive(true);
            if (i < 2)
            {
                Player.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Player.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
        // return camera to normal, BG: white, cullingmask: everything
        Camera.main.backgroundColor = Color.white;
        float layersToCull = Mathf.Pow(2, 8) + Mathf.Pow(2, 9) + Mathf.Pow(2, 10) + Mathf.Pow(2, 11);
        Camera.main.cullingMask = Camera.main.cullingMask | (int) layersToCull;

        // set difficulty to level 1
        CurrentLevel = 0;
        HandleLevels();
        
        // start spawing enemies
        //gameObject.GetComponent<EnemySpawnerController>().CanSpawnEnemies = true;
        EnemySpawnerController.CanSpawnEnemies = true;
    }
}

/*
 * TODO: Level increase messages
 * Level 1 (First play): "They are coming for you..."
 * Level 2 (First play): "You show some skill. ...But you too will fall."
 * Level 3 (First play): "Run... Fight... It only emboldens their pursuit. ...Your end approaches."
 * Level 4 (First play): "It is a shame... You were truly a worthy master." 
 * 
 * Level 1 (!First play): "Another fool seeking power...\n\nYour fate will be the same..."
 * Level 2 (!First play): "They will never stop...\n\nThe curse holds them to their duty...\n\nAnd seals your doom..."
 * Level 3 (!First play): "Few have held the hunters at bay this long...\n\nNone have beaten them back...\n\n\None none ever shall..."
 * Level 4 (!First play): ""
 */
