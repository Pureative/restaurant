using System.Collections.Generic;
using Objects;
using Projectiles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public List<TableController> tableList;
    public Thrower thrower;
    public UnityEvent OnTimeUp;
    public UnityEvent GameStarted;
    
    private TableController _currentTableOrder;
    
    
    private void Awake()
    {
        Instance = this;
        thrower.OnMiss.AddListener(OnMiss);
    }

    public void StartGame()
    {
        MakeOrder();
        GameStarted.Invoke();
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