using UnityEngine;
using System.Collections;

public class AnimationEventController : MonoBehaviour
{
    public GameObject ParentGO;

    private CurrentAnimationEvent _animEventType;

    private void Awake()
    {
        switch (gameObject.name)
        {
            case "Player Sword Attack Forward":
                _animEventType = CurrentAnimationEvent.SWORD_ATTACK_FORWARD;
                break;
            case "Player Sword Attack Backward":
                _animEventType = CurrentAnimationEvent.SWORD_ATTACK_BACKWARD;
                break;
            case "Player Sword Block":
                _animEventType = CurrentAnimationEvent.BLOCK;
                break;
            default:
                break;
        }
    }

    public void HoldAnimation()
    {
        ParentGO.GetComponent<PlayerController>().CurrAnimEvent = _animEventType;
    }

    public void AnimationFinished()
    {
        ParentGO.GetComponent<PlayerController>().CurrAnimEvent = CurrentAnimationEvent.PLAYER_ATTACK_ANIMATION_FINISHED;
    }

    public void ArrowDeathAnimationFinished()
    {
        ArrowController ac = ParentGO.GetComponent<ArrowController>();
        if (ac.IsDead)
        {
            ac.DestroyArrow();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().UpdateScore();
        }
    }

    public void EnemyDeathAnimationFinished()
    {
        EnemyController ec = ParentGO.GetComponent<EnemyController>();
        if (ec.IsDead)
        {
            ec.DestroyEnemy();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().UpdateScore();
        }
    }

    public void PlayerDeathAnimationStart()
    {
        if (ParentGO.GetComponent<PlayerController>().IsDead)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PrepareForGameOver();
        }
    }

    public void PlayerDeathAnimationFinished()
    {
        if (ParentGO.GetComponent<PlayerController>().IsDead)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();
        }
    }
}
