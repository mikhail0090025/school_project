using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
public class NewGameSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown MapDropdown;
    [SerializeField] Slider Team1SizeSlider;
    [SerializeField] Slider Team2SizeSlider;
    [SerializeField] TMP_Dropdown BotsDifficultyDropdown;
    [SerializeField] TMP_Text Team1CountLabel;
    [SerializeField] TMP_Text Team2CountLabel;
    [SerializeField] Button StartButton;
    [SerializeField] TMP_Dropdown GameModeDropdown;
    public static int MapID;
    public static int Team1Size;
    public static int Team2Size;
    public static BotsDifficulty BotsDifficulty;
    public static GameMode GameMode;
    public List<MapEntry> indexToMapName;
    [Header("Secondary UI items")]
    [SerializeField] TMP_Text Team1SizeLabel;
    [SerializeField] TMP_Text Team2SizeLabel;

    void Start()
    {
        Team1SizeSlider.onValueChanged.AddListener(delegate
        {
            this.UpdateUI();
        });
        Team2SizeSlider.onValueChanged.AddListener(delegate
        {
            this.UpdateUI();
        });
        GameModeDropdown.onValueChanged.AddListener(delegate
        {
            this.UpdateUI();
        });
        StartButton.onClick.AddListener(delegate
        {
            MapID = MapDropdown.value;
            Team1Size = (int)Team1SizeSlider.value;
            Team2Size = (int)Team2SizeSlider.value;
            BotsDifficulty = (BotsDifficulty)BotsDifficultyDropdown.value;
            GameMode = (GameMode)GameModeDropdown.value;
            LoadScene.LoadSceneGlobally(indexToMapName.Find(x => x.index == MapID).mapName);
        });
    }
    void UpdateUI()
    {
        Team2CountLabel.text = $"{Team2SizeSlider.value}x";
        Team1CountLabel.text = $"{Team1SizeSlider.value}x";
        if(GameModeDropdown.value == 0)
        {
            Team1SizeLabel.text = "1 team size (Player's team):";
            Team2SizeLabel.gameObject.SetActive(true);
            Team2CountLabel.gameObject.SetActive(true);
            Team2SizeSlider.gameObject.SetActive(true);
        }
        else if (GameModeDropdown.value == 1)
        {
            Team1SizeLabel.text = "Players count:";
            Team2SizeLabel.gameObject.SetActive(false);
            Team2CountLabel.gameObject.SetActive(false);
            Team2SizeSlider.gameObject.SetActive(false);
        }
    }
}

[Serializable]
public class MapEntry
{
    public int index;
    public string mapName;
}
public enum GameMode
{
    TwoTeams, FreeForAll
}