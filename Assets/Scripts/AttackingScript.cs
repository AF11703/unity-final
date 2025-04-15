using System;
using UnityEngine;

public class AttackingScript : MonoBehaviour
{

   [SerializeField] _Character character;

    string target;

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
        Debug.Log($"Trigger entered with: {other.gameObject.name}");

        if (other.CompareTag(target))
        {
            _Character damagedCharacter = other.GetComponent<_Character>();
            Debug.Log($"{target} hit!");
            
            character.Damage(damagedCharacter);

            Debug.Log($"{target} Health: {damagedCharacter.getHealth()}");
            
        }
    }
}
