using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject UpperBodyRunning,
        LowerBodyRunning,
        UpperBodySwordAttackForward,
        UpperBodySwordAttackBackward,
        UpperBodySwordBlock,
        PlayerDying;

    private Animator _swordAttackForwardAnimator, _swordAttackBackwardAnimator, _blockAnimator;

    public float RunAnimationSpeed = 0.42f;
    private bool _canAttack = true;

    private CurrentAnimationEvent _currAnimEvent;
    public CurrentAnimationEvent CurrAnimEvent { get { return _currAnimEvent; } set { _currAnimEvent = value; } }

    private bool _isDead = false;
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    void Awake()
    {
        _swordAttackForwardAnimator = UpperBodySwordAttackForward.GetComponent<Animator>();
        _swordAttackBackwardAnimator = UpperBodySwordAttackBackward.GetComponent<Animator>();
        _blockAnimator = UpperBodySwordBlock.GetComponent<Animator>();
    }

    void Start()
    {
        UpperBodyRunning.GetComponent<Animator>().speed =
            LowerBodyRunning.GetComponent<Animator>().speed = RunAnimationSpeed;
    }

    void Update()
    {
        // DEBUG
        UpperBodyRunning.GetComponent<Animator>().speed =
            LowerBodyRunning.GetComponent<Animator>().speed = RunAnimationSpeed;

        if (_currAnimEvent != CurrentAnimationEvent.NONE)
        {
            HandleAnimationEvents();
        }

        if (_canAttack)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _canAttack = false;
                AttackForward();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _canAttack = false;
                AttackBackward();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _canAttack = false;
                Block();
            }
        }
    }

    private void AttackForward()
    {
        // Enable the attacking spriterenderer first
        UpperBodySwordAttackForward.GetComponent<SpriteRenderer>().enabled = true;
        UpperBodyRunning.GetComponent<SpriteRenderer>().enabled = false;
        _swordAttackForwardAnimator.Play(null);
    }

    private void AttackBackward()
    {
        UpperBodySwordAttackBackward.GetComponent<SpriteRenderer>().enabled = true;
        UpperBodyRunning.GetComponent<SpriteRenderer>().enabled = false;
        _swordAttackBackwardAnimator.Play(null);
    }

    private void Block()
    {
        UpperBodySwordBlock.GetComponent<SpriteRenderer>().enabled = true;
        UpperBodyRunning.GetComponent<SpriteRenderer>().enabled = false;
        _blockAnimator.Play(null);
    }

    private void ResetAnimations()
    {
        UpperBodyRunning.GetComponent<SpriteRenderer>().enabled = true;

        UpperBodySwordAttackForward.GetComponent<SpriteRenderer>().enabled =
            UpperBodySwordAttackBackward.GetComponent<SpriteRenderer>().enabled =
            UpperBodySwordBlock.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void HandleAnimationEvents()
    {
        switch (_currAnimEvent)
        {
            case CurrentAnimationEvent.SWORD_ATTACK_FORWARD:
                if (Input.GetKey(KeyCode.D)) //&& _swordAttackForwardAnimator.speed > 0)
                {
                    _swordAttackForwardAnimator.speed = 0;
                }
                else if (_swordAttackForwardAnimator.speed == 0)
                {
                    _swordAttackForwardAnimator.speed = 1;
                }
                else
                {
                    _currAnimEvent = CurrentAnimationEvent.NONE;
                }
                break;
            case CurrentAnimationEvent.SWORD_ATTACK_BACKWARD:
                if (Input.GetKey(KeyCode.A)) //&& _swordAttackBackwardAnimator.speed > 0)
                {
                    _swordAttackBackwardAnimator.speed = 0;
                }
                else if (_swordAttackBackwardAnimator.speed == 0)
                {
                    _swordAttackBackwardAnimator.speed = 1;
                }
                else
                {
                    _currAnimEvent = CurrentAnimationEvent.NONE;
                }
                break;
            case CurrentAnimationEvent.BLOCK:
                if (Input.GetKey(KeyCode.W)) //&& _blockAnimator.speed > 0)
                {
                    _blockAnimator.speed = 0;
                }
                else if (_blockAnimator.speed == 0)
                {
                    _blockAnimator.speed = 1;
                }
                else
                {
                    _currAnimEvent = CurrentAnimationEvent.NONE;
                }
                break;
            case CurrentAnimationEvent.PLAYER_ATTACK_ANIMATION_FINISHED:
                _canAttack = true;
                _currAnimEvent = CurrentAnimationEvent.NONE;
                ResetAnimations();
                break;
            default:
                break;
        }
    }

    private void Death()
    {
        // Disable collider to prevent anymore enemy attacks
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // On death set main camera to cull Player, Player Attack, Enemy, and Enemy Attack Layer
        float layersToCull = Mathf.Pow(2, 8) + Mathf.Pow(2, 9) + Mathf.Pow(2, 10) + Mathf.Pow(2, 11);
        Camera.main.cullingMask = Camera.main.cullingMask & Camera.main.cullingMask - (int)layersToCull;

        // Set main camera background to red
        Camera.main.backgroundColor = Color.red;                                                            // quick lerp?

        // Stop Player Jiggle animation
        gameObject.GetComponent<Animator>().Stop();

        // Disable child GOs.
        UpperBodySwordAttackForward.SetActive(false);
        UpperBodySwordAttackBackward.SetActive(false);
        UpperBodySwordBlock.gameObject.SetActive(false);
        UpperBodyRunning.gameObject.SetActive(false);
        LowerBodyRunning.gameObject.SetActive(false);

        // Play death animation.
        PlayerDying.GetComponent<SpriteRenderer>().enabled = true;
        PlayerDying.GetComponent<Animator>().Play(null);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy Attack"))
        {
            _isDead = true;
            Death();
        }
    }
}