using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject UpperBodyRunning,
        LowerBodyRunning,
        UpperBodySwordAttackForward,
        UpperBodySwordAttackBackward,
        UpperBodySwordBlock;

    private Animator _swordAttackForwardAnimator, _swordAttackBackwardAnimator, _blockAnimator;

    public float RunAnimationSpeed = 0.42f;
    public float AttackAnimationSpeed = 0.3f;             // Set this higher, adjust animations in dope sheet (slow wind up, fast swing)
    private bool _canAttack = true;

    // private Color _transparent = new Color(0xFF, 0xFF, 0xFF, 0x00);      // probably not needed, but nice to know

    private CurrentAnimationEvent _currAnimEvent;
    public CurrentAnimationEvent CurrAnimEvent { get { return _currAnimEvent; } set { _currAnimEvent = value; } }

    void Awake()
    {
        _swordAttackForwardAnimator = UpperBodySwordAttackForward.GetComponent<Animator>();
        _swordAttackBackwardAnimator = UpperBodySwordAttackBackward.GetComponent<Animator>();
        _blockAnimator = UpperBodySwordBlock.GetComponent<Animator>();
    }

    void Start ()
    {
        UpperBodyRunning.GetComponent<Animator>().speed =
            LowerBodyRunning.GetComponent<Animator>().speed = RunAnimationSpeed;

        _swordAttackForwardAnimator.speed =
            _swordAttackBackwardAnimator.speed =
            _blockAnimator.speed = AttackAnimationSpeed;
    }

    void Update ()
    {
        // DEBUG
        UpperBodyRunning.GetComponent<Animator>().speed =
            LowerBodyRunning.GetComponent<Animator>().speed = RunAnimationSpeed;
        // DEBUG
        _swordAttackForwardAnimator.speed =
            _swordAttackBackwardAnimator.speed =
            _blockAnimator.speed = AttackAnimationSpeed;

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
        // Set other children's spriterenderers to false
    }

    private void HandleAnimationEvents()
    {
        switch (_currAnimEvent)
        {
            case CurrentAnimationEvent.SWORD_ATTACK_FORWARD:
                if (Input.GetKey(KeyCode.D) && _swordAttackForwardAnimator.speed > 0)
                {
                    _swordAttackForwardAnimator.speed = 0;
                }
                else if (_swordAttackForwardAnimator.speed == 0)
                {
                    _swordAttackForwardAnimator.speed = AttackAnimationSpeed;
                }
                else
                {
                    _currAnimEvent = CurrentAnimationEvent.NONE;
                }
                break;
            case CurrentAnimationEvent.SWORD_ATTACK_BACKWARD:
                if (Input.GetKey(KeyCode.A) && _swordAttackBackwardAnimator.speed > 0)
                {
                    _swordAttackBackwardAnimator.speed = 0;
                }
                else if (_swordAttackBackwardAnimator.speed == 0)
                {
                    _swordAttackBackwardAnimator.speed = AttackAnimationSpeed;
                }
                else
                {
                    _currAnimEvent = CurrentAnimationEvent.NONE;
                }
                break;
            case CurrentAnimationEvent.BLOCK:
                if (Input.GetKey(KeyCode.W) && _blockAnimator.speed > 0)
                {
                    _blockAnimator.speed = 0;
                }
                else if (_blockAnimator.speed == 0)
                {
                    _blockAnimator.speed = AttackAnimationSpeed;
                }
                else
                {
                    _currAnimEvent = CurrentAnimationEvent.NONE;
                }
                break;
            case CurrentAnimationEvent.CURRENT_ANIMATION_FINISHED:
                _canAttack = true;
                _currAnimEvent = CurrentAnimationEvent.NONE;
                ResetAnimations();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy Attack"))
        {
            Debug.Log("I'm dead.");
            // Death();
        }
    }

    private void Death()
    {
        // On death set main camera to cull Enemy Layer, 
        //          set main camera background to red 
        //          play animation
    }
}

