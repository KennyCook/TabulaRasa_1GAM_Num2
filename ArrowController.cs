using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
    public GameObject ArrowDeath;

    private float _movementSpeed;

    private bool _isDead = false;
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    private void Start()
    {
        switch (GameController.CurrentLevel)
        {
            case 1:
                _movementSpeed = GameValues.EnemyMoveSpeed_Level_1 * -1.8f;
                break;
            case 2:
                _movementSpeed = GameValues.EnemyMoveSpeed_Level_2 * -1.8f;
                break;
            case 3:
                _movementSpeed = GameValues.EnemyMoveSpeed_Level_3 * -1.8f;
                break;
            case 4:
                _movementSpeed = GameValues.EnemyMoveSpeed_Level_4 * -1.8f;
                break;
        }
    }

    private void Update()
    {
        if (!IsDead)
        {
            transform.Translate(new Vector2(_movementSpeed, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player Attack"))
        {
            _isDead = true;
            _movementSpeed = 0;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ArrowDeath.GetComponent<SpriteRenderer>().enabled = true;
            ArrowDeath.GetComponent<Animator>().Play(null);
        }
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
