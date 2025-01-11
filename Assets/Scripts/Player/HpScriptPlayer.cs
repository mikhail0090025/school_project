using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpScriptPlayer : HPscript
{
    [SerializeField] int DeathWindowIndex;
    [SerializeField] Image PlayerHP;
    [SerializeField] TMPro.TMP_Text TextHp;
    protected override void Start()
    {
        base.Start();
        FindObjectOfType<WindowsManager>().windows[DeathWindowIndex].TurnOff();
        UpdateUI();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UpdateUI();
    }

    protected void UpdateUI()
    {
        TextHp.text = $"{CurrentHP} / {MaxHP}";
        PlayerHP.fillAmount = (float)CurrentHP / MaxHP;
    }
    public override void Dead()
    {
        FindObjectOfType<WindowsManager>().windows[DeathWindowIndex].TurnOn();
    }
}
