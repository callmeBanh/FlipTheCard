using UnityEngine;

public class AudiosManager : MonoBehaviour
{
    public static AudiosManager Instance;
    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = true;
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMusic(bool isOn)
    {
        audioSource.mute = !isOn;
    }
}
