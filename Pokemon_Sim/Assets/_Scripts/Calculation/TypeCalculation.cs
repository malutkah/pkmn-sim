using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeCalculation : MonoBehaviour
{
    public TMP_Dropdown offenseDrop;
    public TMP_Dropdown defenseDrop1;
    public TMP_Dropdown defenseDrop2;
    public TMP_Text valueText;

    private float effectValueMultiplicator;
    private string defenseType1;
    private string defenseType2;
    private string offenseType;

    private void Start()
    {
        CalculateType();
    }

    public void CalculateType()
    {
        offenseType = offenseDrop.options[offenseDrop.value].text;
        defenseType1 = defenseDrop1.options[defenseDrop1.value].text;
        defenseType2 = defenseDrop2.options[defenseDrop2.value].text;

        float type1 = Calculations.GetEffectivness(defenseType1, offenseType);
        float type2 = 1f;

        if (defenseType2 != "None")
        {
            type2 = Calculations.GetEffectivness(defenseType2, offenseType);
        }

        effectValueMultiplicator = type1 * type2;

        string tmpText = effectValueMultiplicator == 2 ? "Super Effective" : effectValueMultiplicator == 0.5f ? "Not Effective" : effectValueMultiplicator == 1 ? "Normal" : effectValueMultiplicator == 4 ? "Double super effective" : "Nothing";

        valueText.text = tmpText; // effectValueMultiplicator.ToString();
    }
}
