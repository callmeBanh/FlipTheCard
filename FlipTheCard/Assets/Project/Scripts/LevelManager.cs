using UnityEngine;
using UnityEngine.UI;
using TMPro;// thư viện điều khiểm TextMeshPro
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Danh sách các nút bấm được sử dụng trong LevelManager
    public Button[] levelButtons;
    public LevelDataGame[] levelsData; // Mảng dữ liệu các level
    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i; // Tạo biến tạm để tránh lỗi closure trong AddListener
            
            // Tự động gán số lên nút bấm từ Data
            TextMeshProUGUI buttonText = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null && i < levelsData.Length)
            {
                buttonText.text = levelsData[i].levelId.ToString();
            }

            // Gán sự kiện chuyển Scene
            levelButtons[i].onClick.AddListener(() => OnLevelSelected(levelsData[index]));
        }
    }

    void OnLevelSelected(LevelDataGame data)
    {
        // Lưu số cặp bài và thời gian của màn đó vào bộ nhớ
        PlayerPrefs.SetInt("PairCount", data.pairCount);
        PlayerPrefs.SetFloat("TimeLimit", data.timeLimit);
        
        SceneManager.LoadScene("UserPlay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
