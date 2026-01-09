using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;
    public LevelDataGame[] levelsData; 

    // Biến static này sẽ tự reset về 0 mỗi khi tắt game
    public static int HighestLevelReached = 0;

    void Start()
    {
        // KHÔNG DÙNG PlayerPrefs NỮA
        // int levelReached = PlayerPrefs.GetInt("LevelReached", 0);
        
        // DÙNG BIẾN STATIC
        int levelReached = HighestLevelReached;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i;
            TextMeshProUGUI buttonText = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = (i + 1).ToString();
            }

            Transform lockIcon = levelButtons[i].transform.Find("LockIcon");

            // So sánh với biến static
            if (i > levelReached)
            {
                levelButtons[i].interactable = false; 
                if (buttonText != null) buttonText.gameObject.SetActive(false);
                if (lockIcon != null) lockIcon.gameObject.SetActive(true);
            }
            else
            {
                levelButtons[i].interactable = true; 
                if (buttonText != null) buttonText.gameObject.SetActive(true);
                if (lockIcon != null) lockIcon.gameObject.SetActive(false);
            }

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