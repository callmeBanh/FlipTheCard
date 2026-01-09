using UnityEngine;
using UnityEngine.SceneManagement; // Thêm thư viện này để quản lý Scene

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
        else
        {
            Destroy(gameObject); // Hủy bản sao thừa nếu quay lại màn Start
        }
    }

    // --- PHẦN MỚI: Tự động đồng bộ âm thanh khi chuyển cảnh ---
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Mỗi khi vào màn mới, áp dụng lại cài đặt âm thanh hiện tại
        ApplySoundSetting();
    }
    // ----------------------------------------------------------

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

    public void ToggleSound()
    {
        Debug.Log("Đã bấm nút tắt nhạc!"); // <--- THÊM DÒNG NÀY
        
        isSoundOn = !isSoundOn;
        if (isSoundOn)
        {
            AudioListener.volume = 1f; // Mở âm thanh toàn cầu
        }
        else
        {
            AudioListener.volume = 0f; // Tắt âm thanh toàn cầu
        }
    }

    // Tách hàm xử lý âm thanh ra riêng để tái sử dụng
    private void ApplySoundSetting()
    {
        if (isSoundOn)
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = 0f;
        }

        // Gọi sang AudiosManager nếu có
        if (AudiosManager.Instance != null)
        {
            AudiosManager.Instance.ToggleMusic(isSoundOn);
        }
    }

    public bool IsPaused() => isPaused;
    public bool IsSoundOn() => isSoundOn;
}