using UnityEngine;

public class GamemodeUI : MonoBehaviour
{
    [SerializeField] GameObject TeamsLabel;
    void Start()
    {
        TeamsLabel.SetActive(NewGameSettings.GameMode == 0);
    }
}
