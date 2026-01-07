using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;


public class Card : MonoBehaviour
{ 
    [SerializeField] private Image image;

    public Sprite hiddenIconSprite;
    public Sprite IconSprite;

    public bool isSelected;

    public CardController cardController;

    public void OnClicked() {
        cardController.SetSelectedCard(this);
    }

  public void SetIconSprite (Sprite sp) {
    IconSprite = sp;
  }

  public void ShowIcon() {
    Tween.Rotation(transform, new Vector3(0f,180f,0f) , 0.2f);
    Tween.Delay(0.1f , () => {
        image.sprite = IconSprite;
        
    });
   
    isSelected = true;
  }
  public void hiddenIcon() {
    Tween.Rotation(transform, new Vector3(0f,0f,0f) , 0.2f);
    Tween.Delay(0.1f , () => {
        image.sprite = hiddenIconSprite;
        isSelected = false;
        
    });
    
    
  }
}
