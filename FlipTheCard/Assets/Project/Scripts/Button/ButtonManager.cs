using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;

    private bool isPaused = false;
    private bool isSoundOn = true;

    private void Awake()
    {
        // Singleton - 1 class khai báo duy nhất dung chung cho tất cả

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    // ================= SOUND =================
    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
       if (isSoundOn)
        {
            AudioListener.volume = 1f;
           
        }
        else
        {
            AudioListener.volume = 0f;
          
        }

        if(AudiosManager.Instance != null)
        {
            AudiosManager.Instance.ToggleMusic(isSoundOn);
        }
    }

    // (Optional) để scene khác kiểm tra trạng thái
    public bool IsPaused() => isPaused;
    public bool IsSoundOn() => isSoundOn;
}
