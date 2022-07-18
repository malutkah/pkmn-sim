using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager is responsible for:
///     - executing moves and calculate the damage         
///         - checking the pokemon and attck type
///     - managing the round-based system
///     - controlling the enemy AI
///     - checking if game is over (who won?)
/// </summary>
/// 

public enum Phases { PLAYER_TURN, ENEMY_TURN, PLAYER_WON, ENEMY_WON, ATTACKING, SWITCHING, CHOOSING }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject PokemonInBattle_Enemy;
    public GameObject PokemonInBattle_Player;
    public EnemyAI ai;
    public BattleLogic logic;
    public BattleUI ui;

    public Phases Phase;

    public bool EnemyAttacked;

    private bool gameOver;
    private PokemonInfoHolder playerInfoHolder, enemyInfoHolder;

    #region Unity

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Phase = Phases.CHOOSING;

        PokemonInBattle_Enemy = GameObject.FindGameObjectWithTag("InBattleEnemy");
        PokemonInBattle_Player = GameObject.FindGameObjectWithTag("InBattle");

        ai.SetEnemyInBattle(PokemonInBattle_Enemy);

        playerInfoHolder = PokemonInBattle_Player.GetComponent<PokemonInfoHolder>();
        enemyInfoHolder = PokemonInBattle_Enemy.GetComponent<PokemonInfoHolder>();
    }

    #endregion

    #region Battle

    public bool IsPlayerFaster()
    {
        return playerInfoHolder.speed > enemyInfoHolder.speed;
    }

    public void SwitchEnemyPokemon(GameObject pkmn)
    {
        ai.SetEnemyInBattle(pkmn);
    }

    public void ExecuteEnemyAttack(string moveName)
    {
        WriteDialogText($"{enemyInfoHolder.poke_name} uses {moveName}");
        logic.EnemyAttack(moveName);
    }

    public void EnemyDecides()
    {
        ai.Execute();
    }

    #region Text

    public void WriteDialogText(string text)
    {
        ui.WriteDialogText(text);
    }

    #endregion

    #endregion
}
