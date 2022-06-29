using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PokemonStatus
{
    NONE,
    BURNED,
    FROZEN,
    PARALYZED,
    POISONED,
    SLEEPING,
    CONFUSED,
    DEAD
}

public class PokemonInfoHolder : MonoBehaviour
{
    public GameObject ClickedPokemon;

    public PokemonStatus status;

    public float CurrentPokemonHp;

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

    public float pokemonStatStepValue_attack;
    public float pokemonStatStepValue_defense;

    public float pokemonStatStepValue_spAttack;
    public float pokemonStatStepValue_spDefense;
    private int pokemonStatStep_a;
    private int pokemonStatStep_d;
    private int pokemonStatStep_spd;
    private int pokemonStatStep_spa;

    private void Start()
    {
        status = PokemonStatus.NONE;
        pokemonStatStepValue_attack = 1f;
        pokemonStatStepValue_spAttack = 1f;
        pokemonStatStepValue_defense = 1f;
        pokemonStatStepValue_spDefense = 1f;

        pokemonStatStep_a = 0;
        pokemonStatStep_d = 0;
        pokemonStatStep_spa = 0;
        pokemonStatStep_spd = 0;
    }

    public string PrimaryType;
    public string SecondaryType;

    #region Pokemon steps

    //* ------------------------------------------------------------------------
    //* |  -6  |  -5 |  -4 |  -3 |  -2 |  -1 | 0 |  1  | 2 |  3  | 4 |  5  | 6 |
    //* | 0.25 | 2/7 | 1/3 | 0.4 | 0.5 | 2/3 | 1 | 1.5 | 2 | 2.5 | 3 | 3.5 | 4 |
    //* ------------------------------------------------------------------------

    public void IncreasePokemonStatStep_Attack(int increaseStepByValue)
    {
        pokemonStatStep_a = pokemonStatStep_a < 6 ? pokemonStatStep_a += increaseStepByValue : pokemonStatStep_a;
        pokemonStatStepValue_attack = pokemonStatStepValue_attack < 4 ? increaseStepByValue * 0.5f : pokemonStatStepValue_attack;
    }

    public void IncreasePokemonStatStep_Defense(int increaseStepByValue)
    {
        pokemonStatStep_d = pokemonStatStep_d < 6 ? pokemonStatStep_d += increaseStepByValue : pokemonStatStep_d;
        pokemonStatStepValue_defense = pokemonStatStepValue_defense < 4 ? increaseStepByValue * 0.5f : pokemonStatStepValue_defense;
    }

    public void IncreasePokemonStatStep_SpAttack(int increaseStepByValue)
    {
        pokemonStatStep_spa = pokemonStatStep_spa < 6 ? pokemonStatStep_spa += increaseStepByValue : pokemonStatStep_spa;
        pokemonStatStepValue_spAttack = pokemonStatStepValue_spAttack < 4 ? increaseStepByValue * 0.5f : pokemonStatStepValue_spAttack;
    }

    public void IncreasePokemonStatStep_SpDefense(int increaseStepByValue)
    {
        pokemonStatStep_spd = pokemonStatStep_spd < 6 ? pokemonStatStep_spd += increaseStepByValue : pokemonStatStep_spd;
        pokemonStatStepValue_spDefense = pokemonStatStepValue_spDefense < 4 ? increaseStepByValue * 0.5f : pokemonStatStepValue_spDefense;
    }

    public void DecreasePokemonStatStep_Attack(int decreaseStepValueBy)
    {
        pokemonStatStep_a = pokemonStatStep_a < 6 ? pokemonStatStep_a -= decreaseStepValueBy : pokemonStatStep_a;
        pokemonStatStepValue_attack = GetDefenseStepValue(pokemonStatStep_a);
    }

    public void DecreasePokemonStatStep_Defense(int decreaseStepValueBy)
    {
        pokemonStatStep_d = pokemonStatStep_d < 6 ? pokemonStatStep_d -= decreaseStepValueBy : pokemonStatStep_d;
        pokemonStatStepValue_defense = GetDefenseStepValue(pokemonStatStep_d);
    }

    public void DecreasePokemonStatStep_SpAttack(int decreaseStepValueBy)
    {
        pokemonStatStep_spa = pokemonStatStep_spa < 6 ? pokemonStatStep_spa -= decreaseStepValueBy : pokemonStatStep_spa;
        pokemonStatStepValue_spAttack = GetDefenseStepValue(pokemonStatStep_spa);
    }

    public void DecreasePokemonStatStep_SpDefense(int decreaseStepValueBy)
    {
        pokemonStatStep_spd = pokemonStatStep_spd < 6 ? pokemonStatStep_spd -= decreaseStepValueBy : pokemonStatStep_spd;
        pokemonStatStepValue_spDefense = GetDefenseStepValue(pokemonStatStep_spd);
    }

    private float GetDefenseStepValue(int step)
    {
        float stepValue = 0f;

        stepValue = step == 0 ? 1f : step == -1 ? (2 / 3) : step == -2 ?
                    .5f : step == -3 ? .4f : step == -4 ? (1 / 3) : step == -5 ?
                    (2 / 7) : step == -6 ? .25f : 1f;

        return stepValue;
    }

    private void SetPokemonStatStepValue()
    {
        if (pokemonStatStepValue_attack < 4)
        {

        }
    }

    #endregion

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
