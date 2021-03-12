using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPokemonInfo : MonoBehaviour
{
    private Image ImagePkmn;

    #region Pokemon Stats Text
    private Text Text_Name;
    private Text Text_Level;
    private Text Text_Attack;
    private Text Text_Defense;
    private Text Text_SpAttack;
    private Text Text_SpDefense;
    private Text Text_Speed;
    #endregion

    string pkmn_name = "";
    string name_eng = "";
    int pkmn_id = 0;

    #region Pokemon stats
    float attack;
    float defense;
    float spAttack;
    float spDefense;
    float speed;
    #endregion

    pokemon pokemon;

    public GameObject JsonReader;
    JsonReader reader;

    void Start()
    {
        JsonReader = GameObject.Find("reader");

        reader = JsonReader.GetComponent<JsonReader>();

        ImagePkmn = GameObject.Find("ImagePkmn").GetComponent<Image>();

        InitText();
    }

    void Load()
    {
        pokemon = new pokemon();
        pokemon = reader.GetPokemons().pokemon.Find(p => p.id == pkmn_id);

        if (pokemon.type.Count == 2)
        {
            Debug.Log($"{pokemon.name.english} ({pokemon.type[0]}/{pokemon.type[1]})");
        }
        else
        {
            Debug.Log($"{pokemon.name.english} ({pokemon.type[0]})");
        }
    }

    public void PokemonOnClick()
    {
        pkmn_name = name;

        pkmn_id = Convert.ToInt32(pkmn_name.Substring(0, 3));

        Load();

        name_eng = pokemon.name.english;
        attack = pokemon.@base.attack;
        defense = pokemon.@base.defense;
        spAttack = pokemon.@base.sp_attack;
        spDefense = pokemon.@base.sp_defense;
        speed = pokemon.@base.speed;

        ShowText();
        ShowImage();
    }

    void InitText()
    {
        Text_Name = GameObject.Find("TextPkmnName").GetComponent<Text>();
        Text_Level = GameObject.Find("TextPkmnLevel").GetComponent<Text>();
        Text_Attack = GameObject.Find("TextPkmnAtk").GetComponent<Text>();
        Text_Defense = GameObject.Find("TextPkmnDef").GetComponent<Text>();
        Text_SpAttack = GameObject.Find("TextPkmnSpAtk").GetComponent<Text>();
        Text_SpDefense = GameObject.Find("TextPkmnSpDef").GetComponent<Text>();
        Text_Speed = GameObject.Find("TextPkmnSpeed").GetComponent<Text>();

    }

    void ShowText()
    {
        Text_Name.text = name_eng;
        Text_Attack.text = attack.ToString();
        Text_Defense.text = defense.ToString();
        Text_SpAttack.text = spAttack.ToString();
        Text_SpDefense.text = spDefense.ToString();
        Text_Speed.text = speed.ToString();
    }

    void ShowImage()
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
}