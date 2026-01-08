using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataGame", menuName = "Scriptable Objects/LevelDataGame")]
public class LevelDataGame : ScriptableObject
{
    public int levelId;
    public float timeLimit;

    public int pairCount; // số cặp bài từng lv
}
