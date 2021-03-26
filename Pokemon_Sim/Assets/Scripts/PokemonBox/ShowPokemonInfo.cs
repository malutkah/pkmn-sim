using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowPokemonInfo : MonoBehaviour
{
    private Image ImagePkmn;
    private PlayerTeam team;
    private pokemon pokemon;
    private GameObject JsonReader;
    private GameObject clickedPokemon;
    private JsonReader reader;
    private PlaySettings settings;
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

    private string pkmn_name = "";
    private string name_eng = "";
    private int pkmn_id = 0;

    #region Pokemon stats
    private float hp;
    private float attack;
    private float defense;
    private float spAttack;
    private float spDefense;
    private float speed;
    private int level = 5;
    private Color buttonColor;
    #endregion

    private string PokemonName_Debug = "";

    private void Start()
    {
        JsonReader = GameObject.Find("reader");

        settings = GameObject.Find("Settings_Handler").GetComponent<PlaySettings>();

        add = GameObject.Find("ButtonAddToTeam").GetComponent<Button>();
        remove = GameObject.Find("ButtonRemoveFromTeam").GetComponent<Button>();

        team = GameObject.Find("TeamHandler").GetComponent<PlayerTeam>();

        level = settings.level;

        reader = JsonReader.GetComponent<JsonReader>();

        ImagePkmn = GameObject.Find("ImagePkmn").GetComponent<Image>();

        InitText();

        remove.enabled = false;
        buttonColor = remove.image.color;
    }

    private void Load()
    {
        pokemon = new pokemon();
        pokemon = reader.GetPokemons().pokemon.Find(p => p.id == pkmn_id);
    }

    public void PokemonOnClick(GameObject sender)
    {
        team.ClickedPokemon = sender;
        team.PokemonOrigPos = sender.transform.localPosition;

        if (sender.tag == settings.InTeam)
        {
            add.enabled = false;
            add.image.color = new Color(255.0f, 255.0f, 255.0f, 0.1f);

            remove.enabled = true;
            remove.image.color = buttonColor;
        }
        else
        {
            add.enabled = true;
            add.image.color = buttonColor;

            remove.enabled = false;
            remove.image.color = new Color(255.0f, 255.0f, 255.0f, 0.1f);
        }

        pkmn_name = name;

        pkmn_id = Convert.ToInt32(pkmn_name.Substring(0, 3));

        Load();

        name_eng = pokemon.name.english;

        // calculate stats
        hp = Calculations.DoHP(pokemon.@base.hp, level);
        attack = Calculations.DoOtherStats(pokemon.@base.attack, level);
        defense = Calculations.DoOtherStats(pokemon.@base.defense, level);
        spAttack = Calculations.DoOtherStats(pokemon.@base.sp_attack, level);
        spDefense = Calculations.DoOtherStats(pokemon.@base.sp_defense, level);
        speed = Calculations.DoOtherStats(pokemon.@base.speed, level);

        ShowText();
        ShowImage();

        PokemonName_Debug = name_eng;
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