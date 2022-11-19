using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ZombieType
{
    Normal,
    Kid,
    Worker,
    Gangster,
    Soldier,
    Doctor,
}

public enum MaterialType
{
    Pickles, // 피클
    Onions, // 양파
    Bread, // 빵
    Cheese, // 치즈
    Lettuce, // 상추
    Patty, // 패티
}

public class Order
{
    public readonly ZombieType ZombieType;
    
    public readonly MaterialType[] Recipe;
    private const int RecipeCount = 3;
    public const int MaterialCount = 7;

    private float _time;
    private const float MaxTime = 12f;
    private const float MinTime = 0f;
    public float Time
    {
        get => _time;
        set
        {
            _time = Mathf.Clamp(value, MinTime, MaxTime);
            // Todo : UI에 표시
            if (_time <= MinTime)
            {
                OnOrderFail?.Invoke();
            }
        }
    }
    
    public Action OnOrderSuccess;
    public Action OnOrderFail;
    public Action OnSubmitFail;

    public void SubmitOrder(MaterialType[] recipe)
    {
        if (recipe.Length != RecipeCount)
        {
            OnSubmitFail?.Invoke();
            return;
        }
        
        for (int i = 0; i < MaterialCount; i++)
        {
            if (Recipe[i] == recipe[i]) continue;
            
            OnSubmitFail?.Invoke();
            return;
        }

        OnOrderSuccess?.Invoke();
    }

    public Order()
    {
        var zombieType = (ZombieType)Random.Range(0, typeof(ZombieType).GetEnumValues().Length);
        
        ZombieType = zombieType;
        Recipe = RecipeSheet[zombieType][Random.Range(0, RecipeCount)];
        _time = MaxTime;
    }

    private static readonly Dictionary<ZombieType, MaterialType[][]> RecipeSheet = new()
    {
        [ZombieType.Normal] = new[]
        {
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Cheese, MaterialType.Patty,
                MaterialType.Onions, MaterialType.Pickles, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Patty, MaterialType.Lettuce, MaterialType.Cheese,
                MaterialType.Pickles, MaterialType.Onions, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Onions, MaterialType.Pickles,
                MaterialType.Cheese, MaterialType.Patty, MaterialType.Bread
            }
        },
        [ZombieType.Kid] = new[]
        {
            new[]
            {
                MaterialType.Bread, MaterialType.Patty, MaterialType.Lettuce, MaterialType.Cheese,
                MaterialType.Onions, MaterialType.Patty, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Patty, MaterialType.Cheese,
                MaterialType.Patty, MaterialType.Pickles, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Cheese, MaterialType.Patty, MaterialType.Patty,
                MaterialType.Onions, MaterialType.Pickles, MaterialType.Bread
            }
        },
        [ZombieType.Worker] = new[]
        {
            new[]
            {
                MaterialType.Bread, MaterialType.Cheese, MaterialType.Patty, MaterialType.Bread,
                MaterialType.Patty, MaterialType.Lettuce, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Patty, MaterialType.Bread, MaterialType.Patty,
                MaterialType.Cheese, MaterialType.Pickles, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Patty, MaterialType.Bread,
                MaterialType.Patty, MaterialType.Onions, MaterialType.Bread
            }
        },
        [ZombieType.Gangster] = new[]
        {
            new[]
            {
                MaterialType.Bread, MaterialType.Cheese, MaterialType.Patty, MaterialType.Cheese,
                MaterialType.Lettuce, MaterialType.Onions, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Patty, MaterialType.Cheese,
                MaterialType.Cheese, MaterialType.Lettuce, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Patty, MaterialType.Cheese,
                MaterialType.Pickles, MaterialType.Cheese, MaterialType.Bread
            }
        },
        [ZombieType.Soldier] = new[]
        {
            new[]
            {
                MaterialType.Bread, MaterialType.Pickles, MaterialType.Patty, MaterialType.Pickles,
                MaterialType.Cheese, MaterialType.Lettuce, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Cheese, MaterialType.Patty, MaterialType.Pickles,
                MaterialType.Pickles, MaterialType.Onions, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Pickles, MaterialType.Patty,
                MaterialType.Onions, MaterialType.Pickles, MaterialType.Bread
            }
        },
        [ZombieType.Doctor] = new[]
        {
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Cheese, MaterialType.Patty,
                MaterialType.Onions, MaterialType.Pickles, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Patty, MaterialType.Cheese,
                MaterialType.Lettuce, MaterialType.Onions, MaterialType.Bread
            },
            new[]
            {
                MaterialType.Bread, MaterialType.Lettuce, MaterialType.Onions, MaterialType.Patty,
                MaterialType.Onions, MaterialType.Pickles, MaterialType.Bread
            }
        },
    };
}