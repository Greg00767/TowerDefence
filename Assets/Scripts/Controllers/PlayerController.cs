using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public delegate void PlayerDeathHandler();

public class PlayerController : MonoBehaviour
{
    public static event PlayerDeathHandler PlayerDeath;
    [SerializeField] PlayerData data;
    public PlayerData Data
    {
        get { return data; }
        set { data = value; }
    }

    private void Start()
    {
        EnemyController.DamageDealer += ChangeHealth;
        TowerController.BuyTowerHandler += PurchaseTower;

        Data.Money.Value = Data.StartMoney.Value;
        Data.CurrentHealth.Value = Data.MaxHealth.Value;
    }

    void ChangeHealth(float value)
    {
        if (Data.CurrentHealth.Value - value < 0)
            Data.CurrentHealth.Value = 0;
        else
            Data.CurrentHealth.Value -= value;

        if (Data.CurrentHealth.Value <= 0)
            PlayerDeath();
    }

    bool PurchaseTower(float value)
    {
        if (value < Data.Money.Value)
        {
            Data.Money.Value -= value;
            return true;
        }
        else
            return false;
    }
}
