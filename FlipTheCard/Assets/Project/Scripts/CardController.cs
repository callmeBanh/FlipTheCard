using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine.SceneManagement;

public class CardController : MonoBehaviour
{
    // code cho từng màng
    [SerializeField] private LevelDataGame[] levels;
     public static int currentLevel = 0;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform gridTrasform;
    [SerializeField] private Sprite[] sprites;
    private List<Sprite> spritePairs;
    Card firstSelected;
    Card SecondSelected;

    int matchCounts;

    // Time game
    [SerializeField] private TMP_Text timeText;
    public float timePlayingLimit = 60f;
    private float timePlaying;
    private bool gameIsPlaying = false;

    // am thanh
    private AudioSource audioSource;
    public AudioClip flipSound;
    public AudioClip winSound;
    


    private void Start()
    {
            // Kiểm tra xem danh sách levels có trống không và index có hợp lệ không
        if (levels != null && currentLevel >= 0 && currentLevel < levels.Length)
        {
            LevelDataGame levelData = levels[currentLevel];
            timePlaying = levelData.timeLimit;
            PrepareSprites(levelData.pairCount);
            CreateCard();
            audioSource = GetComponent<AudioSource>();
            gameIsPlaying = true;
        }
        else
        {
            // Thông báo lỗi cụ thể ra Console để dễ sửa
            Debug.LogError($"Lỗi: Index {currentLevel} không tồn tại trong danh sách Levels (Size hiện tại là {levels?.Length ?? 0})");
            gameIsPlaying = false; 
        }
    }

    void Update()
    {
        if(gameIsPlaying)
        {
            timePlaying -= Time.deltaTime;
            updateTimeUI();
        }
        if(timePlaying <= 0)
        {
            timeOut();
        }
    } 

    void timeOut()
    {
        gameIsPlaying = false;
        timePlaying = 0;
        updateTimeUI();
        Debug.Log("Time Up! You Lose!");
    }  

    void updateTimeUI()
    {
        int seconds = Mathf.CeilToInt(timePlaying);
        timeText.text = seconds.ToString();
    }
    
    private void PrepareSprites(int pairNeed)
    {
        spritePairs = new List<Sprite>();

        // trộn danh sách tất cả các ảnh
        List<Sprite> tempPool = new List<Sprite>(sprites);
        Shuffle(tempPool);
        for (int i = 0; i < pairNeed; i++)
        {
            // add each sprite twice to create pairs
            spritePairs.Add(tempPool[i]);
            spritePairs.Add(tempPool[i]);
        }
        Shuffle(spritePairs);
    }

    void CreateCard()
    {
        for(int i = 0; i < spritePairs.Count; i++)
        {
            Card newCard = Instantiate(cardPrefab, gridTrasform);
            newCard.SetIconSprite(spritePairs[i]);
            newCard.cardController = this;
            
        }
    }

    public void SetSelectedCard(Card selectedCard)
    {
        if(!gameIsPlaying)
        {
            return;
        }

        if(selectedCard.isSelected == false)
        {
            selectedCard.ShowIcon();
            if(firstSelected == null)
            {
                firstSelected = selectedCard;
                audioSource.PlayOneShot(flipSound);
                return;
            }
            if(SecondSelected == null)
            {
                SecondSelected = selectedCard;
                StartCoroutine(CheckMatch(firstSelected, SecondSelected));
                audioSource.PlayOneShot(flipSound);
                firstSelected = null;
                SecondSelected = null;
            }
        }
       
    }

    IEnumerator CheckMatch(Card a , Card b)
    {
        yield return new WaitForSeconds(0.3f);
        if(a.IconSprite == b.IconSprite)
        {
            // it's a match, keep them shown 
            matchCounts++;
            if(matchCounts >= spritePairs.Count / 2)
            {
                Debug.Log("You Win!");
                gameIsPlaying = false;
                SceneManager.LoadScene("Result");
            }

        }
        else
        {
            a.hiddenIcon();
            b.hiddenIcon();
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



