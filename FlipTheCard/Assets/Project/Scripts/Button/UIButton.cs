using UnityEngine;
using UnityEngine.UI; // Bắt buộc có để chỉnh sửa hình ảnh Button

public class UIButton : MonoBehaviour
{
    [Header("Cài đặt Âm thanh (Chỉ dành cho nút Sound)")]
    [SerializeField] private Image buttonIcon; // Kéo cái Image của nút vào đây
    [SerializeField] private Sprite soundOnSprite;  // Hình loa đang bật
    [SerializeField] private Sprite soundOffSprite; // Hình loa đang tắt

    private bool isSoundOn = true; // Biến theo dõi trạng thái hiện tại

    private void Start()
    {
        // Khi game bắt đầu, kiểm tra xem nhạc đang bật hay tắt để hiện hình đúng
        if (AudiosManager.Instance != null)
        {
            // Lấy trạng thái từ AudioSource của Manager (nếu truy cập được)
            // Hoặc mặc định là true nếu game mới vào
            UpdateSoundIcon();
        }
    }

    public void OnPause()
    {
        // Kiểm tra xem Time có đang chạy không để Pause
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0; // Dừng game
            // Nếu bạn có Panel Pause, hãy code bật nó lên ở đây
            Debug.Log("Game Paused");
        }
    }

    public void OnResume()
    {
        Time.timeScale = 1; // Chạy tiếp game
        Debug.Log("Game Resumed");
    }

    public void OnToggleSound()
    {
        // QUAN TRỌNG: Gọi trực tiếp AudiosManager.Instance để tránh lỗi Null
        if (AudiosManager.Instance != null)
        {
            isSoundOn = !isSoundOn; // Đảo ngược trạng thái (Bật -> Tắt, Tắt -> Bật)

            // Gọi hàm bên AudiosManager để thực sự tắt/bật tiếng
            AudiosManager.Instance.ToggleMusic(isSoundOn);

            // Đổi hình ảnh nút
            UpdateSoundIcon();
        }
    }

    // Hàm phụ để đổi hình ảnh
    void UpdateSoundIcon()
    {
        if (buttonIcon != null && soundOnSprite != null && soundOffSprite != null)
        {
            if (isSoundOn)
            {
                buttonIcon.sprite = soundOnSprite;
            }
            else
            {
                buttonIcon.sprite = soundOffSprite;
            }
        }
    }
}