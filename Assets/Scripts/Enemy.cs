using UnityEngine;
using UnityEngine.AI;

using Animancer;
public class Enemy : _Character
{
    NavMeshAgent navMeshAgent;
    
    [SerializeField] float meleeAttackRange = 1.5f;
    [SerializeField] float shootingRange = 10f;
    

    [SerializeField] AnimancerComponent animancerComponent;

    [Header("Animations")]
    [SerializeField] AnimationClip attackAnimation;
    [SerializeField] AnimationClip chaseAnimation;
    [SerializeField] AnimationClip fireballAnimation;
    
    [SerializeField] GameObject weaponHitBox;
    [SerializeField] GameObject player;
    [SerializeField] GameObject rotateObj;
    [SerializeField] GameObject fireballPrefab;

    [SerializeField] GameObject handPos;
    enum EnemyState
    {
        Attacking,
        Shooting,
        Chasing,
    }

    EnemyState currentState = EnemyState.Chasing;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        weaponHitBox.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent.speed = getSpeed();

    }


    private void FixedUpdate()
    {
        if (animancerComponent.IsPlaying(attackAnimation))
        {
            navMeshAgent.isStopped = true;
            weaponHitBox.SetActive(true);

        }
        else
        {
            navMeshAgent.isStopped = false;
            weaponHitBox.SetActive(false);
        }
        
        PlayAnimation(animancerComponent, currentState);
        
    }


    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        
        
       
        rotateObj.transform.LookAt(player.transform.position);






        if (distanceToPlayer <= meleeAttackRange)
        {
            currentState = EnemyState.Attacking;
        }
        else if (distanceToPlayer <= shootingRange && distanceToPlayer > meleeAttackRange + 3f)
        {
            currentState = EnemyState.Shooting;
        }
        else 
        {
            currentState = EnemyState.Chasing;
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
            
            case EnemyState.Shooting:
                ac.Play(fireballAnimation);

                if (!GameObject.FindGameObjectWithTag("Fireball"))
                {
                    Invoke(nameof(ShootFireBall), 0.3f);
                }

                

                break;

            case EnemyState.Chasing:
                weaponHitBox.SetActive(false);
                if (!ac.IsPlaying(chaseAnimation))
                {
                    ac.Play(chaseAnimation);
                }
                
                break;
            
            default: break;
        }
    }

    private void ShootFireBall()
    {
        GameObject fireball = Instantiate(fireballPrefab, handPos.transform.position, Quaternion.identity);
        fireball.transform.LookAt(player.transform.position);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.AddForce((player.transform.position - transform.position).normalized * 10f, ForceMode.Impulse);

        Destroy(fireball, 2f);
    }

    
}
