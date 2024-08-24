using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICEndGame : UICanvas
{
    public Button btnContinue;
    public TextMeshProUGUI txtWin, txtLose;
    public TextMeshProUGUI txtBestScore;
    public TextMeshProUGUI txtScore;

    private void Awake()
    {
        btnContinue.onClick.AddListener(OnClickBtnContinue);
    }

    private void OnClickBtnContinue()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
    public void CheckPlayer()
    {
        Time.timeScale = 0.0f;
        txtScore.text = LevelManager.Ins.GetPlayer().GetScore().ToString();
        UserData.Ins.SaveBestScore(LevelManager.Ins.GetPlayer().GetScore());
        txtBestScore.text = UserData.Ins.GetBestScore().ToString();
        if (LevelManager.Ins.GetPlayer().IsAlive())
        {
            ActiveTxtWin();
        }
        else
        {
            ActiveTxtLose();
        }
    }
    private void ActiveTxtLose()
    {
        CloseAllTxt();
        txtLose.gameObject.SetActive(true);
    }
    private void ActiveTxtWin()
    {
        CloseAllTxt();
        txtWin.gameObject.SetActive(true);
    }
    private void CloseAllTxt()
    {
        txtWin.gameObject.SetActive(false);
        txtLose.gameObject.SetActive(false);
    }
}
