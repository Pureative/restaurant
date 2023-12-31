﻿using System.Collections.Generic;
using Objects;
using Projectiles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float LevelDuration;
    
    [SerializeField]
    private string PlaySceneName;

    [SerializeField]
    private int TargetFPS = 60;

    public List<TableController> tableList;
    public Thrower thrower;
    public UnityEvent GameStarted;
    public UnityEvent<HitType> Hit;
    public UnityEvent Miss;
    public UnityEvent WrongOrder;
    public UnityEvent OnTimeUp;
    public UnityEvent RestartGame;

    public UnityEvent GameEnded;
    
    private TableController _currentTableOrder;

    public bool IsLevelStarted { get; private set; }
    public bool IsGameEnded { get; private set; }
    public float TimeCount { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = TargetFPS;
        thrower.OnMiss.AddListener(OnMiss);
    }

    private void Update()
    {
        if (!IsLevelStarted || IsGameEnded)
        {
            return;
        }

        TimeCount += Time.deltaTime;
        if (TimeCount >= LevelDuration)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        MakeOrder();
        IsLevelStarted = true;
        GameStarted.Invoke();
    }

    public void EndGame()
    {
        IsGameEnded = true;
        GameEnded.Invoke();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        IsLevelStarted = false;
        IsGameEnded = false;
        TimeCount = 0f;
        RestartGame.Invoke();
        _currentTableOrder.CancelOrder();
        _currentTableOrder = null;
        ResumeGame();
        
    }

    public void MakeOrder()
    {
        int index = Random.Range(0, tableList.Count);
        _currentTableOrder = tableList[index];
        _currentTableOrder.MakeOrder();
        _currentTableOrder.TimeUp.RemoveListener(OnOrderTimeUp);
        _currentTableOrder.TimeUp.AddListener(OnOrderTimeUp);
        _currentTableOrder.OnHit.RemoveListener(OnHit);
        _currentTableOrder.OnHit.AddListener(OnHit);
    }
    
    private void OnOrderTimeUp()
    {
        _currentTableOrder.CancelOrder();
        _currentTableOrder.OnHit.RemoveListener(OnHit);
        _currentTableOrder.TimeUp.RemoveListener(OnOrderTimeUp);
        _currentTableOrder = null;
        Debug.Log("Time Up");
        MakeOrder();
    }
    private void OnMiss()
    {
        Debug.Log("Miss");
        Miss.Invoke();
    }
    
    private void OnHit(HitType hitType)
    {
        if (thrower.currentFoodName.Contains(_currentTableOrder.CurrentOrderName))
        {
            Debug.Log($"Hit {hitType}");
            Hit.Invoke(hitType);
        }
        else
        {
            Debug.Log("Wrong Order");
            WrongOrder.Invoke();
        }
        _currentTableOrder.CancelOrder();
        _currentTableOrder.OnHit.RemoveListener(OnHit);
        _currentTableOrder.TimeUp.RemoveListener(OnOrderTimeUp);
        _currentTableOrder = null;

        MakeOrder();

    }
}