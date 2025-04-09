using UnityEngine;
using UnityEngine.InputSystem;
using Animancer;
using System.Collections;






/*
 !NOT DONE!

Look at Unity's Input System guides on YouTube in order to call functions and play animations based on input.
 */














public class Player : _Character
{
    [SerializeField] AnimancerComponent animancerComponent;

    [Header("Animations")]
    [SerializeField] AnimationClip idleAnimation;
    [SerializeField] AnimationClip swing1;
    [SerializeField] AnimationClip swing2;
    [SerializeField] AnimationClip block;


    Animator handAnimator;

    public enum PlayerState
    {
        Idle,
        Swinging,
        Blocking,
        Jumping
    }



    PlayerState currentState = PlayerState.Idle;

    PlayerInput inputActions;
    private void Awake()
    {
        handAnimator = animancerComponent.Animator;
        inputActions = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        
    }

 
    void PlayAnimation(AnimancerComponent ac, Animator an, PlayerState state)
    {
        if (ac.IsPlaying() && state != PlayerState.Idle)
        {
            return;
        }

        AnimationClip[] attackClips = { swing1, swing2 };

        switch (state)
        {
            case PlayerState.Idle:
                an.SetBool("Idle", true);
                an.SetBool("Attack1", false);
                an.SetBool("Attack2", false);
                an.SetBool("Blocking", false);
                an.SetBool("Block", false);

                ac.Play(idleAnimation);
                break;

            case PlayerState.Swinging:
                int randomIndex = Random.Range(0, attackClips.Length);
                AnimationClip attackClip = attackClips[randomIndex];

                an.SetBool("Idle", false);
                an.SetBool("Blocking", false);
                an.SetBool("Block", false);

                if (attackClip == swing1)
                {
                    
                    an.SetBool("Attack1", true);
                    an.SetBool("Attack2", false);
                   
                }
                else 
                {
                    an.SetBool("Attack2", true);
                    an.SetBool("Attack1", false);
                }

                ac.Play(attackClip);
                break;

            case PlayerState.Blocking:
                an.SetBool("Idle", false);
                an.SetBool("Attack1", false);
                an.SetBool("Attack2", false);
                an.SetBool("Blocking", false);
                an.SetBool("Block", true);

                ac.Play(block);
                break;
        }

    }
}
