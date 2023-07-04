using System.Collections.Generic;
using KaneTemplate.Singleton;
using Objects;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableSingleton<GameConfig>
{
    public float maxForce = 100.0f;
    public int maxPoints = 15;
    public float orderTime = 5.0f;
    public List<FoodConfig> foodConfigs;
    
    public FoodConfig GetFoodConfig(string name)
    {
        return foodConfigs.Find(foodConfig => foodConfig.name == name);
    }

}