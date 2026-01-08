using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    [Header("UI References")]
    public Text titleText;
    public Text codeText;
    public Text descriptionText;
    public Image voucherImage;
    public GameObject victoryPanel;

    [Header("Data")]
    public VoucherDatabase database;

    [Header("UI Rewards")]
    public GameObject rewardPopup; // Cái bảng thông báo hiện ra khi mở quà
    public Image iconDisplay;
    public Text codeDisplay;
    public Text titleDisplay;

    private bool isGiftOpened = false;
    public void OnGameWin(GameObject clickedGift)
    {
        if (database == null) 
        {
        Debug.LogError("Chưa kéo Voucher Database vào!");
        return;
        }

        if (isGiftOpened) return; // Chặn mở quà nhiều lần
        isGiftOpened = true;
    // 1. Lấy voucher ngẫu nhiên
        Voucher reward = database.GetRandomVoucher();

        // 2. Gán dữ liệu (Phải dùng .text cho các ô Text)
        if (titleText != null) titleText.text = reward.title;
        if (codeText != null) codeText.text = reward.code;
        if (descriptionText != null) descriptionText.text = reward.description;
        if (voucherImage != null) voucherImage.sprite = reward.icon;

        // 3. Hiện Popup
        if (rewardPopup != null) 
        {
            rewardPopup.SetActive(true);
            Debug.Log("Đã hiện Panel Victory!");
        }
        if (clickedGift != null) 
        {
            clickedGift.SetActive(false);
        }
    }
    

    public void CopyCodeToClipboard()
    {
        GUIUtility.systemCopyBuffer = codeText.text.Replace("CODE: ", "");
    }
    public void ResetVictoryState()
    {
        isGiftOpened = false;
        victoryPanel.SetActive(false);
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
