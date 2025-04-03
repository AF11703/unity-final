using UnityEngine;
using UnityEngine.InputSystem;
using Animancer;

public class Player : _Character
{
    [SerializeField] AnimancerComponent animancerComponent;

    [Header("Animations")]
    [SerializeField] AnimationClip idleAnimation;
    [SerializeField] AnimationClip swing1;
    [SerializeField] AnimationClip swing2;
    [SerializeField] AnimationClip block;

    public enum PlayerState
    {
        Idle,
        Swinging,
        Blocking,
        Jumping
    }

    PlayerState currentState = PlayerState.Idle;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
       if (!Keyboard.current.anyKey.isPressed)
            currentState = PlayerState.Idle;
       

       if (Keyboard.current.spaceKey.isPressed)
            currentState = PlayerState.Jumping;

       if (Mouse.current.leftButton.isPressed)
            currentState = PlayerState.Swinging;


       if (Mouse.current.rightButton.isPressed)
            currentState = PlayerState.Blocking;
           


        PlayAnimation(animancerComponent, currentState);
    }

    public void Heal(float healAmount) { 
       base.setHealth(base.getHealth() + healAmount);
    }

    public void PlayAnimation(AnimancerComponent anim, PlayerState state, float time = 0, float normTime = 0f, float speed = 1f)
    {
        AnimationClip[] attackAnims = { swing1, swing2 };
        bool alreadySwinging = anim.IsPlayingClip(attackAnims[0]) || anim.IsPlayingClip(attackAnims[1]);
        switch (state)
        {
            case PlayerState.Idle:
                if (alreadySwinging)
                    return;
                anim.Play(idleAnimation);
                break;
            case PlayerState.Swinging:
                if (alreadySwinging)
                    return;
                
                // Randomly choose one of the attack animations
                var randomIndex = Random.Range(0, attackAnims.Length);
                
                if (anim.IsPlayingClip(attackAnims[0]) || anim.IsPlayingClip(attackAnims[1]))
                {
                    
                    // If an attack animation is already playing, return
                    return;
                }
  
                anim.Play(attackAnims[randomIndex]);
                break;
            
            case PlayerState.Blocking:
                anim.Play(block);
                break;
        }
    }
}
