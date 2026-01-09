using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;
    public LevelDataGame[] levelsData; 

    void Start()
    {
        // 1. Lấy dữ liệu màn chơi cao nhất đã đạt được từ máy (Mặc định là 0 - tức là Level 1)
        int levelReached = PlayerPrefs.GetInt("LevelReached", 0);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i;
            TextMeshProUGUI buttonText = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = (i + 1).ToString();
            }

            // --- PHẦN SỬA ĐỔI: LOGIC KHÓA/MỞ LEVEL ---
            
            // Tìm icon ổ khóa trong nút (bạn phải đặt tên Gameobject con là "LockIcon")
            Transform lockIcon = levelButtons[i].transform.Find("LockIcon");

            if (i > levelReached)
            {
                // TRƯỜNG HỢP BỊ KHÓA (Index của nút lớn hơn cấp độ đã đạt được)
                levelButtons[i].interactable = false; // Không cho bấm
                
                // Ẩn số đi cho đẹp (tùy chọn)
                if (buttonText != null) buttonText.gameObject.SetActive(false);

                // Hiện ổ khóa
                if (lockIcon != null) lockIcon.gameObject.SetActive(true);
            }
            else
            {
                // TRƯỜNG HỢP ĐƯỢC MỞ
                levelButtons[i].interactable = true; // Cho phép bấm
                
                // Hiện số
                if (buttonText != null) buttonText.gameObject.SetActive(true);

                // Ẩn ổ khóa
                if (lockIcon != null) lockIcon.gameObject.SetActive(false);
            }
            // ------------------------------------------

            levelButtons[i].onClick.AddListener(() => OnLevelSelected(levelIndex));
        }
    }

    void OnLevelSelected(int levelIndex)
    {
        Debug.Log("Level " + levelIndex + " selected.");
        CardController.currentLevel = levelIndex;
        SceneManager.LoadScene("UserPlay");
    }

    // ... (Phần LoadNextLevel và Update giữ nguyên hoặc xóa nếu không dùng ở đây)
}