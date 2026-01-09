using UnityEngine;

public class UIButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void OnPause()
    {
        ButtonManager.Instance.PauseGame(); 
    }

    public void OnResume()
    {
        ButtonManager.Instance.ResumeGame(); 
    }

    public void OnToggleSound()
    {
        ButtonManager.Instance.ToggleSound(); 
    }
}
