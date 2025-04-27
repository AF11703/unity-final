using UnityEngine;

public class BlockAudioTrigger : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip blockSound;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            audioSource.PlayOneShot(blockSound);
        }

    }
}