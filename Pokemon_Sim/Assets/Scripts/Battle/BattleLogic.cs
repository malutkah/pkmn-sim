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
    private PokemonInfoHolder infoHolder;

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

        infoHolder = GameObject.FindWithTag(settings.InBattle).GetComponent<PokemonInfoHolder>();
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

        DecreaseCurrentMovePP(clickedButton);
    }

    public void SwitchPokemon_ButtonClick()
    {
        settings.LastClickedPokemon = null;
        SwitchPlayerPokemon();
        loadBattle.LoadingInfosForPokemonInBattle(ClickedPokemon);
        //
        ShowPokemonCurrentMoves();
        //
        ui.ShowPokemonMoves();
        RefreshPPText();
        SwitchPokemonButton.interactable = false;
    }

    private void ShowPokemonCurrentMoves()
    {
        // show current move pp
    }
    #endregion

    private void RefreshPPText()
    {
        int ppTmp = 0;
        for (int i = 1; i < 5; i++)
        {
            ppTmp = ui.GetCurrentPPFromMove(i);
            SetCurrentPPColor(ppTmp, i);
        }
    }

    #region PowerPoints
    private void DecreaseCurrentMovePP(GameObject clickedButton)
    {
        int currentPP = 0;
        int moveId = 0;

        if (clickedButton.name.Contains("1"))
        {
            currentPP = ui.GetCurrentPPFromMove(1);
            moveId = 1;
        }

        if (clickedButton.name.Contains("2"))
        {
            currentPP = ui.GetCurrentPPFromMove(2);
            moveId = 2;
        }

        if (clickedButton.name.Contains("3"))
        {
            currentPP = ui.GetCurrentPPFromMove(3);
            moveId = 3;
        }

        if (clickedButton.name.Contains("4"))
        {
            currentPP = ui.GetCurrentPPFromMove(4);
            moveId = 4;
        }

        currentPP--;

        SetCurrentPPColor(currentPP, moveId);

        if (currentPP <= 0)
        {
            ui.SetCurrentPPText(moveId, "0");
            //infoHolder.SetCurrentMovePp(moveId, 0);
            clickedButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            ui.SetCurrentPPText(moveId, currentPP.ToString());
            //infoHolder.SetCurrentMovePp(moveId, currentPP);
        }

    }

    private void SetCurrentPPColor(float currentPP, int moveId)
    {
        Color textColor = new Color();
        ColorUtility.TryParseHtmlString("#000000", out textColor);

        //int maxPP = GetPPText_AsInt(clickedButton, ppMaxName);
        int maxPP = ui.GetMaxPPFromMove(moveId);

        float currentPPpercent = currentPP / maxPP * 100;

        if (currentPPpercent <= 60 && currentPPpercent >= 30)
        {
            ui.SetCurrentPPTextColor(moveId, new Color(255, 204, 0, 255));
        }
        else if (currentPPpercent < 31 && currentPPpercent > 0)
        {
            ui.SetCurrentPPTextColor(moveId, new Color(255, 0, 0));
        }
        else if (currentPPpercent > 60 || currentPPpercent == 0)
        {
            ui.SetCurrentPPTextColor(moveId, textColor);
        }
    }
    #endregion
}