using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    #region moves ui
    [SerializeField]
    private TextMeshProUGUI Move1Text;
    [SerializeField]
    private TextMeshProUGUI Move1MaxPP;
    public TextMeshProUGUI Move1CurrentPP;

    [SerializeField]
    private TextMeshProUGUI Move2Text;
    [SerializeField]
    private TextMeshProUGUI Move2MaxPP;
    public TextMeshProUGUI Move2CurrentPP;

    [SerializeField]
    private TextMeshProUGUI Move3Text;
    [SerializeField]
    private TextMeshProUGUI Move3MaxPP;
    public TextMeshProUGUI Move3CurrentPP;

    [SerializeField]
    private TextMeshProUGUI Move4Text;
    [SerializeField]
    private TextMeshProUGUI Move4MaxPP;
    public TextMeshProUGUI Move4CurrentPP;
    #endregion

    #region HP ui
    [SerializeField] private TextMeshProUGUI PlayerPokemonCurrentHp;
    [SerializeField] private TextMeshProUGUI EnemyPokemonCurrentHp;
    #endregion

    [SerializeField]
    private TextMeshProUGUI DialogText;

    private GameObject playerPokemonInBattle;
    private PokemonMoves pokemonMoves;
    private PokemonInfoHolder infoHolder;

    private void Start()
    {
        ShowPokemonMoves(true);
    }

    public void ShowPokemonMoves(bool firstLoad)
    {
        playerPokemonInBattle = GameObject.FindGameObjectWithTag("InBattle");
        infoHolder = playerPokemonInBattle.GetComponent<PokemonInfoHolder>();
        pokemonMoves = playerPokemonInBattle.GetComponent<PokemonMoves>();

        ShowPokemonMoveText();
        ShowPokemonMovePP(firstLoad);
    }

    #region Show move info
    private void ShowPokemonMoveText()
    {
        Move1Text.text = pokemonMoves.MoveNames[0];
        Move2Text.text = pokemonMoves.MoveNames[1];
        Move3Text.text = pokemonMoves.MoveNames[2];
        Move4Text.text = pokemonMoves.MoveNames[3];
    }

    private void ShowPokemonMovePP(bool firstLoad)
    {
        Move1MaxPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[0]).ToString();
        Move2MaxPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[1]).ToString();
        Move3MaxPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[2]).ToString();
        Move4MaxPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[3]).ToString();

        if (firstLoad)
        {
            Move1CurrentPP.text = infoHolder.GetCurrentMovePp(1).ToString();
            Move2CurrentPP.text = infoHolder.GetCurrentMovePp(2).ToString();
            Move3CurrentPP.text = infoHolder.GetCurrentMovePp(3).ToString();
            Move4CurrentPP.text = infoHolder.GetCurrentMovePp(4).ToString();
        }
    }
    #endregion

    #region Coroutines
    public IEnumerator TypeSentence(string sentence)
    {
        DialogText.text = "";

        foreach (var letter in sentence)
        {
            DialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator BattleIntroSentence(string sentence)
    {
        StartCoroutine(TypeSentence(sentence));

        yield return new WaitForSeconds(3f);

        StartCoroutine(TypeSentence("Choose your move!"));
    }
    #endregion

    #region Power Points
    public int GetCurrentPPFromMove(int moveNumber)
    {
        switch (moveNumber)
        {
            case 1:
                return Convert.ToInt32(Move1CurrentPP.text);
            case 2:
                return Convert.ToInt32(Move2CurrentPP.text);
            case 3:
                return Convert.ToInt32(Move3CurrentPP.text);
            case 4:
                return Convert.ToInt32(Move4CurrentPP.text);
            default:
                return 0;
        }
    }

    public int GetMaxPPFromMove(int moveNumber)
    {
        return moveNumber == 1 ? Convert.ToInt32(Move1MaxPP.text) :
            moveNumber == 2 ? Convert.ToInt32(Move2MaxPP.text) :
            moveNumber == 3 ? Convert.ToInt32(Move3MaxPP.text) :
            moveNumber == 4 ? Convert.ToInt32(Move4MaxPP.text) : 0;
    }

    public void SetCurrentPPTextColor(int moveNumber, Color textColor)
    {
        //DONE: like SetCurrentPPText with switch
        switch (moveNumber)
        {
            case 1:
                Move1CurrentPP.color = textColor;
                break;
            case 2:
                Move2CurrentPP.color = textColor;
                break;
            case 3:
                Move3CurrentPP.color = textColor;
                break;
            case 4:
                Move4CurrentPP.color = textColor;
                break;
        }
    }

    public void SetCurrentPPText(int moveNumber, string value)
    {
        switch (moveNumber)
        {
            case 1:
                Move1CurrentPP.text = value;
                break;
            case 2:
                Move2CurrentPP.text = value;
                break;
            case 3:
                Move3CurrentPP.text = value;
                break;
            case 4:
                Move4CurrentPP.text = value;
                break;
        }
    }
    #endregion

    #region HP

    public void SetCurrentPlayerHPText(float newHp)
    {
        PlayerPokemonCurrentHp.text = Mathf.Round(newHp).ToString();
    }

    public void SetCurrentEnemyHPText(float newHp)
    {
        EnemyPokemonCurrentHp.text = Mathf.Round(newHp).ToString();
    }

    #endregion
}
