using System;
using UnityEngine;

public class AttackingScript : MonoBehaviour
{

    [SerializeField] _Character character;
    [SerializeField] Player player;

    [SerializeField] AudioClip enemyHitSound;
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip blockSound;
    AudioSource audioSource;
    
    AudioClip targetHitSound;
    string target;
    bool hasHit = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        target = character.gameObject.tag switch
        {
            "Player" => "Enemy",
            "Enemy" => "Player",
            _ => "Enemy"
        };

        targetHitSound = target == "Player" ? playerHitSound : enemyHitSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        Debug.Log($"Trigger entered with: {other.gameObject.name}");

        if (other.CompareTag(target))
        {
            _Character damagedCharacter = other.GetComponent<_Character>();
            bool isBlocking = false;

            Debug.Log($"{target} hit!");

            if (player != null)
            {
                isBlocking = player.currentState == Player.PlayerState.Blocking;
                character.Damage(damagedCharacter, isBlocking);
            }
            else
                character.Damage(damagedCharacter);

            if (isBlocking)
                audioSource.PlayOneShot(blockSound);
            else
            {
                audioSource.PlayOneShot(targetHitSound);
                audioSource.PlayOneShot(hitSound);
            }
               


            Debug.Log($"{target} Health: {damagedCharacter.getHealth()}");

            hasHit = true;

            Invoke(nameof(ResetHit), 0.5f);

            
            
        }
    }

    private void ResetHit()
    {
        hasHit = false;
    }
}
