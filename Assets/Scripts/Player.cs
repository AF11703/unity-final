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

            if (currentState == PlayerState.Swinging && currentClip == swing)
            {
                var state = animancerComponent.States[swing];

                if (state.NormalizedTime >= 1f)
                {
                    currentState = PlayerState.Idle; // Reset to Idle after swing animation completes
                }
            }
        }
        else
        {
            Debug.Log("No animation is currently playing.");
        }



    }

    public void Heal()
    {
        Debug.Log("Healing the player");
        // Implement healing logic here
        setHealth(getHealth() + 20f); // Example healing logic
    }


    private void FixedUpdate()
    {
        PlayAnimation(animancerComponent, handAnimator, currentState);
    }


    public void OnAttack(InputAction.CallbackContext context) //used by InputSystem
    {

        if (context.performed && currentState != PlayerState.Swinging)
        {
            currentState = PlayerState.Swinging;
        }

    }

    public void OnBlock(InputAction.CallbackContext context) //used by InputSystem
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
