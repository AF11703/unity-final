using UnityEngine;

public class FireDmg : MonoBehaviour
{
     Player player;
     Enemy enemy;

    bool hasHit = false;

    AudioSource audioSource;
    [SerializeField] AudioClip fireballHitSound;
    [SerializeField] AudioClip fireballSound;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        Debug.Log($"Player: {player.gameObject.name}");
        Debug.Log($"Enemy: {enemy.gameObject.name}");

        audioSource.PlayOneShot(fireballSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by fireball!");

            enemy.Damage(player, player.currentState == Player.PlayerState.Blocking);
            hasHit = true;

            audioSource.PlayOneShot(fireballHitSound);
           
            Invoke(nameof(ResetHit), 0.5f);
        }
    }
    

    void ResetHit()
    {
        hasHit = false;
    }
}