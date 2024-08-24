using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICGamePlay : UICanvas
{
    public TextMeshProUGUI txtScore;
    public Button btnPause;
    private void Awake()
    {
        btnPause.onClick.AddListener(OnClickBtnPause);
    }
    private void Update()
    {
        if(gameObject.active)
        {
            txtScore.text = LevelManager.Ins.GetPlayer().GetScore().ToString();
        }
    }
    private void OnClickBtnPause()
    {
        Close(0);
        Time.timeScale = 0f;
        LevelManager.Ins.GetPlayer().SetIsFlying(false);
        GameManager.Ins.ChangeState(GameState.Pause);
    }
}
