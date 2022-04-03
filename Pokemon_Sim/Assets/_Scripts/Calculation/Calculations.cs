﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculations
{
    #region Player Pokemon Stats

    private static float p_hp;
    private static float p_attack;
    private static float p_defense;
    private static float p_spAttack;
    private static float p_spDefense;
    private static float p_speed;
    private static int p_level;
    private static string p_poke_name;
    private static string p_primaryType;
    private static string p_secondaryType;

    #endregion

    #region Enemy Pokemon Stats

    private static float e_hp;
    private static float e_attack;
    private static float e_defense;
    private static float e_spAttack;
    private static float e_spDefense;
    private static float e_speed;
    private static int e_level;
    private static string e_poke_name;
    private static string e_primaryType;
    private static string e_secondaryType;

    #endregion

    #region Move stats

    private static string moveName;
    private static string moveType;
    private static string category;
    private static int accuracy;
    private static int power;

    #endregion

    private static void InitStats(moves attackerMove, PokemonInfoHolder playerPokemonInfo, PokemonInfoHolder enemyPokemonInfo)
    {
        p_hp = playerPokemonInfo.hp;
        p_attack = playerPokemonInfo.attack;
        p_defense = playerPokemonInfo.defense;
        p_spAttack = playerPokemonInfo.spAttack;
        p_spDefense = playerPokemonInfo.spDefense;
        p_speed = playerPokemonInfo.speed;
        p_level = playerPokemonInfo.level;
        p_poke_name = playerPokemonInfo.poke_name;
        p_primaryType = playerPokemonInfo.PrimaryType;
        p_secondaryType = playerPokemonInfo.SecondaryType;

        e_hp = enemyPokemonInfo.hp;
        e_attack = enemyPokemonInfo.attack;
        e_defense = enemyPokemonInfo.defense;
        e_spAttack = enemyPokemonInfo.spAttack;
        e_spDefense = enemyPokemonInfo.spDefense;
        e_speed = enemyPokemonInfo.speed;
        e_level = enemyPokemonInfo.level;
        e_poke_name = enemyPokemonInfo.poke_name;
        e_primaryType = enemyPokemonInfo.PrimaryType;
        e_secondaryType = enemyPokemonInfo.SecondaryType;

        moveName = attackerMove.ename;
        moveType = attackerMove.type;
        category = attackerMove.category;
        accuracy = attackerMove.accuracy;
        power = attackerMove.power;
    }

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

    #region type calculations
    // double effective = 4x
    // effective = 2x
    // normal = 1x
    // not effection = 0.5
    // no effect = 0

    private static float GetEffectivness(string pokemonType)
    {
        float effectValueMultiplicator = 0f;

        switch (moveType)
        {
            case Types.normal:
                effectValueMultiplicator = Types.NormalVs(pokemonType);
                break;

            default:
                break;
        }

        return effectValueMultiplicator;
    }

    #endregion

    #region Damage Calculation

    public static float DoDamageCalculation(moves attackerMove, PokemonInfoHolder playerPokemonInfo, PokemonInfoHolder enemyPokemonInfo, bool playerAttack)
    {
        InitStats(attackerMove, playerPokemonInfo, enemyPokemonInfo);

        float totalDamage = 0f;
        float baseDamage = 1f;
        float F1 = 1f, F2 = 1f, F3 = 1f;
        float critical = 1f;
        float Z = 0f;
        float type1 = 1f;
        float type2 = 1f;
        bool stab = false;
        float stabBonus = 1f;

        // player attacks enemy
        if (playerAttack)
        {
            // player move type VS. enemy's type
            type1 = GetEffectivness(e_primaryType);

            if (p_secondaryType != "")
            {
                type2 = GetEffectivness(e_secondaryType);
            }
        }




        return totalDamage;
    }

    #endregion

    #region Damage calc info

    // total damage
    /*
     * Schaden = ((Level * (2/5) + 2) * Basischaden * ( [sp.]angr. / (50 * [sp.]vert.)) * F1 + 2 ) * Volltreffer * F2 * (Z / 100) * STAB * Typ1 * Typ2 * F3
     * 
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

    // Attack & Defense
    /*
     * [Sp.]Angr. = Stat * SV * FF * IF
     * Stat: Basiswert
     * SV: Stufensystem
     * ------------------------------------------------------------------------
     * |  -6  |  -5 |  -4 |  -3 |  -2 |  -1 | 0 |  1  | 2 |  3  | 4 |  5  | 6 |
     * | 0.25 | 2/7 | 1/3 | 0.4 | 0.5 | 2/3 | 1 | 1.5 | 2 | 2.5 | 3 | 3.5 | 4 |
     * ------------------------------------------------------------------------
     * 
     * FF: Ability default: 1
     * IF: Item default: 1
     * 
     * [Sp.]Def. = Stat * SV * Mod
     * Mod: default: 1
     */

    // Faktor F1
    /*
     * F1 = BRT * RL * 2V2 * SR * FF
     * BRT = 0.5 wenn verbrennung, default: 1
     * RL: default 1 | Reflektor & Lichtschild | Reflektor? RL 0.5 for physisch angr., Lichtschild? RL 0.5 for spezial angr.
     * 2V2 = 1
     * FF = 1 (Feuerfaenger)
     */

    // Faktor F2
    /*
     * Default: 1
     * if Lebensorb: 1.3
     * 
     */

    // Faktor F3
    /*
     * F3 = FKF * EG * A * B
     * FKF = 1 | 0.5, wenn der gegner die ability felskern oder filter hat und die attacke sehr effektiv ist
     * EG = 1 | 1.2 wenn anwender expertengurt hat und eingesetzte attacke sehr effektiv ist
     * A = 1 | 2, wenn anwender die ability aufwertung hat und die attack nicht sehr effektiv ist
     * B = 1 | 0.5, wenn die attacke zum konsum einer schadensreduzierenden Beere furht
     */

    // Ausnahmen:
    /*
     * schmarotzer orientiert sich am angriff des gegners 
     * durchbruch ingnoriert reflektor & lichtschild
     * psychoschock, psychostoss, mystoschwert richten physichen schaden an, als vert. des gengers
     * sanctoklinge used normalen verteidungs wert
     * drachenwut & ultrschall richten immer gleich viel schaden an, ausser der gegner hat weniger als 40 bzw. 20 KP
     * notsituation setzt kp des gegners auf die des anwenders
     * superzahn & naturzorn halbieren immer die KP
     * guillotine, hornbohrer, eiseskalte, geofissur immer one hit => schaden = kp des gegners
     * psywelle: schaden = Level * x | x => rnd 0.5 - 1.5
     */

    // Volltreffer
    /* 
     * step 1: 4.16%
     * step 2: 12.5%
     * step 3: 50%
     * step 4: 100%
     */
    /* attacken mit step 2
     * dunkelklaue
     * feuerfeger
     * giftschweif
     * giftstreich
     * himmelsfeger
     * karateschlag
     * klingensturm
     * krabbhammer
     * kreuzhieb
     * laubklinge
     * luftstoss
     * nachthibe
     * prazisionsschuss
     * psychoklinge
     * rasierblatt
     * raumschlag
     * schlagbefehl
     * schlagbohrer
     * schlitzer
     * steinkante
     * windschnitt
     */
    /* attacken mit step 3
     * tausendfacher donnerblitz
     */
    /*attacken mit 100% volltreffer
     * bergsturm
     * eisesodem
     * finstertreffer
     * trefferschwall
     */

    #endregion


}
