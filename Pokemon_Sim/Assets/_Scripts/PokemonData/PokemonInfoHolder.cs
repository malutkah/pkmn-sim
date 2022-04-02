using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonInfoHolder : MonoBehaviour
{
    public GameObject ClickedPokemon;

    #region Pokemon stats    
    public float hp;
    public float attack;
    public float defense;
    public float spAttack;
    public float spDefense;
    public float speed;
    public int level;
    public string poke_name;
    #endregion

    #region pp
    public int Move1CurrentPp;
    public int Move2CurrentPp;
    public int Move3CurrentPp;
    public int Move4CurrentPp;
    #endregion

    public string PrimaryType;
    public string SecondaryType;

    private PokemonMoves pokeMoves;

    private void Awake()
    {
        pokeMoves = GetComponent<PokemonMoves>();
    }

    public void SetCurrentMovePp(int moveId, int value)
    {
        if (moveId == 1)
        {
            Move1CurrentPp = value;
        }
        if (moveId == 2)
        {
            Move2CurrentPp = value;
        }
        if (moveId == 3)
        {
            Move3CurrentPp = value;
        }
        if (moveId == 4)
        {
            Move4CurrentPp = value;
        }

    }

    public int GetCurrentMovePp(int moveId)
    {
        if (moveId == 1)
        {
            return Move1CurrentPp;
        }
        else if (moveId == 2)
        {
            return Move2CurrentPp;
        }
        else if (moveId == 3)
        {
            return Move3CurrentPp;
        }
        else if (moveId == 4)
        {
            return Move4CurrentPp;
        }
        else
        {
            return -1;
        }
    }
}
