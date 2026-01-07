using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    public void LoadStartGame()
    {
        SceneManager.LoadScene("StartGame");
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
