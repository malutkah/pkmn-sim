using System.Collections;
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

    #region Calculation extras

    private static bool useSpecial = false;
    private static bool isStatusMove = false;

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

    public static float GetEffectivness(string pokemonType, string _moveType)
    {
        float effectValueMultiplicator = 0f;

        //Debug.Log($"Pokemon type {pokemonType}");
        //Debug.Log($"Move type {_moveType}");

        switch (_moveType)
        {
            case Types.normal:
                effectValueMultiplicator = Types.NormalVs(pokemonType);
                break;
            case Types.fight:
                effectValueMultiplicator = Types.FightingVs(pokemonType);
                break;
            case Types.fly:
                effectValueMultiplicator = Types.FlyingVs(pokemonType);
                break;
            case Types.poison:
                effectValueMultiplicator = Types.PoisonVs(pokemonType);
                break;
            case Types.ground:
                effectValueMultiplicator = Types.GroundVs(pokemonType);
                break;
            case Types.rock:
                effectValueMultiplicator = Types.RockVs(pokemonType);
                break;
            case Types.bug:
                effectValueMultiplicator = Types.BugVs(pokemonType);
                break;
            case Types.ghost:
                effectValueMultiplicator = Types.GhostVs(pokemonType);
                break;
            case Types.steel:
                effectValueMultiplicator = Types.SteelVs(pokemonType);
                break;
            case Types.fire:
                effectValueMultiplicator = Types.FireVs(pokemonType);
                break;
            case Types.water:
                effectValueMultiplicator = Types.WaterVs(pokemonType);
                break;
            case Types.grass:
                effectValueMultiplicator = Types.GrassVs(pokemonType);
                break;
            case Types.elec:
                effectValueMultiplicator = Types.ElectricVs(pokemonType);
                break;
            case Types.psy:
                effectValueMultiplicator = Types.PsychicVs(pokemonType);
                break;
            case Types.ice:
                effectValueMultiplicator = Types.IceVs(pokemonType);
                break;
            case Types.dragon:
                effectValueMultiplicator = Types.DragonVs(pokemonType);
                break;
            case Types.dark:
                effectValueMultiplicator = Types.DarkVs(pokemonType);
                break;
            case Types.fairy:
                effectValueMultiplicator = Types.FairyVs(pokemonType);
                break;
        }

        //Debug.Log($"Effect Value Multi: {effectValueMultiplicator}");
        return effectValueMultiplicator;
    }

    #endregion

    #region Damage Calculation

    public static float DoDamageCalculation(moves attackerMove, PokemonInfoHolder playerPokemonInfo, PokemonInfoHolder enemyPokemonInfo, bool playerAttack)
    {
        PokemonInfoHolder attackingPokemon = playerAttack ? playerPokemonInfo : enemyPokemonInfo;
        PokemonInfoHolder defendingPokemon = playerAttack ? enemyPokemonInfo : playerPokemonInfo;

        //Debug.Log($"is enemy info null? {enemyPokemonInfo == null}");

        InitStats(attackerMove, playerPokemonInfo, enemyPokemonInfo);
        MoveManager.attackingPokemon = attackingPokemon;

        float baseDamage = 1f;
        float F1 = 1f, F2 = 1f, F3 = 1f;
        float critical = 1f;
        int criticalStep = 1;
        float critAccuracy = 4.16f;
        float Z = 0f;
        float type1 = 1f;
        float type2 = 1f;
        bool stab = false;
        float stabBonus = 1f;
        int level = 0;
        float sp_attack = 1f;
        float sp_defense = 1f;
        isStatusMove = false;

        // player attacks enemy
        if (playerAttack)
        {
            // player move type VS. enemy's type
            type1 = GetEffectivness(e_primaryType, moveType);
            level = playerPokemonInfo.level;

            type2 = e_secondaryType != "" ? GetEffectivness(e_secondaryType, moveType) : 1;
        }
        else
        {
            // enemy attacks player
            // enemy move type VS. player's type
            type1 = GetEffectivness(p_primaryType, moveType);
            level = enemyPokemonInfo.level;

            type2 = p_secondaryType != "" ? GetEffectivness(p_secondaryType, moveType) : 1;
        }

        if (attackerMove.category == "physical")
        {
            useSpecial = false;
        }
        else if (attackerMove.category == "special")
        {
            useSpecial = true;

            if (attackerMove.power == 0)
            {
                isStatusMove = true;
            }
        }

        criticalStep = MoveManager.GetMoveCriticalStep(attackerMove);

        critAccuracy = criticalStep == 2 ? 12.5f : criticalStep == 3 ? 50f : criticalStep == 4 ? 100 : critAccuracy;

        critical = WasACritical(critAccuracy) ? 1.5f : critical;

        if (critical == 1.5f) Debug.Log("Critical Hit!");

        stab = attackerMove.type == attackingPokemon.PrimaryType || (type2 != 1f ? attackerMove.type == attackingPokemon.SecondaryType : false);
        stabBonus = stab ? 1.5f : stabBonus;

        Z = Random.Range(85, 100);

        baseDamage = CalaculateBasedamage(attackerMove);
        //Debug.Log($"Base Damage: {baseDamage}");

        F1 = Calculate_F1(attackerMove, attackingPokemon);
        F2 = Calculate_F2();
        F3 = Calculate_F3();

        if (!isStatusMove)
        {
            sp_attack = CalculateSp_Attack(attackerMove, attackingPokemon);
            sp_defense = CalculateSp_Defense(attackerMove, attackingPokemon);
        }
        else
        {
            // do status move
            Debug.Log($"Well, shit");
            return 0;
        }

        //Debug.Log($"(({level} * ({2} / {5}) + {2}) * {baseDamage} * ({sp_attack} / ({50} * {sp_defense})) * {F1} + {2}) * {critical} * {F2} * ({Z} / {100}) * {stabBonus} * {type1} * {type2} * {F3}");

        // version 1 => not very accurate
        var totalDamage1 = ((level * (2 / 5) + 2) * baseDamage * (sp_attack / (50 * sp_defense)) * F1 + 2) * critical * F2 * (Z / 100) * stabBonus * type1 * type2 * F3;

        // version 2
        var _attack = useSpecial ? attackingPokemon.spAttack : attackingPokemon.attack;
        var _defense = useSpecial ? defendingPokemon.spDefense : defendingPokemon.defense;

        var totalDamage2 = ( ( ( (2 * level) / 5 ) + 2 * attackerMove.power * _attack / _defense ) / 50 + 2 ) * 1 * 1 * 1 * critical * (Z / 100) * stabBonus * type1 * type2;

        return totalDamage2 * 10;
    }

    #endregion

    private static bool WasACritical(float critAccuracy)
    {
        return Random.Range(0, 100) <= critAccuracy;
    }

    private static bool IsAttackStab(moves attackMove, PokemonInfoHolder attackinPokemon)
    {
        if (attackMove.type == attackinPokemon.PrimaryType)
        {
            return true;
        }
        else if (attackMove.type == attackinPokemon.SecondaryType)
        {
            return true;
        }

        return false;
    }

    private static float CalculateSp_Attack(moves attackerMove, PokemonInfoHolder attackinPokemon)
    {
        float attack = 0f;
        float stat = 1f;
        float sv = 1f;
        float ff = 1f;
        float _if = 1f;

        if (!useSpecial)
        {
            // pyhsical attack
            stat = attackinPokemon.attack;
            sv = attackinPokemon.pokemonStatStepValue_attack;
        }
        else
        {
            // special attack
            sv = attackinPokemon.pokemonStatStepValue_spAttack;

            // attack is a status moves
            if (attackerMove.power == 0)
            {
                isStatusMove = true;
                return 1f;
            }
        }

        attack = stat * sv * ff * _if;

        return attack;
    }

    private static float CalculateSp_Defense(moves attackerMove, PokemonInfoHolder attackingPokemon)
    {
        float defense = 0f;
        float stat = 1f;
        float mod = 1f;
        float sv = 1f;

        if (!useSpecial)
        {
            // pyhsical attack
            stat = attackingPokemon.defense;
            sv = attackingPokemon.pokemonStatStepValue_defense;
        }
        else if (attackerMove.category == "special")
        {
            // special attack
            sv = attackingPokemon.pokemonStatStepValue_spDefense;
            stat = attackingPokemon.spDefense;
        }

        defense = stat * sv * mod;

        return defense;
    }

    private static float CalaculateBasedamage(moves attackerMove)
    {
        float value = 0f;
        float rh = 1;
        float bs = 1;
        float it = 1;
        float lv = 1;
        float ls = 1;
        float nm = 1;
        float af = 1;
        float zf = 1;

        ls = attackerMove.ename == "Mud Sport" ? 0.5f : 1f;
        nm = attackerMove.ename == "Water Sport" ? 0.5f : 1f;

        bs = attackerMove.power;

        value = rh * bs * it * lv * ls * nm * af * zf;

        return value;
    }

    private static float Calculate_F1(moves attackingMove, PokemonInfoHolder attackingPokemon)
    {
        float value = 1f;
        float burn = 1f;
        float twoVs2 = 1f;
        float sr = 1f;
        float ff = 1f;
        float rl = 1f;

        // get if attacking pokemon is burning
        // get if reflector or light shield is active

        value = burn * rl * twoVs2 * sr * ff;

        return value;
    }

    private static float Calculate_F2()
    {
        return 1f;
    }

    private static float Calculate_F3()
    {
        return 1f;
    }

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
     * SR: sonnen-/regentanz, default: 1 | Regnet es, beträgt der Faktor für Wasser-Attacken 1,5 und für Feuer-Attacken℅0,5. Scheint dagegen die Sonne, gilt für Feuer-Attacken der Wert 1,5 und für Wasser-Attacken 0,5
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
     * nachtnebel: schaden in hoehe des enemy levels
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
