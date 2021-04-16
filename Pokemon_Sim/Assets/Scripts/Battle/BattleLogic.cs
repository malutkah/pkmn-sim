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
    [HideInInspector]
    public IDictionary<GameObject, Transform> pokemonParent;
    public IDictionary<GameObject, Vector3> pokemonPositions;

    private BattleUI ui;
    private LoadBattle loadBattle;
    private PlaySettings settings;

    private void Awake()
    {
        ui = gameObject.GetComponent<BattleUI>();
        loadBattle = gameObject.GetComponent<LoadBattle>();

        pokemonParent = new Dictionary<GameObject, Transform>();
    }

    #region switchting pokemon
    public void SentPokemonIntoBattle(GameObject playerPokemonInTeam)
    {
        settings = GameObject.FindWithTag("Settings").GetComponent<PlaySettings>();

        playerPokemonInTeam.GetComponent<SpriteRenderer>().sortingOrder = 2;
        playerPokemonInTeam.tag = settings.InBattle;
        playerPokemonInTeam.transform.SetParent(GameObject.FindWithTag("BattleStationPlayer").transform);
        playerPokemonInTeam.transform.localPosition = new Vector3(0f, .1f, -9720f);
        playerPokemonInTeam.transform.localScale = new Vector3(1.3f, 1.3f, 108);
    }

    private void SwitchPlayerPokemon()
    {
        loadBattle.GetPokemonParentDict();

        GameObject pokemonInBattle = GameObject.FindWithTag(settings.InBattle);

        pokemonInBattle.transform.SetParent(pokemonParent[pokemonInBattle]);

        ClickedPokemon.transform.parent.GetComponent<Image>().color = new Color(255, 255, 255);

        ClickedPokemon.tag = settings.InBattle;

        ReturnPlayerPokemonToTeam(pokemonInBattle);
        SentPokemonIntoBattle(ClickedPokemon);
    }

    private void ReturnPlayerPokemonToTeam(GameObject pokemonInBattle)
    {
        loadBattle.GetPokemonPositionsDict();

        pokemonInBattle.tag = settings.InBattleTeam;
        pokemonInBattle.transform.localScale = new Vector3(300f, 300f, 1f);
        pokemonInBattle.transform.localPosition = pokemonPositions[pokemonInBattle];
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
        //Debug.Log($"Switching {ClickedPokemon.GetComponent<PokemonInfoHolder>().poke_name} with " +
        //$"{GameObject.FindWithTag("InBattle").GetComponent<PokemonInfoHolder>().poke_name}");

        settings.LastClickedPokemon = null;
        SwitchPlayerPokemon();
        loadBattle.LoadingInfosForPokemonInBattle(ClickedPokemon);
        ui.ShowPokemonMoves();
        SwitchPokemonButton.interactable = false;
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