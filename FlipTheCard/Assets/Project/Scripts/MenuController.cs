using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    public void LoadChooseLevel()
    {
        SceneManager.LoadScene("ChooseLevel");
    }

    public void LoadUserPlay()
    {
        SceneManager.LoadScene("UserPlay");
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("UserPlay");

        
        Debug.Log("Đang tải lại màn chơi...");
    }

    public void LoadStartGame()
    {
        SceneManager.LoadScene("StartGame");
    }

    public void LoadNextLevel()
    {
        CardController.currentLevel++; 
        // Kiểm tra xem có vượt quá số lượng màn chơi bạn có không (vd: 12 màn)
        if (CardController.currentLevel < 12) 
        {
            SceneManager.LoadScene("UserPlay"); // Load lại chính Scene chơi game
        }
        else
        {
            Debug.Log("Hết màn rồi!");
            SceneManager.LoadScene("ChooseLevel"); // Quay về màn chọn level
        }
    }
    public void QuitGame()
    {
        Debug.Log("Đang thoát game..."); 
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}