using UnityEngine;

public class MenuAudioScript : MonoBehaviour
{
    [SerializeField]public AudioSource menuAudio;

    [SerializeField] public AudioClip menuBgAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuAudio.clip = menuBgAudio;
        menuAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
