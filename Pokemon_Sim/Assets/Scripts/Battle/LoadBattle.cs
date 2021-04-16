using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadBattle : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerHealthBar;

    #region UI Elements
    [SerializeField]
    private TextMeshProUGUI PlayerPokemonName;
    [SerializeField]
    private TextMeshProUGUI PlayerPokemonMaxHp;
    [SerializeField]
    private TextMeshProUGUI PlayerPokemonCurrentHp;
    [SerializeField]
    private TextMeshProUGUI PlayerPokemonLevel;
    #endregion

    #region battle stations
    [SerializeField]
    private GameObject BattleStationPlayer;
    //[SerializeField]
    private GameObject BattleStationEnemy;
    #endregion

    #region Pokemon stats
    private float hp;
    private float attack;
    private float defense;
    private float spAttack;
    private float spDefense;
    private float speed;
    private int level = 5;
    #endregion

    #region player team slots
    [SerializeField]
    private Image PlayerTeamSlot1;
    [SerializeField]
    private Image PlayerTeamSlot2;
    [SerializeField]
    private Image PlayerTeamSlot3;
    [SerializeField]
    private Image PlayerTeamSlot4;
    [SerializeField]
    private Image PlayerTeamSlot5;
    [SerializeField]
    private Image PlayerTeamSlot6;
    #endregion

    #region script objects
    private BattleUI ui;
    private BattleLogic logic;
    private PlaySettings settings;
    private pokemon pokemon;
    private JsonReader reader;
    private PokemonInfoHolder infoHolder;
    private PlayerTeam playerTeam;
    private HealthBar playerHealthBar;
    #endregion

    private List<GameObject> teamList;
    private Vector3 resetPosition = new Vector3(0f, 0f, 0f);
    private Vector3 pokemonLocalScale = new Vector3(300f, 300f, 1f);

    private int teamSize = 0;
    private IDictionary<GameObject, Vector3> pokemonPositions = new Dictionary<GameObject, Vector3>();
    private IDictionary<GameObject, Transform> pokemonParent = new Dictionary<GameObject, Transform>();

    /// <summary>
    /// - get pokemon data (hp etc.) for setting health bar value
    /// - load pokemon info (json) from teamList
    /// - set health bar value
    /// - set health text (current HP, max HP) 
    /// </summary>

    #region Unity
    private void Awake()
    {
        playerTeam = GameObject.Find("TeamHandler").GetComponent<PlayerTeam>();
        reader = GameObject.FindWithTag("JsonReader").GetComponent<JsonReader>();
        settings = GameObject.FindWithTag("Settings").GetComponent<PlaySettings>();
        logic = gameObject.GetComponent<BattleLogic>();
        ui = gameObject.GetComponent<BattleUI>();

        if (playerTeam.GetTeam() != null)
        {
            teamList = playerTeam.GetTeam();
            teamSize = teamList.Count;
        }

        LoadPlayerPokemonInImageSlots();
    }
    #endregion

    public void GetPokemonParentDict()
    {
        logic.pokemonParent = pokemonParent;
    }

    public void GetPokemonPositionsDict()
    {
        logic.pokemonPositions = pokemonPositions;
    }

    #region Loading Battle Stuff
    private void LoadPlayerPokemonInImageSlots()
    {
        for (int i = 0; i < teamSize; i++)
        {
            if (i == 0)
            {
                pokemonPositions.Add(teamList[i], resetPosition);
                pokemonParent.Add(teamList[i], PlayerTeamSlot1.transform);

                LoadingInfosForPokemonInBattle(teamList[i]);
                logic.SentPokemonIntoBattle(teamList[i]);

                /* teamList[i].transform.SetParent(PlayerTeamSlot1.transform);
                 teamList[i].transform.localPosition = resetPosition;
                 teamList[i].transform.localScale = pokemonLocalScale; */
            }

            if (i == 1)
            {
                pokemonPositions.Add(teamList[i], resetPosition);
                pokemonParent.Add(teamList[i], PlayerTeamSlot2.transform);

                teamList[i].tag = settings.InBattleTeam;
                teamList[i].transform.SetParent(PlayerTeamSlot2.transform);
                teamList[i].transform.localPosition = resetPosition;
                teamList[i].transform.localScale = pokemonLocalScale;
            }

            if (i == 2)
            {
                pokemonPositions.Add(teamList[i], resetPosition);
                pokemonParent.Add(teamList[i], PlayerTeamSlot3.transform);

                teamList[i].tag = settings.InBattleTeam;
                teamList[i].transform.SetParent(PlayerTeamSlot3.transform);
                teamList[i].transform.localPosition = resetPosition;
                teamList[i].transform.localScale = pokemonLocalScale;
            }

            if (teamSize == 6)
            {
                if (i == 3)
                {
                    pokemonPositions.Add(teamList[i], resetPosition);
                    pokemonParent.Add(teamList[i], PlayerTeamSlot4.transform);

                    teamList[i].tag = settings.InBattleTeam;
                    teamList[i].transform.SetParent(PlayerTeamSlot4.transform);
                    teamList[i].transform.localPosition = resetPosition;
                    teamList[i].transform.localScale = pokemonLocalScale;
                }

                if (i == 4)
                {
                    pokemonPositions.Add(teamList[i], resetPosition);
                    pokemonParent.Add(teamList[i], PlayerTeamSlot5.transform);

                    teamList[i].tag = settings.InBattleTeam;
                    teamList[i].transform.SetParent(PlayerTeamSlot5.transform);
                    teamList[i].transform.localPosition = resetPosition;
                    teamList[i].transform.localScale = pokemonLocalScale;
                }

                if (i == 5)
                {
                    pokemonPositions.Add(teamList[i], resetPosition);
                    pokemonParent.Add(teamList[i], PlayerTeamSlot6.transform);

                    teamList[i].tag = settings.InBattleTeam;
                    teamList[i].transform.SetParent(PlayerTeamSlot6.transform);
                    teamList[i].transform.localPosition = resetPosition;
                    teamList[i].transform.localScale = pokemonLocalScale;
                }
            }
        }
    }
    #endregion

    #region Pokemon
    public void LoadingInfosForPokemonInBattle(GameObject playerPokemon)
    {
        infoHolder = playerPokemon.GetComponent<PokemonInfoHolder>();

        LoadPokemonInfo(playerPokemon);

        Debug.Log($"{pokemon.name.english} was sent into battle");
        StartCoroutine(ui.BattleIntroSentence($"{pokemon.name.english} was sent into battle"));

        InitializePlayerPokemonUiText();
    }

    private void InitializePlayerPokemonUiText()
    {
        PlayerPokemonName.text = pokemon.name.english;
        PlayerPokemonMaxHp.text = infoHolder.hp.ToString();
        PlayerPokemonCurrentHp.text = infoHolder.hp.ToString();
        PlayerPokemonLevel.text = infoHolder.level.ToString();
    }

    private void LoadPokemonInfo(GameObject pkmn)
    {
        int id = Convert.ToInt32(pkmn.name.Substring(0, 3));

        pokemon = new pokemon();
        pokemon = reader.GetPokemons().pokemon.Find(p => p.id == id);
    }
    #endregion
}
