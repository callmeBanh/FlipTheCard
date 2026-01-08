using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    public void LoadStartGame()
    {
        SceneManager.LoadScene("StartGame");
    }
    public void LoadChooseLevel()
    {
        SceneManager.LoadScene("ChooseLevel");
    }

    public void LoadUserPlay()
    {
        Debug.Log("Đang vào màn chơi: UserPlay");
        SceneManager.LoadScene("UserPlay"); 
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("UserPlay");

        
        Debug.Log("Đang tải lại màn chơi...");
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