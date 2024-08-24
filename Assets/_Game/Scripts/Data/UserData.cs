using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    public void SaveBestScore(int score)
    {
        if(score > GetBestScore())
        {
            PlayerPrefs.SetInt(Cache.keyBestScore, score);
        }
    }
    public int GetBestScore() => PlayerPrefs.GetInt(Cache.keyBestScore);
}
