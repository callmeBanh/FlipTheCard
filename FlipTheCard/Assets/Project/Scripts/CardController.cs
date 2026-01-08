// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using PrimeTween;
// using TMPro;


// public class CardController : MonoBehaviour
// {
//     [SerializeField] private Card cardPrefab;
//     [SerializeField] private Transform gridTrasform;
//     [SerializeField] private Sprite[] sprites;
//     private List<Sprite> spritePairs;
//     Card firstSelected;
//     Card SecondSelected;

//     int matchCounts;

//     // Time game
//     [SerializeField] private TMP_Text timeText;
//     public float timePlayingLimit = 60f;
//     private float timePlaying;
//     private bool gameIsPlaying = false;
    


//     private void Start()
//     {
//         PrepareSprites();
//         CreateCard();
//         timePlaying = timePlayingLimit;
//         gameIsPlaying = true;
//     }

//     void Update()
//     {
//         if(gameIsPlaying)
//         {
//             timePlaying -= Time.deltaTime;
//             updateTimeUI();
//         }
//         if(timePlaying <= 0)
//         {
//             timeOut();
//         }
//     } 

//     void timeOut()
//     {
//         gameIsPlaying = false;
//         timePlaying = 0;
//         updateTimeUI();
//         Debug.Log("Time Up! You Lose!");
//     }  

//     void updateTimeUI()
//     {
//         int seconds = Mathf.CeilToInt(timePlaying);
//         timeText.text = seconds.ToString();
//     }
    
//     private void PrepareSprites()
//     {
//         spritePairs = new List<Sprite>();
//         for (int i = 0; i < sprites.Length; i++)
//         {
//             // add each sprite twice to create pairs
//             spritePairs.Add(sprites[i]);
//             spritePairs.Add(sprites[i]);
//         }
//         Shuffle(spritePairs);
//     }

//     void CreateCard()
//     {
//         for(int i = 0; i < spritePairs.Count; i++)
//         {
//             Card newCard = Instantiate(cardPrefab, gridTrasform);
//             newCard.SetIconSprite(spritePairs[i]);
//             newCard.cardController = this;
            
//         }
//     }

//     public void SetSelectedCard(Card selectedCard)
//     {
//         if(!gameIsPlaying)
//         {
//             return;
//         }

//         if(selectedCard.isSelected == false)
//         {
//             selectedCard.ShowIcon();
//             if(firstSelected == null)
//             {
//                 firstSelected = selectedCard;
//                 return;
//             }
//             if(SecondSelected == null)
//             {
//                 SecondSelected = selectedCard;
//                 StartCoroutine(CheckMatch(firstSelected, SecondSelected));
//                 firstSelected = null;
//                 SecondSelected = null;
//             }
//         }
       
//     }

//     IEnumerator CheckMatch(Card a , Card b)
//     {
//         yield return new WaitForSeconds(0.3f);
//         if(a.IconSprite == b.IconSprite)
//         {
//             // it's a match, keep them shown 
//             matchCounts++;
//             if(matchCounts >= spritePairs.Count / 2)
//             {
//                 Debug.Log("You Win!");
//                 gameIsPlaying = false;

//             }

//         }
//         else
//         {
//             a.hiddenIcon();
//             b.hiddenIcon();
//         }
//     }

//     void Shuffle(List<Sprite> list)
// {
//     for (int i = 0; i < list.Count; i++)
//     {
//         int rnd = Random.Range(i, list.Count);
//         Sprite temp = list[i];
//         list[i] = list[rnd];
//         list[rnd] = temp;
//     }
// }


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;

public class CardController : MonoBehaviour
{
    [Header("Cấu hình Thẻ bài")]
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform gridTrasform;
    [SerializeField] private Sprite[] sprites;
    
    [Header("Giao diện (UI)")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text turnText;

    [Header("Luật chơi")]
    public float timePlayingLimit = 60f;
    public int maxTurnsLimit = 20; // Giới hạn số lượt lật (ví dụ: 20 cặp)

    private List<Sprite> spritePairs;
    private Card firstSelected;
    private Card SecondSelected;
    private int matchCounts;
    private float timePlaying;
    private int turnsCount;
    private bool gameIsPlaying = false;
    private bool isBusy = false; // Ngăn click khi đang so sánh

    private void Start()
    {
        PrepareSprites();
        CreateCard();
        
        // Khởi tạo thông số
        timePlaying = timePlayingLimit;
        turnsCount = 0;
        updateTimeUI();
        updateTurnUI();
        
        gameIsPlaying = true;
    }

    void Update()
    {
        if (gameIsPlaying)
        {
            // Xử lý đếm ngược thời gian
            timePlaying -= Time.deltaTime;
            updateTimeUI();

            if (timePlaying <= 0)
            {
                GameOver("Hết giờ rồi! Bạn đã thua.");
            }
        }
    }

    // --- QUẢN LÝ LƯỢT CHƠI ---
    public void SetSelectedCard(Card selectedCard)
    {
        // Chặn click nếu: Game dừng, đang xử lý, hoặc card đã chọn rồi
        if (!gameIsPlaying || isBusy || selectedCard.isSelected) return;

        selectedCard.ShowIcon();

        if (firstSelected == null)
        {
            firstSelected = selectedCard;
        }
        else
        {
            SecondSelected = selectedCard;
            
            // Mỗi lần lật lá thứ 2 tính là 1 lượt
            turnsCount++;
            updateTurnUI();

            StartCoroutine(CheckMatch(firstSelected, SecondSelected));
            
            // Reset biến tạm ngay để tránh lỗi click nhanh, 
            // nhưng tham chiếu vẫn được giữ trong Coroutine CheckMatch
            firstSelected = null;
            SecondSelected = null;
        }

        // Kiểm tra nếu hết lượt
        if (turnsCount >= maxTurnsLimit && gameIsPlaying)
        {
            // Đợi một chút để người chơi thấy lá bài cuối rồi mới báo thua
            Tween.Delay(0.5f, () => {
                if (matchCounts < spritePairs.Count / 2)
                    GameOver("Hết lượt lật rồi! Bạn đã thua.");
            });
        }
    }

    IEnumerator CheckMatch(Card a, Card b)
    {
        isBusy = true; // Khóa click
        yield return new WaitForSeconds(0.6f); // Đợi hiệu ứng lật bài của PrimeTween

        if (a.IconSprite == b.IconSprite)
        {
            // Khớp cặp
            matchCounts++;
            // Vô hiệu hóa Button để không click lại được nữa
            a.GetComponent<UnityEngine.UI.Button>().interactable = false;
            b.GetComponent<UnityEngine.UI.Button>().interactable = false;

            if (matchCounts >= spritePairs.Count / 2)
            {
                GameOver("Chúc mừng! Bạn đã thắng!");
            }
        }
        else
        {
            // Không khớp thì úp lại
            a.hiddenIcon();
            b.hiddenIcon();
        }

        isBusy = false; // Mở khóa click
    }

    // --- CÁC HÀM HỖ TRỢ ---
    void GameOver(string message)
    {
        gameIsPlaying = false;
        Debug.Log(message);
        // Tại đây bạn có thể kích hoạt Panel Thắng/Thua của mình
    }

    void updateTimeUI()
    {
        if (timeText != null)
        {
            int seconds = Mathf.CeilToInt(Mathf.Max(0, timePlaying));
            timeText.text = seconds.ToString();
        }
    }

    void updateTurnUI()
    {
        if (turnText != null)
        {
            turnText.text = turnsCount + " / " + maxTurnsLimit;
        }
    }

    private void PrepareSprites()
    {
        spritePairs = new List<Sprite>();
        for (int i = 0; i < sprites.Length; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }
        Shuffle(spritePairs);
    }

    void CreateCard()
    {
        foreach (Transform child in gridTrasform) Destroy(child.gameObject);

        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card newCard = Instantiate(cardPrefab, gridTrasform);
            newCard.SetIconSprite(spritePairs[i]);
            newCard.cardController = this;
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            Sprite temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}
