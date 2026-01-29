using UnityEngine;
using UnityEngine.UI; // Bắt buộc có để chỉnh sửa hình ảnh Button

public class UIButton : MonoBehaviour
{
     public void OnToggleSound()
    {
        if (ButtonManager.Instance == null)
        {
            Debug.LogError("ButtonManager.Instance == null");
            return;
        }

        ButtonManager.Instance.ToggleSound();
    }

    public void OnPauseGame()
    {
        if (ButtonManager.Instance == null)
        {
            Debug.LogError("ButtonManager.Instance == null");
            return;
        }

        ButtonManager.Instance.PauseGame();
    }

    public void OnResumeGame()
    {
        if (ButtonManager.Instance == null)
        {
            Debug.LogError("ButtonManager.Instance == null");
            return;
        }

        ButtonManager.Instance.ResumeGame();
    }
}
