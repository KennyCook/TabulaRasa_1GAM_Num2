using UnityEngine;

public class EnemySpawnerController: MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public GameObject EnemyAttackZoneRight, EnemyAttackZoneLeft;

    public float  _respawnTicker, _levelUpDelay;

    private bool _canSpawnEnemies = true, _levelIncreased = false, _changeEnamyAttackZones = false;
    public static bool CanSpawnEnemies { get; set; }// { get { return _canSpawnEnemies; } set { _canSpawnEnemies = value; } }
    public bool LevelIncreased { get { return _levelIncreased; } set { _levelIncreased = value; } }

    private void Start()
    {
        CanSpawnEnemies = true;
        _respawnTicker = GameValues.RespawnTime_Level_1;
        EnemyAttackZoneLeft.transform.position = -GameValues.EnemyAttackZonePosition_Level_1;
        EnemyAttackZoneRight.transform.position = GameValues.EnemyAttackZonePosition_Level_1;
    }

    private void Update()
    {
        if (CanSpawnEnemies)
        {
            _respawnTicker -= Time.deltaTime;
            _levelUpDelay -= Time.deltaTime;

            if (_respawnTicker <= 0) // && _levelUpDelay <= 0)
            {
                switch (Random.Range(0, 3))
                {
                    // Spear or Axe Enemy left
                    case 0:
                        Instantiate(EnemyPrefabs[Random.Range(0, 2)], new Vector2(Camera.main.transform.position.x - 15f, 0), Quaternion.identity);
                        break;
                    // Spear or Axe Enemy right
                    case 1:
                        Instantiate(EnemyPrefabs[Random.Range(0, 2)], new Vector2(Camera.main.transform.position.x + 15f, 0), new Quaternion(0, 180f, 0, 0));
                        break;
                    // Arrow above
                    case 2:
                        Instantiate(EnemyPrefabs[2], EnemyPrefabs[2].transform.position, EnemyPrefabs[2].transform.rotation);
                        break;
                }

                switch (GameController.CurrentLevel)
                {
                    case 1:
                        _respawnTicker = GameValues.RespawnTime_Level_1;
                        break;
                    case 2:
                        _respawnTicker = GameValues.RespawnTime_Level_2;
                        break;
                    case 3:
                        _respawnTicker = GameValues.RespawnTime_Level_3;
                        break;
                    case 4:
                        _respawnTicker = GameValues.RespawnTime_Level_4;
                        break;
                }

                if (_changeEnamyAttackZones)
                {
                    switch (GameController.CurrentLevel)
                    {
                        case 1:
                            EnemyAttackZoneLeft.transform.position = -GameValues.EnemyAttackZonePosition_Level_1;
                            EnemyAttackZoneRight.transform.position = GameValues.EnemyAttackZonePosition_Level_1;
                            break;
                        case 2:
                            EnemyAttackZoneLeft.transform.position = -GameValues.EnemyAttackZonePosition_Level_2;
                            EnemyAttackZoneRight.transform.position = GameValues.EnemyAttackZonePosition_Level_2;
                            break;
                        case 3:
                            EnemyAttackZoneLeft.transform.position = -GameValues.EnemyAttackZonePosition_Level_3;
                            EnemyAttackZoneRight.transform.position = GameValues.EnemyAttackZonePosition_Level_3;
                            break;
                        case 4:
                            EnemyAttackZoneLeft.transform.position = -GameValues.EnemyAttackZonePosition_Level_4;
                            EnemyAttackZoneRight.transform.position = GameValues.EnemyAttackZonePosition_Level_4;
                            break;
                    }
                    _changeEnamyAttackZones = false;
                }
            }
        }

        if (_levelIncreased)
        {
            _levelUpDelay = GameValues.DifficultyLevelIncreaseSpawnDelay;
            _changeEnamyAttackZones = true;
            _levelIncreased = false;
        }
    }
}
