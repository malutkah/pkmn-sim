using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Types
{
    public const string normal = "Normal";
    public const string fire = "Fire";
    public const string fly = "Flying";
    public const string water = "Water";
    public const string dark = "Dark";
    public const string fairy = "Fairy";
    public const string poison = "Poison";
    public const string bug = "Bug";
    public const string ground = "Ground";
    public const string rock = "Rock";
    public const string elec = "Electric";
    public const string steel = "Steel";
    public const string grass = "Grass";
    public const string ghost = "Ghost";
    public const string ice = "Ice";
    public const string fight = "Fighting";
    public const string dragon = "Dragon";
    public const string psy = "Psychic";

    #region Type calculations

    public static float NormalVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == normal || pokemonType == fight || pokemonType == fly || pokemonType == poison || pokemonType == ground
            || pokemonType == elec || pokemonType == grass || pokemonType == water || pokemonType == fire || pokemonType == bug
            || pokemonType == psy || pokemonType == ice || pokemonType == dragon || pokemonType == dark || pokemonType == fairy)
        {
            value = 1f;
        }
        else if (pokemonType == rock || pokemonType == steel)
        {
            value = 0.5f;
        }

        return value;
    }

    public static float FightingVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == normal || pokemonType == rock || pokemonType == steel || pokemonType == ice || pokemonType == dark)
        {
            value = 2f;
        }
        else if (pokemonType == fight || pokemonType == ground || pokemonType == fire || pokemonType == water || pokemonType == grass
             || pokemonType == elec || pokemonType == dragon)
        {
            value = 1f;
        }
        else if (pokemonType == fly || pokemonType == poison || pokemonType == bug || pokemonType == psy || pokemonType == fairy)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float FlyingVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == fight || pokemonType == bug || pokemonType == grass)
        {
            value = 2f;
        }
        else if (pokemonType == normal || pokemonType == fly || pokemonType == poison || pokemonType == ground || pokemonType == ghost
             || pokemonType == fire || pokemonType == water || pokemonType == psy || pokemonType == ice || pokemonType == dragon
              || pokemonType == dark || pokemonType == fairy)
        {
            value = 1f;
        }
        else if (pokemonType == rock || pokemonType == steel || pokemonType == elec)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float PoisonVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == grass || pokemonType == fairy)
        {
            value = 2f;
        }
        else if (pokemonType == normal || pokemonType == fight || pokemonType == fly || pokemonType == bug || pokemonType == fire
             || pokemonType == water || pokemonType == dragon || pokemonType == dark || pokemonType == elec || pokemonType == psy
             || pokemonType == ice)
        {
            value = 1f;
        }
        else if (pokemonType == poison || pokemonType == ground || pokemonType == rock || pokemonType == ghost)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float GroundVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == poison || pokemonType == rock || pokemonType == fire || pokemonType == steel || pokemonType == elec)
        {
            value = 2f;
        }
        else if (pokemonType == normal || pokemonType == fight || pokemonType == ground || pokemonType == ghost || pokemonType == dark
         || pokemonType == water || pokemonType == psy || pokemonType == ice || pokemonType == dragon || pokemonType == fairy)
        {
            value = 1f;
        }
        else if (pokemonType == bug || pokemonType == grass)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float RockVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == fly || pokemonType == bug || pokemonType == fire || pokemonType == ice)
        {
            value = 2f;
        }
        else if (pokemonType == normal || pokemonType == grass || pokemonType == water || pokemonType == elec || pokemonType == fairy
         || pokemonType == poison || pokemonType == ghost || pokemonType == dark || pokemonType == psy || pokemonType == dragon
          || pokemonType == rock)
        {
            value = 1f;
        }
        else if (pokemonType == fight || pokemonType == ground || pokemonType == steel)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float BugVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == grass || pokemonType == psy || pokemonType == dark)
        {
            value = 2f;
        }
        else if (pokemonType == normal || pokemonType == ground || pokemonType == rock || pokemonType == bug || pokemonType == water
         || pokemonType == elec || pokemonType == ice || pokemonType == dragon)
        {
            value = 1f;
        }
        else if (pokemonType == fight || pokemonType == steel || pokemonType == fire || pokemonType == poison || pokemonType == ghost
         || pokemonType == fly || pokemonType == fairy)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float GhostVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == ghost || pokemonType == psy)
        {
            value = 2f;
        }
        else if (pokemonType == poison || pokemonType == bug || pokemonType == fire || pokemonType == rock || pokemonType == dark
         || pokemonType == steel || pokemonType == fairy || pokemonType == water || pokemonType == ice || pokemonType == grass || pokemonType == dragon
          || pokemonType == fly || pokemonType == ground || pokemonType == elec)
        {
            value = 1f;
        }
        else if (pokemonType == dark)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float SteelVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == rock || pokemonType == ice || pokemonType == fairy)
        {
            value = 2f;
        }
        else if (pokemonType == poison || pokemonType == bug || pokemonType == rock || pokemonType == dark
         || pokemonType == grass || pokemonType == dragon || pokemonType == fly || pokemonType == ground
          || pokemonType == normal || pokemonType == fight || pokemonType == psy || pokemonType == ghost)
        {
            value = 1f;
        }
        else if (pokemonType == steel || pokemonType == fire || pokemonType == water || pokemonType == elec)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float FireVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == bug || pokemonType == ice || pokemonType == steel || pokemonType == grass)
        {
            value = 2f;
        }
        else if (pokemonType == poison || pokemonType == bug || pokemonType == rock || pokemonType == dark
         || pokemonType == fly || pokemonType == ground || pokemonType == normal || pokemonType == fight ||
           pokemonType == psy || pokemonType == ghost)
        {
            value = 1f;
        }
        else if (pokemonType == rock || pokemonType == fire || pokemonType == water || pokemonType == dragon)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float WaterVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == rock || pokemonType == ground || pokemonType == fire)
        {
            value = 2f;
        }
        else if (pokemonType == poison || pokemonType == bug || pokemonType == ice || pokemonType == dark
         || pokemonType == steel || pokemonType == fairy || pokemonType == fly || pokemonType == elec
          || pokemonType == normal || pokemonType == fight || pokemonType == psy || pokemonType == ghost)
        {
            value = 1f;
        }
        else if (pokemonType == dragon || pokemonType == water || pokemonType == grass)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float GrassVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == rock || pokemonType == ground || pokemonType == water)
        {
            value = 2f;
        }
        else if (pokemonType == ice || pokemonType == dark || pokemonType == fairy || pokemonType == elec
        || pokemonType == normal || pokemonType == fight || pokemonType == psy || pokemonType == ghost)
        {
            value = 1f;
        }
        else if (pokemonType == fly || pokemonType == poison || pokemonType == bug || pokemonType == steel
         || pokemonType == fire || pokemonType == grass || pokemonType == dragon)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float ElectricVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == water || pokemonType == fly)
        {
            value = 2f;
        }
        else if (pokemonType == poison || pokemonType == bug || pokemonType == rock || pokemonType == dark
         || pokemonType == grass || pokemonType == fairy || pokemonType == rock || pokemonType == normal
         || pokemonType == fight || pokemonType == psy || pokemonType == ghost || pokemonType == fire)
        {
            value = 1f;
        }
        else if (pokemonType == grass || pokemonType == dragon || pokemonType == elec)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float PsychicVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == poison || pokemonType == fight)
        {
            value = 2f;
        }
        else if (pokemonType == bug || pokemonType == rock || pokemonType == rock || pokemonType == grass
        || pokemonType == dragon || pokemonType == fly || pokemonType == ground || pokemonType == normal
        || pokemonType == ice || pokemonType == ghost || pokemonType == fire || pokemonType == elec
         || pokemonType == water)
        {
            value = 1f;
        }
        else if (pokemonType == steel || pokemonType == psy)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float IceVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == dragon || pokemonType == fly || pokemonType == grass || pokemonType == ground)
        {
            value = 2f;
        }
        else if (pokemonType == poison || pokemonType == bug || pokemonType == rock || pokemonType == dark
         || pokemonType == elec || pokemonType == fairy || pokemonType == normal || pokemonType == fight
         || pokemonType == psy || pokemonType == ghost)
        {
            value = 1f;
        }
        else if (pokemonType == steel || pokemonType == fire || pokemonType == water || pokemonType == ice)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float DragonVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == dragon)
        {
            value = 2f;
        }
        else if (pokemonType == poison || pokemonType == bug || pokemonType == rock || pokemonType == dark
         || pokemonType == grass || pokemonType == dragon || pokemonType == fly || pokemonType == ground
          || pokemonType == normal || pokemonType == fight || pokemonType == psy || pokemonType == ghost
          || pokemonType == fire || pokemonType == water || pokemonType == elec || pokemonType == ice)
        {
            value = 1f;
        }
        else if (pokemonType == steel)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float DarkVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == ghost  || pokemonType == psy )
        {
            value = 2f;
        }
        else if (pokemonType == poison || pokemonType == bug || pokemonType == rock || pokemonType == elec
         || pokemonType == grass || pokemonType == dragon || pokemonType == fly || pokemonType == ground
          || pokemonType == normal || pokemonType == fight || pokemonType == water || pokemonType == fire
          || pokemonType == ice )
        {
            value = 1f;
        }
        else if (pokemonType == fight || pokemonType == dark || pokemonType == fairy)
        {
            value = 0.5f;
        }

        return value;
    }
    public static float FairyVs(string pokemonType)
    {
        float value = 0f;

        if (pokemonType == fight || pokemonType == dragon || pokemonType == dark)
        {
            value = 2f;
        }
        else if (pokemonType == elec || pokemonType == bug || pokemonType == rock || pokemonType == dark
         || pokemonType == grass || pokemonType == ice || pokemonType == fly || pokemonType == ground
          || pokemonType == normal || pokemonType == fairy || pokemonType == psy || pokemonType == ghost)
        {
            value = 1f;
        }
        else if (pokemonType == steel || pokemonType == fire || pokemonType == poison)
        {
            value = 0.5f;
        }

        return value;
    }

    #endregion
}
