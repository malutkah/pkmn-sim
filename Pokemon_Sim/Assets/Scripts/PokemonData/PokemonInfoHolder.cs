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

    public void SetCurrentMovePp(int moveId, int value)
    {
        Move1CurrentPp = moveId == 1 ? Move1CurrentPp = value :
            Move2CurrentPp = moveId == 2 ? Move2CurrentPp = value :
            Move3CurrentPp = moveId == 3 ? Move3CurrentPp = value :
            Move4CurrentPp = moveId == 4 ? Move4CurrentPp = value : Move4CurrentPp = 0;
    }

    public int GetCurrentMovePp(int moveId)
    {
        return moveId == 1 ? Move1CurrentPp :
            moveId == 2 ? Move2CurrentPp :
            moveId == 3 ? Move3CurrentPp :
            moveId == 4 ? Move4CurrentPp : 0;
    }
}
