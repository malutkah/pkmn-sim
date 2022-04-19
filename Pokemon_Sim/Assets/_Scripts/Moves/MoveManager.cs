using System;

public static class MoveManager
{
    // handle moves and such like status moves

    public static PokemonInfoHolder attackingPokemon;

    public static int GetMoveCriticalStep(moves attackingMove)
    {
        int step = 1;

        if (attackingMove.ename == "Shadow Claw" || attackingMove.ename == "Blaze Kick"
            || attackingMove.ename == "Poison Tail" || attackingMove.ename == "Sky Attack"
            || attackingMove.ename == "Karate Chop" || attackingMove.ename == "Razor Wind"
            || attackingMove.ename == "crabhammer" || attackingMove.ename == "Cross Chop"
            || attackingMove.ename == "Leaf Blad" || attackingMove.ename == "Aeroblast"
            || attackingMove.ename == "Night Slash" || attackingMove.ename == "Snipe Shot"
            || attackingMove.ename == "Psycho Cut" || attackingMove.ename == "Razor Leaf"
            || attackingMove.ename == "Spacial Rend" || attackingMove.ename == "Attack Order"
            || attackingMove.ename == "Drill Run" || attackingMove.ename == "Slash"
            || attackingMove.ename == "Stone Edge" || attackingMove.ename == "Air Cutter")
        {
            step = 2;
        }

        return step;
    }

    private static void ExecuteMoveEffect(moves attackerMove)
    {

    }

    private static bool DoesAttackDoDamage(moves attackerMove)
    {
        bool doesDamage = false;



        return doesDamage;
    }

    #region all the moves

    private static void Execute_SwordDance()
    {
        // increases the attack stat of the attackin Pokemon for 2 steps (SV)

        
    }

    #endregion
}