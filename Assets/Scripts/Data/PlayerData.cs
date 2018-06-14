using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/Player")]
public class PlayerData : TDObject
{
    [Header("Стартовое количество денег")]
    [SerializeField] FloatVariable startMoney;
    public FloatVariable StartMoney
    {
        get { return startMoney; }
        set { startMoney = value; }
    }

    [Header("Текущее количество денег")]
    [SerializeField] FloatVariable money;
    public FloatVariable Money
    {
        get { return money; }
        set { money = value; }
    }

    [Header("Максимум жизней")]
    [SerializeField] FloatVariable maxHealth;
    public FloatVariable MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    [Header("Текущее значение жизней")]
    [SerializeField] FloatVariable currentHealth;
    public FloatVariable CurrentHealth
    {
        get { return currentHealth; }

        set
        {
            if (value.Value > MaxHealth.Value)
                currentHealth = MaxHealth;
            else if (value.Value < 0)
                currentHealth.Value = 0;
            else
                currentHealth = value;
        }
    }
}
