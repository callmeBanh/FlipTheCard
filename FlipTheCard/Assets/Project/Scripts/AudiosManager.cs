using UnityEngine;

public class AudiosManager : MonoBehaviour
{
    public static AudiosManager Instance;

    [SerializeField] private AudioSource bgmSource; 
    public bool isSoundOn = true; 

    private void Awake()
    {
        // --- ĐOẠN CODE QUAN TRỌNG ĐỂ GIỮ NHẠC ---
        if (Instance == null)
        {
            Instance = this;
            // Giữ đối tượng này tồn tại xuyên suốt các Scene
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Nếu đã có một Manager từ Scene trước rồi, thì xóa cái mới này đi
            Destroy(gameObject); 
            return;
        }
        // ----------------------------------------
    }

    public void ToggleMusic(bool status)
    {
        isSoundOn = status;
        if (bgmSource != null)
        {
            bgmSource.mute = !isSoundOn;
        }
    }
}