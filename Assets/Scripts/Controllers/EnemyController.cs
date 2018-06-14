using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public delegate void DamageDealerHandler(float value);
public class EnemyController : MonoBehaviour
{
    public static event DamageDealerHandler DamageDealer;
    public SpriteRenderer spriteObject;

    Vector3 startPosition;
    Vector3 nextPosition;
    Vector3 endPosition;
    Vector3 currentPosition;
    Vector3 positionOffsetZ;
    PathController path;

    int currentWayPointIndex;

    [SerializeField] EnemyData data;
    public EnemyData Data
    {
        get { return data; }
        set { data = value; }
    }

    [SerializeField] float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value < 0)
                currentHealth = 0;
            else
                currentHealth = value;
        }
    }

    Transform pool;

    public void StartEnemy(PathController path, Transform enemyPool)
    {
        Debug.Log("Enemy - " + Data.name + " started.");
        this.path = path;
        pool = enemyPool;
        currentHealth = Data.Health;

        currentWayPointIndex = 0;
        positionOffsetZ = Vector3.up * 0.1f;
        startPosition = path.Data.WayPoints[currentWayPointIndex] + positionOffsetZ;
        endPosition = path.Data.WayPoints[path.Data.WayPoints.Length - 1] + positionOffsetZ;
        StartFollowPath();
    }

    void GetNextPosition()
    {
        currentWayPointIndex++;

        if (currentWayPointIndex == path.Data.WayPoints.Length - 1)
            nextPosition = endPosition;
        else
            nextPosition = path.Data.WayPoints[currentWayPointIndex] + positionOffsetZ;
    }

    void StartFollowPath ()
    {
        currentPosition = startPosition;
        transform.position = currentPosition;

        StartCoroutine(FollowToNextWayPoint());
    }

    IEnumerator FollowToNextWayPoint()
    {
        do
        {
            GetNextPosition();
            transform.LookAt(nextPosition, Vector3.up);
            while (Vector3.Distance(transform.position, nextPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * Data.MoveSpeed);
                yield return null;
            }
            yield return null;

        } while (nextPosition != endPosition);

        DamageDealer(Data.PlayerDamage);
        BackToPool();
    }

    public void GetDamage(float value)
    {
        CurrentHealth -= value;

        if (currentHealth == 0)
            BackToPool();
    }

    void BackToPool()
    {
        this.transform.SetParent(pool);
        this.gameObject.SetActive(false);
    }
}
