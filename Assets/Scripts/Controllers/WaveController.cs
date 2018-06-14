using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public delegate void WaveStateHandler();

public class WaveController : MonoBehaviour
{
    public static event WaveStateHandler WaveFinished;

    WaveData data;
    public WaveData Data
    {
        get { return data; }
        set { data = value; }
    }

    Transform enemyPool;
    Transform wavePool;

    //Создание волны врагов
    public void CreateWave(Transform enemyPool, Transform wavePool, PathController path)
    {
        this.enemyPool = enemyPool;
        this.wavePool = wavePool;

        int i = 0;
        while (i++ != Data.EnemyCount)
        {
            Transform enemy = enemyPool.GetChild(0);
            enemy.SetParent(wavePool);
            enemy.gameObject.SetActive(false);
        }

        StartEnemies(path);
    }

    //Создание волны врагов
    void StartEnemies(PathController path)
    {
        StartCoroutine(MoveEnemies(path));
    }

    //Инициализация запуска волны
    IEnumerator MoveEnemies(PathController path)
    {
        yield return new WaitForSeconds(Data.StartDelay);

        while (wavePool.childCount > 0)
        {
            foreach (Transform enemy in wavePool)
            {
                if (!enemy.gameObject.activeSelf)
                {
                    enemy.gameObject.SetActive(true);
                    enemy.GetComponent<EnemyController>().StartEnemy(path, enemyPool);
                    yield return new WaitForSeconds(Data.EnemyDelay);
                }
            }

            yield return null;
        }

        //while (wavePool.childCount > 0)
        //    yield return null;

        WaveFinished();
    }
}
