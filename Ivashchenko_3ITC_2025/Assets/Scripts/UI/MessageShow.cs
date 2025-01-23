using UnityEngine;
using TMPro;
public class MessageShow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static MessageShow MyInstance;
    [SerializeField] TMPro.TMP_Text labelMessage;
    public TMPro.TMP_Text LabelMessage => labelMessage;
    static float ShowingTime;
    static float ShowingDuration;
    void Start()
    {
        MyInstance = this;
        labelMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowingTime += Time.deltaTime;
        if(ShowingTime > ShowingDuration)
        {
            MyInstance.labelMessage.gameObject.SetActive(false);
        }
    }
    public static void Show(string text, float seconds)
    {
        ShowingTime = 0f;
        ShowingDuration = seconds;
        MyInstance.labelMessage.gameObject.SetActive(true);
        MyInstance.labelMessage.text = text;
    }
}
