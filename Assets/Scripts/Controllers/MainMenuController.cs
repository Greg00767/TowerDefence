using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void MainMenuStateHandler(LocationController location);

public class MainMenuController : MonoBehaviour
{
    public static event MainMenuStateHandler GameStarted;

    //Параметры игры
    [SerializeField] GameData gameParameters;
    public GameData GameParameters
    {
        get { return gameParameters; }
    }

    //Текущая выбранная локация
    LocationController currentLocation;
    public LocationController CurrentLocation
    {
        get { return currentLocation; }
        set { currentLocation = value; }
    }

    // UI компоненты ///////////////////////////////////////////////
    //UI стартовое меню
    [SerializeField] GameObject startPanel;
    public GameObject StartPanel
    {
        get { return startPanel; }
    }

    //UI заголовок игры
    [SerializeField] Text gameTitle;
    public Text GameTitle
    {
        get { return gameTitle; }
        set { gameTitle = value; }
    }

    //UI Кнопка Старта игры
    [SerializeField] Button startButton;
    public Button StartButton
    {
        get { return startButton; }
        set { startButton = value; }
    }

    //UI Шаблоны
    [SerializeField] GameObject locationTemplate;
    [SerializeField] GameObject waveTemplate;

    //UI Слайдеры
    [SerializeField] Slider enemyCount;
    [SerializeField] Slider enemyProportions;

    //UI Списки с прокруткой
    [SerializeField] ScrollRect scrollViewLocations;
    [SerializeField] ScrollRect scrollViewWaves;
    // UI компоненты ///////////////////////////////////////////////

    void Start()
    {
        StatController.StartNewGame += OnStartNewGame;
        InitNewGame();
    }

    void OnStartNewGame()
    {
        InitNewGame();
    }

    public void InitNewGame()
    {
        if (StartPanel)
            StartPanel.SetActive(true);

        GameTitle.text = GameParameters.GameName;
        CurrentLocation = null;

        HideTemplates();
        DeactivateButton(StartButton);

        FillLocations();
    }

    //Заполнить слайдер локаций
    void FillLocations()
    {
        //Индекс позиции кнопки локации в слайдере
        int index = 0;

        for (int i = 0; i < gameParameters.Locations.Length; i++)
        {
            if (gameParameters.Locations[i] != null)
            {
                ShowLocation(gameParameters.Locations[i], index);
                index++;
            }
        }
    }

    //Создать экземпляр шаблона кнопки локации и поместить в слайдер локаций с учетом индекса
    void ShowLocation(LocationData locData, int index)
    {
        GameObject location = Instantiate(locationTemplate, locationTemplate.transform.parent);
        {
            location.SetActive(true);
            location.name = locData.Name + index.ToString();
            location.AddComponent<LocationController>().Data = locData;
            location.GetComponent<LocationController>().Index = index;
            location.GetComponent<Button>().onClick.AddListener(delegate { SelectLocation(location.GetComponent<LocationController>()); });

            Text locationLabel = location.GetComponentInChildren<Text>();
            locationLabel.text = locData.Name;
        }
    }

    //Заполнить слайдер волн для выбранной локации
    void FillWaves()
    {
        ClearWaves();

        for (int i = 0; i < CurrentLocation.Data.WavesData.Length; i++)
            ShowWave(i);
    }

    //Создать экземпляр шаблона кнопки волны и поместить в слайдер волн с учетом индекса
    void ShowWave(int index)
    {
        GameObject wave = Instantiate(waveTemplate, waveTemplate.transform.parent);
        {
            wave.SetActive(true);
            wave.name = index.ToString();
            wave.tag = "UIWave";
            wave.GetComponent<Button>().onClick.AddListener(delegate { SelectWave(index); });

            Text waveLabel = wave.GetComponentInChildren<Text>();
            waveLabel.text = wave.name;
        }
    }

    //Очистить список волн
    void ClearWaves()
    {
        Transform waveContent = waveTemplate.transform.parent;

        foreach (Transform wave in waveContent)
            if (wave.tag == "UIWave")
                Destroy(wave.gameObject);
    }

    //Выбор локации
    public void SelectLocation(LocationController loc)
    {
        CurrentLocation = loc;
        ActivateButton(StartButton);
        FillWaves();
    }

    //Выбор волны
    public void SelectWave(int index)
    {
        ShowWaveParameters(CurrentLocation.Data.WavesData[index]);
    }

    //Начать игру
    public void StartGame()
    {
        StartPanel.SetActive(false);
        GameStarted(currentLocation);
    }

    //Скрыть UI шаблоны
    void HideTemplates()
    {
        locationTemplate.SetActive(false);
        waveTemplate.SetActive(false);
    }

    //Отображение параметров волны
    void ShowWaveParameters(WaveData wave)
    {
        enemyCount.minValue = wave.MinEnemyCount;
        enemyCount.maxValue = wave.MaxEnemyCount;
        enemyCount.value = wave.EnemyCount;

        enemyProportions.minValue = wave.MinEnemyCount;
        enemyProportions.maxValue = wave.MaxEnemyProportions;
        enemyProportions.value = wave.EnemyTypeProportions;
    }

    void ActivateButton(Button btn)
    {
        if (!btn.IsInteractable())
            btn.interactable = true;
    }

    void DeactivateButton(Button btn)
    {
        if (btn.IsInteractable())
            btn.interactable = false;
    }
}
