using UnityEngine;
using System.Collections;

public class EnemySpawnerController: MonoBehaviour
{
    public GameObject[] EnemyPrefabs;

    public float  _respawnTicker, _timeUntilNextSpawn = 5f;

    private bool _canSpawnEnemies = true;
    public bool CanSpawnEnemies { get { return _canSpawnEnemies; } set { _canSpawnEnemies = value; } }

    private void Start()
    {
        _respawnTicker = _timeUntilNextSpawn;
    }

    private void Update()
    {
        _respawnTicker-= Time.deltaTime;

        if (_respawnTicker <= 0 && _canSpawnEnemies)
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

            _respawnTicker = _timeUntilNextSpawn;
        }
    }
}
