using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculations
{
    #region calcutlate pokemon stats
    public static float DoHP(float baseHP, int level)
    {   
        return ((2 * baseHP * level) / 100) + level + 10;
    }

    public static float DoOtherStats(float baseStat, int level)
    {
        return ((2 * baseStat * level) / 100) + 5;
    }

    #endregion

    #region typ calculations
    // double effective = 4x
    // effective = 2x
    // normal = 1x
    // not effection = 0.5
    // no effect = 0



    #endregion

    #region Damage CALC

    /*
     * Schaden = ((Level * (2/5) + 2) * Basischaden * ( [sp.]angr. / (50 * [sp.]vert.)) * F1 + 2 ) * Volltreffer * F2 * (Z / 100) * STAB * Typ1 * Typ2 * F3
     * [Sp.]Angr. gibt den allgemeinen Angriffs- bzw. sp. agriffswert den ANGREIFERS an 
     * volltreffer = 1, bei volltreffer 1.5
     * Z ist ein wert, der berechnet wird, indem von 100 eine random zahl zwischen 0 & 15 abgezogen wird => Z-Wert zwischen 85 & 100
     * STAB = Typ-Bonus -> wenn Angreifer und Attack denselben Typen besitzen. STAB = true ? 1.5 : 1
     * 
     */

    // https://pokefans.net/strategie/artikel/schadensberechnung
    // https://www.pokewiki.de/Schaden

    // BASISSCHADEN
    // Basischaden = RH * BS * IT * LV * LS * NM * AF * ZF
    /*
     * RH: rechte hand default: 1
     * BS: basisstaerke der attacke
     * IT: ItemFaktor: default: 1
     * LV: Ladevorgang default 1
     * LS: 0.5 wenn lehmsuhler default 1
     * NM: 0.5 wenn nassmacher default 1
     * AF: Ability des Angreifers default 1
     * ZF: Ability des Ziels default 1
     */

    #endregion
}
