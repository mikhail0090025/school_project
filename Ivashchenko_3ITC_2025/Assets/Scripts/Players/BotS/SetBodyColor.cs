using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SetBodyColor : MonoBehaviour
{
    [SerializeField] List<Renderer> BodypartsRenderer;
    [SerializeField] List<Color> AvailableColors;
    [Tooltip("If you set it to true, it will take random color from list, ignoring variable below")]
    [SerializeField] bool RandomColor;
    [SerializeField] Color botsColor;
    public Color BotsColor => botsColor;
    void Start()
    {
        var chosenColor = RandomColor ? AvailableColors[Random.Range(0, AvailableColors.Count)] : botsColor;
        botsColor = chosenColor;
        foreach (var item in BodypartsRenderer)
        {
            item.material.color = chosenColor;
            item.material.SetFloat("_Smoothness", 0f);
        }
    }
}
