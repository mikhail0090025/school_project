using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpScriptPlayer : HPscript
{
    [SerializeField] GameObject DeathWindow;
    [SerializeField] Image PlayerHP;
    [SerializeField] TMPro.TMP_Text TextHp;
    protected override void Start()
    {
        base.Start();
        DeathWindow.SetActive(false);
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
        DeathWindow.SetActive(true);
    }
}
