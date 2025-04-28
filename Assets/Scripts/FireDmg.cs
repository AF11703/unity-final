using UnityEngine;

public class FireDmg : MonoBehaviour
{
    Player player;
    Enemy enemy;

    bool hasHit = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            enemy.Damage(player, player.currentState == Player.PlayerState.Blocking);
            hasHit = true;

            Invoke(nameof(ResetHit), 0.5f);
        }
    }
    

    void ResetHit()
    {
        hasHit = false;
    }
}