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
    public UnityEvent GameStarted;
    public UnityEvent<HitType> Hit;
    public UnityEvent Miss;
    public UnityEvent WrongOrder;
    public UnityEvent OnTimeUp;

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
        if (thrower.objectToThrow.name.Contains(_currentTableOrder.CurrentOrderName))
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