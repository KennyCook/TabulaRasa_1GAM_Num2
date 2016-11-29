using UnityEngine;
using System.Collections;

public class ObjectSpawnerController : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;

    public float _timeUntilNextSpawn = 5f;

    private void Update()
    {
        _timeUntilNextSpawn -= Time.deltaTime;

        if (_timeUntilNextSpawn <= 0)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    Instantiate(EnemyPrefabs[0], new Vector2(Camera.main.transform.position.x - 7f, 0), Quaternion.identity);
                    break;

                case 1:
                    Instantiate(EnemyPrefabs[0], new Vector2(Camera.main.transform.position.x + 7f, 0), new Quaternion(0, 180f, 0, 0));
                    break;

                //case 2:
            }
            _timeUntilNextSpawn = 5f;
        }
    }
}
