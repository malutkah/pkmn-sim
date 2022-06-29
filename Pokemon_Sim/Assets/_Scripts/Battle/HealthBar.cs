using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;

    public void SetMaxHp(float hp)
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }


    public void SetHP(float hp)
    {
        hpSlider.value -= hp;
    }
}
