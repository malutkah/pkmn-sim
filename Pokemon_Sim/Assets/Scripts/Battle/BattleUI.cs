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
    private TextMeshProUGUI Move1MaxPP, Move1CurrentPP;

    [SerializeField]
    private TextMeshProUGUI Move2Text;
    [SerializeField]
    private TextMeshProUGUI Move2MaxPP, Move2CurrentPP;

    [SerializeField]
    private TextMeshProUGUI Move3Text;
    [SerializeField]
    private TextMeshProUGUI Move3MaxPP, Move3CurrentPP;

    [SerializeField]
    private TextMeshProUGUI Move4Text;
    [SerializeField]
    private TextMeshProUGUI Move4MaxPP, Move4CurrentPP;
    #endregion

    [SerializeField]
    private TextMeshProUGUI DialogText;

    private GameObject playerPokemonInBattle;
    private PokemonMoves pokemonMoves;

    private void Start()
    {
        ShowPokemonMoves();
    }

    public void ShowPokemonMoves()
    {
        playerPokemonInBattle = GameObject.FindGameObjectWithTag("InBattle");
        pokemonMoves = playerPokemonInBattle.GetComponent<PokemonMoves>();

        ShowPokemonMoveText();
        ShowPokemonMovePP();
    }

    #region Show move info
    private void ShowPokemonMoveText()
    {
        Move1Text.text = pokemonMoves.MoveNames[0];
        Move2Text.text = pokemonMoves.MoveNames[1];
        Move3Text.text = pokemonMoves.MoveNames[2];
        Move4Text.text = pokemonMoves.MoveNames[3];
    }

    private void ShowPokemonMovePP()
    {
        Move1MaxPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[0]).ToString();
        Move2MaxPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[1]).ToString();
        Move3MaxPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[2]).ToString();
        Move4MaxPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[3]).ToString();

        Move1CurrentPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[0]).ToString();
        Move2CurrentPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[1]).ToString();
        Move3CurrentPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[2]).ToString();
        Move4CurrentPP.text = pokemonMoves.GetMovePP(pokemonMoves.MoveNames[3]).ToString();
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
        return moveNumber == 1 ? Convert.ToInt32(Move1CurrentPP.text) : 
            moveNumber == 2 ? Convert.ToInt32(Move2CurrentPP.text) :
            moveNumber == 3 ? Convert.ToInt32(Move3CurrentPP.text) : 
            moveNumber == 4 ? Convert.ToInt32(Move4CurrentPP.text) : 0;
    }

    public int GetMaxPPFromMove(int moveNumber)
    {
        return moveNumber == 1 ? Convert.ToInt32(Move1MaxPP.text) :
            moveNumber == 2 ? Convert.ToInt32(Move2MaxPP.text) :
            moveNumber == 3 ? Convert.ToInt32(Move3MaxPP.text) :
            moveNumber == 4 ? Convert.ToInt32(Move4MaxPP.text) : 0;
    }
    #endregion

}
