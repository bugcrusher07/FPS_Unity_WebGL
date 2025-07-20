using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource gunShot;
    [SerializeField] AudioSource enemySounds;
    [SerializeField] AudioSource bgSounds;

    [SerializeField] AudioClip gunShotSound;
    [SerializeField] AudioClip enemySwingSound;
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] AudioClip bgSound1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunShot.clip = gunShotSound;
        bgSounds.clip = bgSound1;
        bgSounds.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EnemyDeathSound()
    {
        enemySounds.clip = enemyDeathSound;
        enemySounds.Play();
    }

    public void EnemySwingSound()
    {
        enemySounds.clip = enemySwingSound;
        enemySounds.Play();
    }

    public void PlayGunSound()
    {
        gunShot.Play();
    }
}
