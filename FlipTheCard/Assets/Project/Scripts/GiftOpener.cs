using UnityEngine;

public class GiftOpener : MonoBehaviour
{
   public VictoryManager victoryManager;

   public void OpenGift()
   {
       victoryManager.OnGameWin();
       gameObject.SetActive(false);
   }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
