using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] Image IndicatorsImage;
    [SerializeField] GameObject Indicator;
    [SerializeField] float ShowTime;
    float ShownTime;
    static DamageIndicator DI;
    Vector2 InitPos;
    const int SizeIndicator = 100;
    void Start()
    {
        DI = this;
        InitPos = IndicatorsImage.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        ShownTime += Time.deltaTime;
        Indicator.SetActive(ShownTime <= ShowTime);
    }
    public static void ShowIndicator(float dir)
    {
        var x = Mathf.Sin(dir);
        var y = Mathf.Cos(dir);
        DI.transform.position = new Vector2(DI.InitPos.x + (x * SizeIndicator), DI.InitPos.y + (y * SizeIndicator));
        DI.ShownTime = 0;
    }
}
