using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadTestBattle : MonoBehaviour
{
    [SerializeField] private GameObject PlayerHealthBar;

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

    private PokemonInfoHolder playerInfoHolder, enemyInfoHolder;

    private void Awake()
    {
        playerInfoHolder = PlayerPokemonPrefab.GetComponent<PokemonInfoHolder>();
        enemyInfoHolder = EnemyPokemonPrefab.GetComponent<PokemonInfoHolder>();
    }

    void Start()
    {
        LoadPlayerPokmeon();
        LoadEnemyPokmeon();
    }

    private void InitializePlayerPokemonUiText()
    {
        PlayerPokemonName.text = playerPkmn.name.english;
        PlayerPokemonMaxHp.text = playerInfoHolder.hp.ToString();
        PlayerPokemonCurrentHp.text = Mathf.Round(playerInfoHolder.hp).ToString(); // DONE: runden
        PlayerPokemonLevel.text = playerInfoHolder.level.ToString();
    }

    private void InitializeEnemyPokemonUiText()
    {
        EnemyPokemonName.text = enemyPkmn.name.english;
        EnemyPokemonMaxHp.text = enemyInfoHolder.hp.ToString();
        EnemyPokemonCurrentHp.text = Mathf.Round(enemyInfoHolder.hp).ToString(); // DONE: runden
        EnemyPokemonLevel.text = enemyInfoHolder.level.ToString();
    }

    private void LoadPlayerPokmeon()
    {
        var id = Convert.ToInt32(PlayerPokemonPrefab.name.Substring(0, 3));
        playerPkmn = new pokemon();

        reader.GetPokemons().pokemon.Find(p => p.id == id);
    }

    private void LoadEnemyPokmeon()
    {
        var id = Convert.ToInt32(EnemyPokemonPrefab.name.Substring(0, 3));
        enemyPkmn = new pokemon();

        reader.GetPokemons().pokemon.Find(p => p.id == id);
    }
}
