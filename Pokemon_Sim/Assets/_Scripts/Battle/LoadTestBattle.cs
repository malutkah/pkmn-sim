using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadTestBattle : MonoBehaviour
{
    [SerializeField] private GameObject PlayerHealthBar;
    [SerializeField] private int Level;

    #region UI Elements
    [SerializeField] private TextMeshProUGUI PlayerPokemonName;
    [SerializeField] private TextMeshProUGUI PlayerPokemonMaxHp;
    [SerializeField] private TextMeshProUGUI PlayerPokemonCurrentHp;
    [SerializeField] private TextMeshProUGUI PlayerPokemonLevel;

    [SerializeField] private TextMeshProUGUI EnemyPokemonName;
    [SerializeField] private TextMeshProUGUI EnemyPokemonMaxHp;
    [SerializeField] private TextMeshProUGUI EnemyPokemonCurrentHp;
    [SerializeField] private TextMeshProUGUI EnemyPokemonLevel;
    #endregion

    public GameObject PlayerPokemonPrefab;
    public GameObject EnemyPokemonPrefab;
    public GameObject BattleStationPlayer;
    public GameObject BattleStationEnemy;
    public JsonReader reader;

    private pokemon playerPkmn, enemyPkmn;
    private PokemonMoves playerMoves, enemyMoves;

    private PokemonInfoHolder playerInfoHolder, enemyInfoHolder;

    private BattleLogic logic;

    private void Awake()
    {
        logic = GetComponent<BattleLogic>();

        playerInfoHolder = PlayerPokemonPrefab.GetComponent<PokemonInfoHolder>();
        enemyInfoHolder = EnemyPokemonPrefab.GetComponent<PokemonInfoHolder>();
        playerMoves = PlayerPokemonPrefab.GetComponent<PokemonMoves>();
        enemyMoves = EnemyPokemonPrefab.GetComponent<PokemonMoves>();

        playerMoves.Init();
        playerMoves.Init();

        LoadPlayerPokemon();
        LoadEnemyPokemon();

        CalculatePokemonBaseStats(playerInfoHolder, playerPkmn);
        CalculatePokemonBaseStats(enemyInfoHolder, enemyPkmn);

        SetCurrentPokemonMovePP();

        logic.SetPkmnInBattle(PlayerPokemonPrefab, true);
        logic.SetPkmnInBattle(EnemyPokemonPrefab, false);

        InitializePlayerPokemonUiText();
        InitializeEnemyPokemonUiText();
    }

    private void InitializePlayerPokemonUiText()
    {
        PlayerPokemonName.text = playerPkmn.name.english;
        PlayerPokemonMaxHp.text = Mathf.Round(playerInfoHolder.hp).ToString();
        PlayerPokemonCurrentHp.text = Mathf.Round(playerInfoHolder.hp).ToString(); // DONE: runden
        PlayerPokemonLevel.text = playerInfoHolder.level.ToString();
    }

    private void InitializeEnemyPokemonUiText()
    {
        EnemyPokemonName.text = enemyPkmn.name.english;
        EnemyPokemonMaxHp.text = Mathf.Round(enemyInfoHolder.hp).ToString();
        EnemyPokemonCurrentHp.text = Mathf.Round(enemyInfoHolder.hp).ToString(); // DONE: runden
        EnemyPokemonLevel.text = enemyInfoHolder.level.ToString();
    }

    private void SetCurrentPokemonMovePP()
    {
        for (int i = 0; i < 4; i++)
        {
            playerInfoHolder.SetCurrentMovePp(i+1, playerMoves.GetMovePP(playerMoves.MoveNames[i]));
            enemyInfoHolder.SetCurrentMovePp(i+1, playerMoves.GetMovePP(playerMoves.MoveNames[i]));
        }
    }

    private void CalculatePokemonBaseStats(PokemonInfoHolder infoHolder, pokemon pkmn)
    {
        infoHolder.level = Level;
        infoHolder.hp = Calculations.DoHP(pkmn.@base.hp, Level);
        infoHolder.attack = Calculations.DoOtherStats(pkmn.@base.attack, Level);
        infoHolder.defense = Calculations.DoOtherStats(pkmn.@base.defense, Level);
        infoHolder.spAttack = Calculations.DoOtherStats(pkmn.@base.sp_attack, Level);
        infoHolder.spDefense = Calculations.DoOtherStats(pkmn.@base.sp_defense, Level);
        infoHolder.speed = Calculations.DoOtherStats(pkmn.@base.speed, Level);
    }

    private void LoadPlayerPokemon()
    {
        var id = Convert.ToInt32(PlayerPokemonPrefab.name.Substring(0, 3));
        playerPkmn = new pokemon();

        playerPkmn = reader.GetPokemons().pokemon.Find(p => p.id == id);
    }

    private void LoadEnemyPokemon()
    {
        var id = Convert.ToInt32(EnemyPokemonPrefab.name.Substring(0, 3));
        enemyPkmn = new pokemon();

        enemyPkmn = reader.GetPokemons().pokemon.Find(p => p.id == id);

        Debug.Log($"{enemyPkmn.type[0]}");
    }
}
