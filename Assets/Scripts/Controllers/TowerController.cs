using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public delegate bool BuyTowerHandler(float value);

public class TowerController : MonoBehaviour
{
    public static event BuyTowerHandler BuyTowerHandler;
    enum TowerState { Deactivated, Active, EnemyFound, AttackEnemy}

    //Ссылка на хранилище параметров вышки
    [SerializeField] TowerData data;
    public TowerData Data
    {
        get { return data; }
        set { data = value; }
    }

    //Ссылка на картинку вышки
    [SerializeField] SpriteRenderer spriteTower;
    public SpriteRenderer SpriteTower
    {
        get { return spriteTower; }
        set { spriteTower = value; }
    }

    //Ссылка на картинку радиуса
    [SerializeField] SpriteRenderer spriteRange;
    public SpriteRenderer SpriteRange
    {
        get { return spriteRange; }
        set { spriteRange = value; }
    }

    //Текущая цель вышки
    Transform target;
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    //Активная в данный момент волна
    Transform wave;
    public Transform Wave
    {
        get { return wave; }
        set { wave = value; }
    }

    //Состояние вышки Активна/Неактивна
    bool isTowerActive = false;
    public bool IsTowerActive
    {
        get { return isTowerActive; }
        set { isTowerActive = value; }
    }

    //Найден ли ближайший враг
    bool enemyFound = false;
    public bool EnemyFound
    {
        get { return enemyFound; }
        set { enemyFound = value; }
    }

    //Найден ли ближайший враг
    bool isTowerShoots = false;
    public bool IsTowerShoots
    {
        get { return isTowerShoots; }
        set { isTowerShoots = value; }
    }

    //Пул снарядов
    GameObject bullets;
    public GameObject Bullets
    {
        get { return bullets; }
        set { bullets = value; }
    }

    public void InitTower(GameObject bulletPool)
    {
        SetRangeCircle();
        Bullets = bulletPool;
    }

    void Update()
    {
        //Удерживать цель на прицеле, пока она не выйдет из радиуса атаки вышки
        if (isTowerActive && enemyFound)
        {
            transform.LookAt(target);

            if (!IsTowerShoots)
                StartFire();

            //Искать новую цель, если текущая вышла за радиус атаки вышки или убита
            if (Vector3.Distance(transform.position, target.position) > Data.AttackRange || target.parent != Wave)// ||
                //target.GetComponent<EnemyController>().CurrentHealth == 0)
            {
                enemyFound = false;
                FindEnemy(Wave);
            }
        }
    }

    //Активирует вышку и начинает поиск ближайшего врага
    public void FindEnemy(Transform wave)
    {
        Wave = wave;
        isTowerActive = true;
        SpriteRange.enabled = false;
        StartCoroutine(DoFind());
    }

    //Определяет ближайшего врага в радиусе вышки, пока вышка не отключится или пока враг не будет найден
    IEnumerator DoFind()
    {
        float curDistance;
        float minDistance;
        Transform closestEnemy = null;

        while (isTowerActive && !enemyFound)
        {
            curDistance = Data.AttackRange;
            minDistance = curDistance;

            foreach (Transform enemy in Wave)
            {
                curDistance = Vector3.Distance(transform.position, enemy.position);

                if (curDistance <= Data.AttackRange)
                {
                    if (curDistance < minDistance)
                    {
                        minDistance = curDistance;
                        closestEnemy = enemy;
                    }
                }
            }

            if (closestEnemy != null)
            {
                target = closestEnemy;
                enemyFound = true;
            }

            yield return null;
        }
    }

    void SetRangeCircle()
    {
        SpriteRange.transform.localScale *= Data.AttackRange;
    }

    void StartFire()
    {
        IsTowerShoots = true;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        //foreach (Transform bullet in Bullets.transform)
        {
            while (IsTowerActive && enemyFound)
            {
                Transform bullet = Bullets.transform.GetChild(0);
                bullet.gameObject.SetActive(true);
                bullet.GetComponent<BulletController>().FlyBullet(Bullets.transform, this, target);
                yield return new WaitForSeconds(Data.FireRate);
            }
        }

        IsTowerShoots = false;
    }

    public bool BuyTower()
    {
        return BuyTowerHandler(Data.PurchasePrice);
    }
}
