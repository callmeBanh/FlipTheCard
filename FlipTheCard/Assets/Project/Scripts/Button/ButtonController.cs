using UnityEngine;
using UnityEngine.SceneManagement; // Dùng để chuyển cảnh Exit/Play

public class ButtonController : MonoBehaviour
{
    // Hàm này dùng cho nút Loa (Sound)
    public void OnClickToggleSound()
    {
        // Gọi qua Instance (Cái duy nhất còn sống sót)
        if (ButtonManager.Instance != null)
        {
            ButtonManager.Instance.ToggleSound();
        }
    }

    // Hàm này dùng cho nút Pause
    public void OnClickPause()
    {
        if (ButtonManager.Instance != null)
        {
            ButtonManager.Instance.PauseGame();
        }
    }

    // Hàm này dùng cho nút Resume
    public void OnClickResume()
    {
        if (ButtonManager.Instance != null)
        {
            ButtonManager.Instance.ResumeGame();
        }
    }

    // Hàm này dùng cho nút Play (Chuyển sang màn chơi)
    public void OnClickPlayGame()
    {
        // Thay "ChooseLevel" bằng tên màn chọn level của bạn
        SceneManager.LoadScene("ChooseLevel"); 
    }

    // Hàm này dùng cho nút Exit
    public void OnClickExit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}