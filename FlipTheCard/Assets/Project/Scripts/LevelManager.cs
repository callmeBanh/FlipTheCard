using UnityEngine;
using UnityEngine.UI;
using TMPro;// thư viện điều khiểm TextMeshPro
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Danh sách các nút bấm được sử dụng trong LevelManager
    public Button[] levelButtons;
    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
           TextMeshProUGUI buttonText = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            
            if (buttonText != null)
            {
                // Đánh số bắt đầu từ 1 (i + 1)
                buttonText.text = (i + 1).ToString();
            }

            int levelIndex = i + 1; 
            levelButtons[i].onClick.AddListener(() => OnLevelSelected(levelIndex));
        }
    }

    void OnLevelSelected(int levelIndex)
    {
        Debug.Log("Level " + levelIndex + " selected.");
        // // Tải cảnh tương ứng với cấp độ đã chọn
        // SceneManager.LoadScene("Level" + levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
