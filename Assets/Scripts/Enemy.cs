using UnityEngine;
using UnityEngine.AI;

using Animancer;
public class Enemy : _Character
{
    [SerializeField] AnimancerComponent animancerComponent;

    [Header("Animations")]
    [SerializeField] AnimationClip attackAnimation;
    [SerializeField] AnimationClip chaseAnimation;

    NavMeshAgent navMeshAgent;

    [SerializeField] GameObject player;
    [SerializeField] GameObject rotateObj;
    enum EnemyState
    {
        Attacking,
        Chasing,
        Reset
    }

    EnemyState currentState = EnemyState.Chasing;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        setSpeed(1f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent.speed = getSpeed();

    }


    private void FixedUpdate()
    {
        PlayAnimation(animancerComponent, currentState);
    }


    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        
        
       
        rotateObj.transform.LookAt(player.transform.position);
        
        
        
       


        if (distanceToPlayer <= 1.5f)
        {
            if (currentState != EnemyState.Attacking)
            {
                currentState = EnemyState.Attacking;
            }
            
            

            return;
        }
        else
        {
            if (currentState != EnemyState.Chasing)
            {
                currentState = EnemyState.Chasing;
            }
            navMeshAgent.SetDestination(player.transform.position);
        }

                

    }

    void PlayAnimation(AnimancerComponent ac, EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Attacking:
                ac.Play(attackAnimation);
                break;
            
            case EnemyState.Chasing:
                if (!ac.IsPlaying(chaseAnimation))
                {
                    ac.Play(chaseAnimation);
                }
                
                break;
            
            default: break;
        }
    }
}
