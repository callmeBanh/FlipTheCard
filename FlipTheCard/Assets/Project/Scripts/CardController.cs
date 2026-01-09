using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine.SceneManagement;

public class CardController : MonoBehaviour
{
    // ... Khai báo biến giữ nguyên ...
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

    private void Start()
    {
        // ... (Code Start giữ nguyên) ...
        if (levels != null && currentLevel < levels.Length)
        {
            LevelDataGame levelData = levels[currentLevel];
            timePlaying = levelData.timeLimit;
            if(levelData.pairCount > 0)
            {
                PrepareSprites(levelData.pairCount);
                CreateCard();
                audioSource = GetComponent<AudioSource>();
                gameIsPlaying = true;
                Debug.Log($"Bắt đầu màn {currentLevel + 1} với {levelData.pairCount} cặp bài.");
            }
        }
        else
        {
            Debug.LogError($"Lỗi: Index {currentLevel} không tồn tại.");
            gameIsPlaying = false; 
        }
    }

    void Update()
    {
        // ... (Code Update giữ nguyên) ...
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

    // ... (Các hàm timeOut, updateTimeUI, PrepareSprites, CreateCard giữ nguyên) ...
    void timeOut()
    {
        gameIsPlaying = false;
        timePlaying = 0;
        updateTimeUI();
        Debug.Log("Time Up! You Lose!");
        // Có thể load Scene thua tại đây
    }  

    void updateTimeUI()
    {
        int seconds = Mathf.CeilToInt(timePlaying);
        timeText.text = seconds.ToString();
    }
    
    private void PrepareSprites(int pairNeed)
    {
        spritePairs = new List<Sprite>();
        List<Sprite> tempPool = new List<Sprite>(sprites);
        Shuffle(tempPool);
        for (int i = 0; i < pairNeed; i++)
        {
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
        // ... (Code SetSelectedCard giữ nguyên) ...
        if(!gameIsPlaying || Time.timeScale == 0) return;

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
                
                // Lưu ý: Code gốc của bạn reset null ở đây là HƠI RỦI RO, 
                // nhưng nếu code cũ chạy ổn thì tôi giữ nguyên logic của bạn.
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
            matchCounts++;
            if(matchCounts >= spritePairs.Count / 2)
            {
                Debug.Log("You Win!");
                gameIsPlaying = false;

                // --- PHẦN THÊM MỚI: MỞ KHÓA LEVEL TIẾP THEO ---
                UnlockNextLevel();
                // ----------------------------------------------

                SceneManager.LoadScene("Result");
            }
        }
        else
        {
            a.hiddenIcon();
            b.hiddenIcon();
        }
    }

    // --- HÀM MỚI ĐỂ LƯU TIẾN ĐỘ ---
    void UnlockNextLevel()
    {
        // Lấy cấp độ cao nhất hiện tại đang lưu trong máy
        int levelReached = PlayerPrefs.GetInt("LevelReached", 0);

        // Nếu màn chơi hiện tại (currentLevel) là màn cao nhất người chơi từng chơi
        // Ví dụ: Đang ở Level 0, LevelReached là 0 -> Thắng -> Lưu LevelReached lên 1
        if (currentLevel >= levelReached)
        {
            PlayerPrefs.SetInt("LevelReached", currentLevel + 1);
            PlayerPrefs.Save(); // Lưu xuống ổ cứng ngay lập tức
            Debug.Log("Đã mở khóa Level " + (currentLevel + 2)); // +2 vì log cho con người đọc (bắt đầu từ 1)
        }
    }
    // -----------------------------

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