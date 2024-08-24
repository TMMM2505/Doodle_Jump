using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICNews : UICanvas
{
    public Button BtnExit;
    private void Awake()
    {
        BtnExit.onClick.AddListener(OnClickBtnExit);
    }

    private void OnClickBtnExit()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
