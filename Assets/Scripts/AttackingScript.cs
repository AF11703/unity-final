using System;
using UnityEngine;

public class AttackingScript : MonoBehaviour
{

    [SerializeField] _Character character;
    [SerializeField] Player player; 
    string target;
    bool hasHit = false;

    private void Start()
    {
        target = character.gameObject.tag switch
        {
            "Player" => "Enemy",
            "Enemy" => "Player",
            _ => "Enemy"
        };

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        Debug.Log($"Trigger entered with: {other.gameObject.name}");

        if (other.CompareTag(target))
        {
            _Character damagedCharacter = other.GetComponent<_Character>();

            
            Debug.Log($"{target} hit!");

            if (player != null)
                character.Damage(damagedCharacter, player.currentState == Player.PlayerState.Blocking);
            else
                character.Damage(damagedCharacter);


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
