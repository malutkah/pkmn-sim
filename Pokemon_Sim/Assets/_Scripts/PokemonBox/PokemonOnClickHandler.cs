﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PokemonOnClickHandler : MonoBehaviour
{
    private PlayerTeam team;
    private PlaySettings settings;
    private PokemonInfoHolder infoHolder;
    private PokemonMoves pokeMoves;
    private JsonReader reader;
    private BattleLogic logic;

    private Image ImagePkmn;
    private pokemon pokemon;
    private GameObject JsonReader;
    private GameObject clickedPokemon;
    private GameObject oldSender;
    private Button add, remove;

    #region Pokemon Stats Text
    private Text Text_Name;
    private Text Text_Level;
    private Text Text_Hp;
    private Text Text_Attack;
    private Text Text_Defense;
    private Text Text_SpAttack;
    private Text Text_SpDefense;
    private Text Text_Speed;
    #endregion

    #region Move Text
    private Text Text_Move1;
    private Text Text_Move2;
    private Text Text_Move3;
    private Text Text_Move4;

    #endregion

    private string pkmn_name = "";
    private string name_eng = "";
    private int pkmn_id = 0;

    private bool isClicked = false;
    private bool firstLoad;

    #region Pokemon stats
    private float hp;
    private float attack;
    private float defense;
    private float spAttack;
    private float spDefense;
    private float speed;
    private int level = 5;
    #endregion

    private Color originalButtonColor;

    #region Unity
    private void Awake()
    {
        infoHolder = gameObject.GetComponent<PokemonInfoHolder>();
        pokeMoves = GetComponent<PokemonMoves>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "BattleCalcuTesting")
        {

            JsonReader = GameObject.Find("reader");
            settings = GameObject.Find("Settings_Handler").GetComponent<PlaySettings>();
            team = GameObject.Find("TeamHandler").GetComponent<PlayerTeam>();
            add = GameObject.Find("ButtonAddToTeam").GetComponent<Button>();
            remove = GameObject.Find("ButtonRemoveFromTeam").GetComponent<Button>();
            ImagePkmn = GameObject.Find("ImagePkmn").GetComponent<Image>();

            if (settings != null)
            {
                level = settings.level;
            }

            if (JsonReader != null)
            {
                reader = JsonReader.GetComponent<JsonReader>();
            }

            InitText();

            remove.enabled = false;
            firstLoad = true;
            originalButtonColor = remove.image.color;
        }
    }
    #endregion

    private void Load()
    {
        pokemon = new pokemon();
        pokemon = reader.GetPokemons().pokemon.Find(p => p.id == pkmn_id);
    }

    private void HighlightSelectedPokemon(GameObject sender)
    {
        if (settings.LastClickedPokemon == null)
        {
            sender.transform.parent.GetComponent<Image>().color = new Color(255, 0, 0);

            logic.SwitchPokemonButton.interactable = true;
        }

        if (settings.LastClickedPokemon != null && settings.LastClickedPokemon == sender)
        {
            if (!isClicked)
            {
                sender.transform.parent.GetComponent<Image>().color = new Color(255, 255, 255);
                isClicked = true;

                logic.SwitchPokemonButton.interactable = false;
            }
            else
            {
                sender.transform.parent.GetComponent<Image>().color = new Color(255, 0, 0);
                isClicked = false;

                logic.SwitchPokemonButton.interactable = true;
            }

            settings.LastClickedPokemon = null;
        }

        if (settings.LastClickedPokemon != null && settings.LastClickedPokemon != sender)
        {
            sender.transform.parent.GetComponent<Image>().color = new Color(255, 0, 0);
            settings.LastClickedPokemon.transform.parent.GetComponent<Image>().color = new Color(255, 255, 255);

            isClicked = isClicked ? isClicked = false : isClicked;

            logic.SwitchPokemonButton.interactable = true;
        }

        settings.LastClickedPokemon = sender;
    }

    public void PokemonOnClick(GameObject sender)
    {
        team.ClickedPokemon = sender;
        infoHolder.ClickedPokemon = sender;

        LoadPokemonData();

        if (firstLoad)
        {
            infoHolder.SetCurrentMovePp(1, pokeMoves.GetMovePP(pokeMoves.MoveNames[0]));
            infoHolder.SetCurrentMovePp(2, pokeMoves.GetMovePP(pokeMoves.MoveNames[1]));
            infoHolder.SetCurrentMovePp(3, pokeMoves.GetMovePP(pokeMoves.MoveNames[2]));
            infoHolder.SetCurrentMovePp(4, pokeMoves.GetMovePP(pokeMoves.MoveNames[3]));
            firstLoad = false;
        }

        name_eng = pokemon.name.english;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("BattleScene"))
        {
            Debug.Log($"{name_eng} is in Battle Scene");
            logic = GameObject.Find("BattleManager").GetComponent<BattleLogic>();

            logic.ClickedPokemon = sender;

            if (sender.tag == settings.InBattleTeam)
            {
                HighlightSelectedPokemon(sender);
            }
        }
        else
        {

            if (sender.tag == settings.InTeam)
            {
                EnableButtonsAddPokemonToTeam(false);
            }
            else
            {
                team.PokemonOrigPos = sender.transform.localPosition;

                EnableButtonsAddPokemonToTeam(true);
            }

            CalculatePokemonStats();

            infoHolder.PrimaryType = pokemon.type[0];
            if (pokemon.type.Count == 2)
            {
                infoHolder.SecondaryType = pokemon.type[1];
            }

            ShowText();
            ShowImage();
        }
    }

    private void LoadPokemonData()
    {
        pkmn_name = gameObject.name;

        pkmn_id = Convert.ToInt32(pkmn_name.Substring(0, 3));

        Load();
    }

    private void CalculatePokemonStats()
    {
        infoHolder.level = level;
        infoHolder.poke_name = pokemon.name.english;

        hp = Calculations.DoHP(pokemon.@base.hp, level);
        infoHolder.hp = hp;

        attack = Calculations.DoOtherStats(pokemon.@base.attack, level);
        infoHolder.attack = attack;

        defense = Calculations.DoOtherStats(pokemon.@base.defense, level);
        infoHolder.defense = defense;

        spAttack = Calculations.DoOtherStats(pokemon.@base.sp_attack, level);
        infoHolder.spAttack = spAttack;

        spDefense = Calculations.DoOtherStats(pokemon.@base.sp_defense, level);
        infoHolder.spDefense = spDefense;

        speed = Calculations.DoOtherStats(pokemon.@base.speed, level);
        infoHolder.speed = speed;
    }

    private void EnableButtonsAddPokemonToTeam(bool addButtonEnabled)
    {
        add.enabled = addButtonEnabled;
        remove.enabled = !addButtonEnabled;

        if (addButtonEnabled)
        {
            add.image.color = originalButtonColor;

            remove.image.color = new Color(255.0f, 255.0f, 255.0f, 0.1f);
        }
        else
        {
            add.image.color = new Color(255.0f, 255.0f, 255.0f, 0.1f);

            remove.image.color = originalButtonColor;
        }
    }

    #region Text and Image
    private void InitText()
    {
        Text_Name = GameObject.Find("TextPkmnName").GetComponent<Text>();
        Text_Level = GameObject.Find("TextPkmnLevel").GetComponent<Text>();
        Text_Hp = GameObject.Find("TextPkmnHp").GetComponent<Text>();
        Text_Attack = GameObject.Find("TextPkmnAtk").GetComponent<Text>();
        Text_Defense = GameObject.Find("TextPkmnDef").GetComponent<Text>();
        Text_SpAttack = GameObject.Find("TextPkmnSpAtk").GetComponent<Text>();
        Text_SpDefense = GameObject.Find("TextPkmnSpDef").GetComponent<Text>();
        Text_Speed = GameObject.Find("TextPkmnSpeed").GetComponent<Text>();

        Text_Move1 = GameObject.Find("TextMove1").GetComponent<Text>();
        Text_Move2 = GameObject.Find("TextMove2").GetComponent<Text>();
        Text_Move3 = GameObject.Find("TextMove3").GetComponent<Text>();
        Text_Move4 = GameObject.Find("TextMove4").GetComponent<Text>();
    }

    private void ShowText()
    {
        Text_Name.text = name_eng;
        Text_Level.text = level.ToString();

        Text_Hp.text = Mathf.RoundToInt(hp).ToString();
        Text_Attack.text = Mathf.RoundToInt(attack).ToString();
        Text_Defense.text = Mathf.RoundToInt(defense).ToString();
        Text_SpAttack.text = Mathf.RoundToInt(spAttack).ToString();
        Text_SpDefense.text = Mathf.RoundToInt(spDefense).ToString();
        Text_Speed.text = Mathf.RoundToInt(speed).ToString();

        Text_Move1.text = pokemon.poke_moves[0];
        Text_Move2.text = pokemon.poke_moves[1];
        Text_Move3.text = pokemon.poke_moves[2];
        Text_Move4.text = pokemon.poke_moves[3];
    }

    private void ShowImage()
    {
        if (pokemon.id < 10)
        {
            ImagePkmn.sprite = Resources.Load<Sprite>($"images/00{pokemon.id}");
        }

        if (pokemon.id < 100 && pokemon.id >= 10)
        {
            ImagePkmn.sprite = Resources.Load<Sprite>($"images/0{pokemon.id}");
        }

        if (pokemon.id >= 100)
        {
            ImagePkmn.sprite = Resources.Load<Sprite>($"images/{pokemon.id}");
        }
    }
    #endregion
}