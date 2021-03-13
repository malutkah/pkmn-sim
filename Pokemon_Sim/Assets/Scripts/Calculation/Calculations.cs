using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculations
{
    // calcutlate pokemon stats
    public static float Calculate_HP(float baseHP, int level)
    {   
        return ((2 * baseHP * level) / 100) + level + 10;
    }

    public static float Calculate_OtherStats(float baseStat, int level)
    {
        return ((2 * baseStat * level) / 100) + 5;
    }
}
