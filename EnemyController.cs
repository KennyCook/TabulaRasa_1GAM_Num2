using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public GameObject UpperBodyRunning, LowerBodyRunning, EnemyAttacking, EnemyDying;

    private float _movementSpeed;
    private Animator _attackAnimator, _deathAnimator;

    private bool _canAttack = true;

    private bool _isDead = false;
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    private void Awake()
    {
        _attackAnimator = EnemyAttacking.GetComponent<Animator>();
        _deathAnimator = EnemyDying.GetComponent<Animator>();
    }

    private void Start()
    {
        UpperBodyRunning.GetComponent<Animator>().speed =
            LowerBodyRunning.GetComponent<Animator>().speed = GameValues.RunAnimationSpeed;
        switch (GameController.CurrentLevel)
        {
            case 1:
                _movementSpeed = GameValues.EnemyMoveSpeed_Level_1;
                break;
            case 2:
                _movementSpeed = GameValues.EnemyMoveSpeed_Level_2;
                break;
            case 3:
                _movementSpeed = GameValues.EnemyMoveSpeed_Level_3;
                break;
            case 4:
                _movementSpeed = GameValues.EnemyMoveSpeed_Level_4;
                break;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector2(_movementSpeed, 0f));
    }

    private void Attack()
    {
        UpperBodyRunning.GetComponent<SpriteRenderer>().enabled = false;
        EnemyAttacking.GetComponent<SpriteRenderer>().enabled = true;
        _attackAnimator.Play(null);
    }

    private void EnemyDeath()
    {
        EnemyDying.GetComponent<SpriteRenderer>().enabled = true;

        // Set GOs to inactive to prevent postmortem collisions
        EnemyAttacking.SetActive(false);
        UpperBodyRunning.SetActive(false);
        LowerBodyRunning.SetActive(false);

        // Play death animation
        _deathAnimator.Play(null);

        // If enemy is moving to the right, reverse their direction
        if (_movementSpeed > 0)
        {
            _movementSpeed = -_movementSpeed;
        }
    }

    // Called via Animation Event.
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player Attack"))
        {
            _isDead = true;
            EnemyDeath();
        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy Attack Zone") && _canAttack)
        {
            _canAttack = false;
            Attack();
        }
    }
}
