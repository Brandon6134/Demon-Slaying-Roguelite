using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource sfxSource;

    void Awake()
    {
        Instance = this; //create easy global access to this script by referencing AudioManager.Instance
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip,volume);
    }

}
