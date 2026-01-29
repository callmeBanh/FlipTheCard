using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;

    private bool isPaused = false;
    private bool isSoundOn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        if (AudiosManager.Instance != null)
            AudiosManager.Instance.ToggleMusic(isSoundOn);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public bool IsSoundOn() => isSoundOn;
}
