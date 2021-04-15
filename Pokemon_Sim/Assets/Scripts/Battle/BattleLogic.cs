using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleLogic : MonoBehaviour
{
    public Button SwitchPokemonButton;

    [HideInInspector]
    public GameObject ClickedPokemon;

    private BattleUI ui;

    private void Awake()
    {
        ui = gameObject.GetComponent<BattleUI>();
    }

    public void SentPokemonIntoBattle(GameObject playerPokemon, GameObject battleStation, PlaySettings settings)
    {
        playerPokemon.tag = settings.InBattle;
        playerPokemon.GetComponent<SpriteRenderer>().sortingOrder = 2;
        playerPokemon.transform.SetParent(battleStation.transform);
        playerPokemon.transform.localPosition = new Vector3(0f, .1f, -9720f);
        playerPokemon.transform.localScale = new Vector3(1.3f, 1.3f, 108);
    }

    #region switchting pokemon
    private void SwitchPlayerPokemon(GameObject battleStation, PlaySettings settings, IDictionary<GameObject, Transform> pokemonParent)
    {
        GameObject pokemonInBattle = GameObject.FindWithTag(settings.InBattle);

        pokemonInBattle.tag = settings.InBattleTeam;
        pokemonInBattle.transform.SetParent(pokemonParent[pokemonInBattle]);

        ClickedPokemon.tag = settings.InBattle;

        SentPokemonIntoBattle(ClickedPokemon, battleStation, settings);
    }
    #endregion

    #region button clicks
    public void ExecuteMove_ButtonClick()
    {
        // get button name
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        string moveName = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;

        // display debug text
        //Debug.Log(moveName);

        // decrease current pp
        DecreaseMovePP(clickedButton);
    }

    public void SwitchPokemon_ButtonClick()
    {
        Debug.Log($"Switching {ClickedPokemon.GetComponent<PokemonInfoHolder>().poke_name} with {GameObject.FindWithTag("InBattle").GetComponent<PokemonInfoHolder>().poke_name}");
    }
    #endregion

    #region PowerPoints
    private void DecreaseMovePP(GameObject clickedButton)
    {
        float currentPP = 0;
        string ppName = "";
        string ppMaxName = "";

        if (clickedButton.name.Contains("1"))
        {
            currentPP = GetPPText_AsInt(clickedButton, "Move1CurrentPP");
            ppName = "Move1CurrentPP";
            ppMaxName = "Move1MaxPP";
        }

        if (clickedButton.name.Contains("2"))
        {
            currentPP = GetPPText_AsInt(clickedButton, "Move2CurrentPP");
            ppName = "Move2CurrentPP";
            ppMaxName = "Move2MaxPP";
        }

        if (clickedButton.name.Contains("3"))
        {
            currentPP = GetPPText_AsInt(clickedButton, "Move3CurrentPP");
            ppName = "Move3CurrentPP";
            ppMaxName = "Move3MaxPP";
        }

        if (clickedButton.name.Contains("4"))
        {
            currentPP = GetPPText_AsInt(clickedButton, "Move4CurrentPP");
            ppName = "Move4CurrentPP";
            ppMaxName = "Move4MaxPP";
        }

        currentPP--;

        // change current pp color
        SetCurrentPPColor(currentPP, clickedButton, ppName, ppMaxName);

        if (currentPP <= 0)
        {
            clickedButton.gameObject.transform.Find(ppName).GetComponent<TextMeshProUGUI>().text = "0";
            clickedButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            clickedButton.gameObject.transform.Find(ppName).GetComponent<TextMeshProUGUI>().text = currentPP.ToString();
        }

    }

    private void SetCurrentPPColor(float currentPP, GameObject clickedButton, string ppName, string ppMaxName)
    {
        Color textColor = new Color();
        ColorUtility.TryParseHtmlString("#000000", out textColor);

        float maxPP = GetPPText_AsInt(clickedButton, ppMaxName);

        float currentPPpercent = currentPP / maxPP * 100;

        if (currentPPpercent <= 60 && currentPPpercent >= 30)
        {
            clickedButton.gameObject.transform.Find(ppName).GetComponent<TextMeshProUGUI>().color = new Color(255, 204, 0, 255);
        }
        else if (currentPPpercent < 31 && currentPPpercent > 0)
        {
            clickedButton.gameObject.transform.Find(ppName).GetComponent<TextMeshProUGUI>().color = new Color(255, 0, 0);
        }
        else if (currentPPpercent > 60 || currentPPpercent == 0)
        {
            clickedButton.gameObject.transform.Find(ppName).GetComponent<TextMeshProUGUI>().color = textColor;
        }
    }

    private int GetPPText_AsInt(GameObject clickedButton, string move)
    {
        return Convert.ToInt32(clickedButton.gameObject.transform.Find(move).GetComponent<TextMeshProUGUI>().text);
    }

    #endregion
}