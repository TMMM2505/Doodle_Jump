using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {GamePlay, MainMenu, EndGame, Victory, News, Pause};
public class GameManager : Singleton<GameManager>
{
    private GameState currentState;
    private UICMainMenu uicMainMenu;
    private UICEndGame uicEndGame;
    private void Start()
    {
        ChangeState(GameState.MainMenu);
        Time.timeScale = 0.0f;
    }
    public void ChangeState(GameState newGameState)
    {
        if(newGameState != null)
        {
            currentState = newGameState;
        }
        switch(currentState)
        {
            case GameState.GamePlay:
                {
                    InGamePlay();
                    break;
                }
            case GameState.MainMenu:
                {
                    InMainMenu();
                    break;
                }
            case GameState.EndGame:
                {
                    InEndGame();
                    break;
                }
            case GameState.News:
                {
                    News();
                    break;
                }
            case GameState.Pause:
                {
                    Pause();
                    break;
                }
        }
    }

    private void Pause()
    {
        UIManager.Ins.OpenUI<UICPause>();
    }

    private void News()
    {
        UIManager.Ins.OpenUI<UICNews>();
    }

    private void InEndGame()
    {
        UIManager.Ins.CloseAll();
        uicEndGame = UIManager.Ins.OpenUI<UICEndGame>();
        uicEndGame.CheckPlayer();
    }

    private void InMainMenu()
    {
        uicMainMenu = UIManager.Ins.OpenUI<UICMainMenu>();
        uicMainMenu.SetBestScore();
        /*LevelManager.Ins.RestLevel();
        LevelManager.Ins.SetPLayer();*/
        LevelManager.Ins.ResetLevelMng();
        LevelManager.Ins.SpawnPlatform();
        LevelManager.Ins.OnInit();
    }

    private void InGamePlay()
    {
        UIManager.Ins.OpenUI<UICGamePlay>();
        LevelManager.Ins.GetPlayer().SetActive();
    }
}
