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
        ParentGO.GetComponent<PlayerController>().CurrAnimEvent = CurrentAnimationEvent.CURRENT_ANIMATION_FINISHED;
    }
}
