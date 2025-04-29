using UnityEngine;

public class CalmSoundPlayer : MonoBehaviour
{
    [SerializeField] GameObject enemyGo;

   

    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if (!audioSource.isPlaying && !enemyGo.activeInHierarchy)
        {
            audioSource.Play();
        } 
       else if (audioSource.isPlaying && enemyGo.activeInHierarchy)
        {
            audioSource.Stop();
        }


        float enemyHP = enemyGo.GetComponent<Enemy>().getHealth();

        if (enemyHP <= 0f)
        {
            audioSource.Play();
        }
    }
}
