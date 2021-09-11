using UnityEngine;

// Contain user data
// Several can be change
[System.Serializable]
public class SaveData
{
    public string playerName;
    public int highScore;

    public SaveData()
    {
        playerName = "null";
        highScore = 0;
    }
    
}
