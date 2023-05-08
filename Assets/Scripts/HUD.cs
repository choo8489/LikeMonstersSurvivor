using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }

    public InfoType type;
    private Text myText;
    private Slider mySlider;

    private void Awake()
    {
        TryGetComponent(out myText);
        TryGetComponent(out mySlider);
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float currentExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.NextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.NextExp.Length - 1)];
                mySlider.value = currentExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = $"Lv.{GameManager.instance.level}";
                break;
            case InfoType.Kill:
                myText.text = $"{GameManager.instance.kill}";
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.MaxGameTime - GameManager.instance.GameTime;
                myText.text = $"{((int)remainTime / 60):00}:{((int)remainTime % 60):00}";
                break;  
            case InfoType.Health:
                mySlider.value = GameManager.instance.health / GameManager.instance.maxHealth;
                break;
        }
    }
}
