using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICMainMenu : UICanvas
{
    public Button btnPlay;
    public Button btnInstrutor;
    public TextMeshProUGUI txtBestScore;
    private void Awake()
    {
        btnPlay.onClick.AddListener(OnClickBtnPlay);
        btnInstrutor.onClick.AddListener(OnClickBtnInstructor);
    }
    private void OnClickBtnInstructor()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.News);
    }

    private void OnClickBtnPlay()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.GamePlay);
        Time.timeScale = 1.0f;
    }
    public void SetBestScore()
    {
        txtBestScore.text = UserData.Ins.GetBestScore().ToString();
    }
}
