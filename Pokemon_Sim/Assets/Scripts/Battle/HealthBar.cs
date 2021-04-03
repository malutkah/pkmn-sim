using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider playerHpSlider;

    public void SetMaxPlayerHp(float hp)
    {
        playerHpSlider.maxValue = hp;
        playerHpSlider.value = hp;
    }


    public void SetPlayerHP(float hp)
    {
        playerHpSlider.value = hp;
    }
}
