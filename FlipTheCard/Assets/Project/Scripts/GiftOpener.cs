using UnityEngine;

public class GiftOpener : MonoBehaviour
{
   public VictoryManager victoryManager;
   private static bool hasOpenedAnyGift = false;
   public void OpenGift()
   {
        if (hasOpenedAnyGift) return; 

            hasOpenedAnyGift = true;
        if(victoryManager != null)
        {
            victoryManager.OnGameWin(this.gameObject);
        }
   }
   public static void ResetGiftStatus()
    {
        hasOpenedAnyGift = false;
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
