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
    [SerializeField] TMP_Text Team1SizeLabel;
    [SerializeField] TMP_Text Team2SizeLabel;
    [SerializeField] Button StartButton;
    public static int MapID;
    public static int Team1Size;
    public static int Team2Size;
    public List<MapEntry> indexToMapName;

    void Start()
    {
        Team1SizeSlider.onValueChanged.AddListener(delegate
        {
            Team1SizeLabel.text = $"{Team1SizeSlider.value}x";
        });
        Team2SizeSlider.onValueChanged.AddListener(delegate
        {
            Team2SizeLabel.text = $"{Team2SizeSlider.value}x";
        });
        StartButton.onClick.AddListener(delegate
        {
            MapID = MapDropdown.value;
            Team1Size = (int)Team1SizeSlider.value;
            Team2Size = (int)Team2SizeSlider.value;
            LoadScene.LoadSceneGlobally(indexToMapName.Find(x => x.index == MapID).mapName);
        });
    }
}

[Serializable]
public class MapEntry
{
    public int index;
    public string mapName;
}