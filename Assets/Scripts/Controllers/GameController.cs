using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    //UI панель игры
    [SerializeField] GameObject gamePanel;
    public GameObject GamePanel
    {
        get { return gamePanel; }
        set { gamePanel = value; }
    }

    //Локация
    [SerializeField] LocationController location;
    public LocationController Location
    {
        get { return location; }
        set { location = value; }
    }

    //Волна
    [SerializeField] WaveController wave;
    public WaveController Wave
    {
        get { return wave; }
        set { wave = value; }
    }

    //Сцена
    [SerializeField] GameObject levelDesign;
    public GameObject LevelDesign
    {
        get { return levelDesign; }
        set { levelDesign = value; }
    }

    //
    [SerializeField] GameObject ground;
    public GameObject Ground
    {
        get { return ground; }
        set { ground = value; }
    }

    //Траектория движения
    [SerializeField] PathController path;
    public PathController Path
    {
        get { return path; }
        set { path = value; }
    }

    //
    [SerializeField] PlayerController player;
    public PlayerController Player
    {
        get { return player; }
        set { player = value; }
    }

    //Номер текущей волны
    [SerializeField] IntVariable currentWaveNumber;
    public IntVariable CurrentWaveNumber
    {
        get { return currentWaveNumber; }
        set { currentWaveNumber = value; }
    }

    //Количество врагов в текущей волне
    int enemyCount;
    public int EnemyCount
    {
        get { return enemyCount; }
        set { enemyCount = value; }
    }

    //Размер пула врагов
    int enemyPoolSize;
    public int EnemyPoolSize
    {
        get { return enemyPoolSize; }
        set { enemyPoolSize = value; }
    }

    //Размер пула снарядов
    int bulletPoolSize;
    public int BulletPoolSize
    {
        get { return bulletPoolSize; }
        set { bulletPoolSize = value; }
    }

    //Пул врагов для одной волны
    GameObject enemyPool;
    public GameObject EnemyPool
    {
        get { return enemyPool; }
        set { enemyPool = value; }
    }

    //Пул снарядов для башен
    GameObject bulletPool;
    public GameObject BulletPool
    {
        get { return bulletPool; }
        set { bulletPool = value; }
    }

    readonly int maxBulletPoolSize = 100;
    public int MaxBulletPoolSize
    {
        get { return maxBulletPoolSize; }
    }

    //Волна
    GameObject wavePool;
    public GameObject WavePool
    {
        get { return wavePool; }
        set { wavePool = value; }
    }

    //Шаблон врага
    [SerializeField] GameObject enemyTemplate;
    public GameObject EnemyTemplate
    {
        get { return enemyTemplate; }
        set { enemyTemplate = value; }
    }

    //Шаблон вышки
    [SerializeField] GameObject towerTemplate;
    public GameObject TowerTemplate
    {
        get { return towerTemplate; }
        set { towerTemplate = value; }
    }

    //Шаблон пули
    [SerializeField] GameObject bulletTemplate;
    public GameObject BulletTemplate
    {
        get { return bulletTemplate; }
        set { bulletTemplate = value; }
    }

    //Шаблон вышки UI
    [SerializeField] GameObject towerTemplateUI;
    public GameObject TowerTemplateUI
    {
        get { return towerTemplateUI; }
        set { towerTemplateUI = value; }
    }

    //
    GameObject towers;

    //
    GameObject tower;

    //
    TowerController currentTowerController;

    //
    Camera mainCamera;

    void Start()
    {
        PlayerController.PlayerDeath += StopGame;

        if (GamePanel)
            GamePanel.SetActive(false);

        if (LevelDesign)
            LevelDesign.SetActive(false);

        MainMenuController.GameStarted += InitGame;
    }

    //Инициализация игрового процесса
    public void InitGame(LocationController loc)
    {
        Location = loc;

        GamePanel.SetActive(true);
        LevelDesign.SetActive(true);

        Path.Data = Location.Data;
        Path.Line = Path.GetComponent<LineRenderer>();

        CurrentWaveNumber.Value = 0;

        FillTowers();

        CreateTowersGroup();

        //Создание пула врагов
        CreateEnemyPool();

        //
        CreateBulletPool();

        WaveController.WaveFinished += CreateWave;

        //Создание волны
        CreateWave();

        //Старт волны
        //StartWave(wavePool, path);
    }

    //Заполнить слайдер вышек
    void FillTowers()
    {
        towerTemplateUI.SetActive(false);

        for (int i = 0; i < Location.Data.Towers.Length; i++)
        {
            if (Location.Data.Towers[i] != null)
                ShowTower(Location.Data.Towers[i]);
        }
    }

    //Создать экземпляр шаблона кнопки локации и поместить в слайдер локаций с учетом индекса
    void ShowTower(TowerData towerData)
    {
        GameObject tower = Instantiate(towerTemplateUI, towerTemplateUI.transform.parent);
        {
            tower.SetActive(true);
            tower.name = towerData.Name;
            //TowerController towerControl = tower.AddComponent<TowerController>();
            TowerController towerControl = tower.GetComponent<TowerController>();

            towerControl.Data = towerData;
            tower.GetComponent<Image>().sprite = towerData.Sprite;
        }
    }

    void CreateTowersGroup()
    {
        towers = new GameObject();
        towers.transform.SetParent(null);
        towers.name = "Towers";
        towers.transform.position = Vector3.zero;
    }

    //Создание пула врагов для одной волны
    void CreateEnemyPool()
    {
        EnemyPool = new GameObject();
        EnemyPool.name = "Enemy pool";
        EnemyPool.transform.SetParent(null);
        EnemyPool.SetActive(false);

        //Размер пула равен максимальному размеру волны.
        EnemyPoolSize = Location.Data.WavesData[0].MaxEnemyCount;

        for (int i = 0; i < EnemyPoolSize; i++)
        {
            CreateEnemy((EnemyType)Random.Range((int)EnemyType.enemyType1, (int)EnemyType.enemyType3 + 1), i + 1);
        }
    }

    //Создание пула снарядов
    void CreateBulletPool()
    {
        BulletPool = new GameObject();
        BulletPool.name = "Bullet pool";
        BulletPool.transform.SetParent(null);
        BulletPool.SetActive(false);

        //Размер пула равен максимальному размеру волны.
        BulletPoolSize = MaxBulletPoolSize;

        for (int i = 0; i < BulletPoolSize; i++)
        {
            CreateBullet(BulletType.bulletType1, i + 1);
        }
    }

    void CreateEnemy(EnemyType type, int id)
    {
        GameObject enemy = Instantiate(enemyTemplate, EnemyPool.transform);
        {
            EnemyController enemyControl = enemy.GetComponent<EnemyController>();

            enemy.name = "Enemy " + id;
            enemyControl.Data = Location.Data.ObjectTypes.EnemyTypes[(int)type];
            enemyControl.spriteObject.sprite = enemyControl.Data.Sprite;
        }
    }

    void CreateBullet(BulletType type, int id)
    {
        GameObject bullet = Instantiate(bulletTemplate, BulletPool.transform);
        {
            BulletController controller = bullet.GetComponent<BulletController>();

            bullet.name = "Bullet " + id;
            controller.Data = Location.Data.ObjectTypes.BulletTypes[(int)type];
            controller.SpriteBullet.sprite = controller.Data.BulletSprite;
        }
    }

    //Создание волны врагов
    void CreateWave()
    {
        CurrentWaveNumber.Value++;

        if (WavePool == null)
        {
            WavePool = new GameObject();
            WavePool.name = "Wave pool";
            WavePool.transform.SetParent(null);
            WavePool.SetActive(true);
        }

        if (CurrentWaveNumber.Value <= Location.Data.WavesData.Length)
        {
            Wave.Data = Location.Data.WavesData[CurrentWaveNumber.Value - 1];
            Wave.CreateWave(enemyPool.transform, WavePool.transform, path);
        }
        else
        {
            //Stats
        }
    }

    public void OnBeginDragTower(TowerController tc)
    {
        mainCamera = Camera.main;

        tower = Instantiate(towerTemplate, null);
        {
            tower.name = "Tower";

            TowerController controller = tower.GetComponent<TowerController>();
            SpriteRenderer image = controller.SpriteTower;

            tower.SetActive(true);

            controller.Data = tc.Data;
            image.sprite = tc.Data.Sprite;

            controller.InitTower(BulletPool);
        }
    }

    public void OnDragTower()
    {
        Vector3 mousePos = Input.mousePosition + Vector3.forward * 0.1f;
        Vector3 objPos = mainCamera.ScreenToWorldPoint(mousePos);

        tower.transform.position = new Vector3(objPos.x, Ground.transform.position.y + 0.2f, objPos.z);
    }

    public void OnEndDragTower()
    {
        if (tower.GetComponent<TowerController>().BuyTower())
        {
            tower.transform.SetParent(towers.transform);
            tower.GetComponent<TowerController>().FindEnemy(WavePool.transform);
        }
        else
        {
            Destroy(tower);
        }
    }

    public void OnClickTower(TowerController tower)
    {
        currentTowerController = tower;
    }

    void StopGame()
    {
        Time.timeScale = 0.0f;
    }
}
