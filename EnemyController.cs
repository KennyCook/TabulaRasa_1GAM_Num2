using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public GameObject UpperBodyRunning, LowerBodyRunning, EnemyAttacking, EnemyDying;

    public float MovementSpeed = 0.05f;

    private Animator _attackAnimator, _deathAnimator;

    private bool _canAttack = true, _canMove = true;

    private void Awake()
    {
        _attackAnimator = EnemyAttacking.GetComponent<Animator>();
        _deathAnimator = EnemyDying.GetComponent<Animator>();
    }

    private void Start()
    {
        UpperBodyRunning.GetComponent<Animator>().speed =
            LowerBodyRunning.GetComponent<Animator>().speed = 0.42f;
    }

    private void Update()
    {
        if (_canMove)
        {
            Move();
        }
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player Attack"))
        {
            Debug.Log("Bleh.");
            Die();
        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy Attack Zone") && _canAttack)
        {
            _canAttack = false;
            Debug.Log(name + " beginning attack.");
            Attack();
        }
    }

    private void Die()
    {
        EnemyDying.GetComponent<SpriteRenderer>().enabled = true;
        // Set GOs to inactive to prevent postmortem collisions
        EnemyAttacking.SetActive(false);
        UpperBodyRunning.SetActive(false);
        LowerBodyRunning.SetActive(false);
        _canMove = false;
        _deathAnimator.Play(null);
    }
}
