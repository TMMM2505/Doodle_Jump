using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICPause : UICanvas
{
    public Button btnContinue;
    private void Awake()
    {
        btnContinue.onClick.AddListener(OnClickBtnContinue);
    }

    private void OnClickBtnContinue()
    {
        Close(0);
        Time.timeScale = 1.0f;
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }
}
