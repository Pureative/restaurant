using System.Collections.Generic;
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
    public string PlaySceneName;
    
    public List<TableController> tableList;
    public Thrower thrower;
    public UnityEvent GameStarted;
    public UnityEvent GameEnded;
    
    private TableController _currentTableOrder;

    public bool IsLevelStarted { get; private set; }
    public bool IsGameEnded { get; private set; }
    public float TimeCount { get; private set; }
    
    private void Awake()
    {
        Instance = this;
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

    public void Restart()
    {
        IsLevelStarted = false;
        IsGameEnded = false;
        SceneManager.LoadScene(PlaySceneName);
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
        _currentTableOrder = null;
        Debug.Log("Time Up");
        MakeOrder();
    }
    private void OnMiss()
    {
        Debug.Log("Miss");
    }
    
    private void OnHit(HitType hitType)
    {
        _currentTableOrder.CancelOrder();
        _currentTableOrder = null;
        MakeOrder();
    }
}