using UnityEngine;
using Spine.Unity;

public class AnimationController : MonoBehaviour
{
    public SkeletonAnimation animation;
    public PlayerController player;
    private PlayerState _state;

    // Start is called before the first frame update
    void Start()
    {
        _state = PlayerState.Idle;
        animation.AnimationState.SetAnimation(0, "idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_state != player.state)
        {
            _state = player.state;
            switch (_state)
            {
                case PlayerState.Idle:
                    animation.AnimationState.SetAnimation(0, "idle", true);
                    break;
                case PlayerState.Run:
                    animation.AnimationState.SetAnimation(0, "run", true);
                    break;
                case PlayerState.Jump:
                    animation.AnimationState.SetAnimation(0, "jump", false);
                    break;
                case PlayerState.Slide:
                    animation.AnimationState.SetAnimation(0, "hoverboard", false);
                    break;
            }
        }
    }
}
