using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public GameObject UpperBodyRunning, LowerBodyRunning, EnemyAttacking, EnemyDying;

    public float MovementSpeed = 0.05f, RunAnimationSpeed = 0.42f;

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
            LowerBodyRunning.GetComponent<Animator>().speed = RunAnimationSpeed;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector2(MovementSpeed, 0f));
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
        if (MovementSpeed > 0)
        {
            MovementSpeed = -MovementSpeed;
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
            Debug.Log(name + " beginning attack.");
            Attack();
        }
    }
}
