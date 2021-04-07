using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleLogic : MonoBehaviour
{
    private BattleUI ui;

    private void Awake()
    {
        ui = gameObject.GetComponent<BattleUI>();
    }

    public void SentPokemonIntoBattle(GameObject playerPokemon, GameObject battleStation, PlaySettings settings)
    {
        playerPokemon.tag = settings.InBattle;
        playerPokemon.transform.SetParent(battleStation.transform);
        playerPokemon.transform.localPosition = new Vector3(0f, .1f, -9720f);
        playerPokemon.transform.localScale = new Vector3(1.3f, 1.3f, 108);
    }

    #region switchting pokemon
    private void SwitchPlayerPokemon(GameObject pokemonInTeam, GameObject battleStation, PlaySettings settings, IDictionary<GameObject, Transform> pokemonParent)
    {
        GameObject pokemonInBattle = GameObject.FindWithTag(settings.InBattle);

        pokemonInBattle.tag = settings.InBattleTeam;
        pokemonInBattle.transform.SetParent(pokemonParent[pokemonInBattle]);

        pokemonInTeam.tag = settings.InBattle;

        SentPokemonIntoBattle(pokemonInTeam, battleStation, settings);
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
    #endregion

    #region PowerPoints
    private void DecreaseMovePP(GameObject clickedButton)
    {
        int currentPP = 0;
        string ppName = "";

        if (clickedButton.name.Contains("1"))
        {
            currentPP = GetPPText_AsInt(clickedButton, "Move1CurrentPP");
            ppName = "Move1CurrentPP";
        }

        if (clickedButton.name.Contains("2"))
        {
            currentPP = GetPPText_AsInt(clickedButton, "Move2CurrentPP");
            ppName = "Move2CurrentPP";
        }

        if (clickedButton.name.Contains("3"))
        {
            currentPP = GetPPText_AsInt(clickedButton, "Move3CurrentPP");
            ppName = "Move3CurrentPP";
        }

        if (clickedButton.name.Contains("4"))
        {
            currentPP = GetPPText_AsInt(clickedButton, "Move4CurrentPP");
            ppName = "Move4CurrentPP";
        }

        currentPP--;

        // change current pp color

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

    private void SetCurrentPPColor(int currentPP)
    {

    }

    private int GetPPText_AsInt(GameObject clickedButton, string move)
    {
        return Convert.ToInt32(clickedButton.gameObject.transform.Find(move).GetComponent<TextMeshProUGUI>().text);
    }

    #endregion
}