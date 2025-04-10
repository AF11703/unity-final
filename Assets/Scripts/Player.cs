using UnityEngine;
using UnityEngine.InputSystem;
using Animancer;


public class Player : _Character
{
    [SerializeField] AnimancerComponent animancerComponent;

    [Header("Animations")]
    [SerializeField] AnimationClip idleAnimation;
    [SerializeField] AnimationClip swing;
    [SerializeField] AnimationClip block;

    float attackStart = 0f;

    Animator handAnimator;

    public enum PlayerState
    {
        Idle,
        Swinging,
        Blocking,
        Jumping
    }



    PlayerState currentState = PlayerState.Idle;

    
    private void Awake()
    {
        handAnimator = animancerComponent.Animator;
    }


    private void Update()
    {
        Debug.Log($"Current state is: {currentState}");

        if (animancerComponent.IsPlaying())
        {
            AnimationClip currentClip = animancerComponent.States.Current.Clip;
            Debug.Log($"Current animation clip is: {currentClip.name}");
        }
        else
        {
            Debug.Log("No animation is currently playing.");
        }
    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.Swinging)
        {
            attackStart += Time.deltaTime;
        }
        else
        {
            attackStart = 0f;
        }
        
        /*
        if (currentState != PlayerState.Blocking && animancerComponent.IsPlaying(block))
        {
            animancerComponent.Stop();
        }
        */

        PlayAnimation(animancerComponent, handAnimator, currentState);
    }


    void OnAttack() //used by InputSystem
    {
       
        Debug.Log("Attacking");
        currentState = PlayerState.Swinging;
    }

    void OnBlock(InputAction.CallbackContext context) //used by InputSystem
    {
        if (context.performed)
        {
            Debug.Log("Blocking");
            currentState = PlayerState.Blocking;
        }
        else if (context.canceled)
        {
            Debug.Log("Unblocking");
            currentState = PlayerState.Idle;
        }
    }

    void PlayAnimation(AnimancerComponent ac, Animator an, PlayerState state)
    {

        /*
        if (ac.IsPlaying() && state != PlayerState.Idle)
        {
            return;
        }
        */

        switch (state)
        {
            case PlayerState.Idle:
                an.SetBool("Idle", true);
                an.SetBool("Attack1", false);
                an.SetBool("Block", false);

                

                ac.Play(idleAnimation);
                break;

            case PlayerState.Swinging:
                an.SetBool("Idle", false);
                an.SetBool("Block", false);
                an.SetBool("Attack1", true);

                ac.Play(swing);
              

                break;

            case PlayerState.Blocking:
                an.SetBool("Idle", false);
                an.SetBool("Attack1", false);
                an.SetBool("Block", true);

                

                ac.Play(block);
                break;
        }

    }
}
