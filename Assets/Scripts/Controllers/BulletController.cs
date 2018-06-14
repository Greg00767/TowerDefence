using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    //Ссылка на хранилище параметров снаряда
    [SerializeField] BulletData data;
    public BulletData Data
    {
        get { return data; }
        set { data = value; }
    }

    //Ссылка на картинку снаряда
    [SerializeField] SpriteRenderer spriteBullet;
    public SpriteRenderer SpriteBullet
    {
        get { return spriteBullet; }
        set { spriteBullet = value; }
    }

    TowerController tower;
    Transform target;
    float bulletSpeed;
    Transform pool;

    public void FlyBullet(Transform pool, TowerController tower, Transform target)
    {
        this.transform.SetParent(null);
        this.transform.position = tower.transform.position;
        this.pool = pool;
        this.tower = tower;
        this.target = target;

        bulletSpeed = tower.Data.ShellSpeed;
    }

    void Update()
    {
        if (target)
        {
            if (Vector3.Distance(this.transform.position, target.position) > 0.1f)
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, Time.deltaTime * bulletSpeed);
            else if (this.gameObject.activeSelf)
            {
                target.GetComponent<EnemyController>().GetDamage(tower.Data.DamageValue);
                target = null;

                this.transform.SetParent(pool);
                this.gameObject.SetActive(false);
            }
        }
    }
}
