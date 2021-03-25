using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonMoves : MonoBehaviour
{
    public string[] pokemon_moves = new string[4];

    private pokemon pokemon;
    private moves moves;
    private JsonReader reader;

    #region attack lists
    private List<string> attack_normal = new List<string>();
    private List<string> attack_fire = new List<string>();
    private List<string> attack_water = new List<string>();
    private List<string> attack_dark = new List<string>();
    private List<string> attack_fairy = new List<string>();
    private List<string> attack_poison = new List<string>();
    private List<string> attack_bug = new List<string>();
    private List<string> attack_ground = new List<string>();
    private List<string> attack_rock = new List<string>();
    private List<string> attack_electric = new List<string>();
    private List<string> attack_steel = new List<string>();
    private List<string> attack_grass = new List<string>();
    private List<string> attack_ghost = new List<string>();
    private List<string> attack_ice = new List<string>();
    private List<string> attack_fighting = new List<string>();
    private List<string> attack_dragon = new List<string>();
    private List<string> attack_psychic = new List<string>();
    #endregion

    #region attack types
    private const string _normal = "normal";
    private const string _fire = "fire";
    private const string _water = "water";
    private const string _dark = "dark";
    private const string _fairy = "fairy";
    private const string _poison = "poision";
    private const string _bug = "bug";
    private const string _ground = "ground";
    private const string _rock = "rock";
    private const string _elec = "electric";
    private const string _steel = "steel";
    private const string _grass = "grass";
    private const string _ghost = "ghost";
    private const string _ice = "ice";
    private const string _fight = "fighting";
    private const string _dragon = "dragon";
    private const string _psy = "psychic";
    #endregion

    private int pkmn_id = 0;
    private string name_eng;

    private int move_id = 0;
    private int move_name;

    private void Start()
    {
        reader = GameObject.Find("reader").GetComponent<JsonReader>();

        pokemon = new pokemon();
        moves = new moves();
    }

    private void LoadPokemon()
    {
        pkmn_id = Convert.ToInt32(name.Substring(0, 3));

        pokemon = reader.GetPokemons().pokemon.Find(p => p.id == pkmn_id);

        name_eng = pokemon.name.english;
    }

    private void LoadMovesByType(string type)
    {
        moves = reader.GetMoves().moves.Find(m => m.type == type);
    }

    // load attacks to pokemon and add to array
}
