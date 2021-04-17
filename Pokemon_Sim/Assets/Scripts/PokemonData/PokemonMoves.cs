using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PokemonMoves : MonoBehaviour
{
    //[HideInInspector]
    public string[] MoveNames = new string[4];

    private pokemon pokemon;
    private moves moves;
    private JsonReader reader;

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

    private int move_name;

    private string pkmn_type1 = "";
    private string pkmn_type2 = "";

    private void Start()
    {
        reader = GameObject.Find("reader").GetComponent<JsonReader>();

        pokemon = new pokemon();
        moves = new moves();

        LoadPokemon();
        SaveMoveNames();
    }

    private void LoadPokemon()
    {
        pkmn_id = Convert.ToInt32(name.Substring(0, 3));

        pokemon = reader.GetPokemons().pokemon.Find(p => p.id == pkmn_id);

        name_eng = pokemon.name.english;

        pkmn_type1 = pokemon.type[0];

        if (pokemon.type.Count >= 2)
        {
            pkmn_type2 = pokemon.type[1];
        }
    }
    
    private void LoadMovesByName(string moveName)
    {
        moves = reader.GetMoves().moves.Find(m => m.ename == moveName);
    }

    private void SaveMoveNames()
    {
        for (int i = 0; i < 4; i++)
        {
            LoadMovesByName(pokemon.poke_moves[i]);
            MoveNames[i] = pokemon.poke_moves[i];
        }
    }
        
    #region get move infos
    public int GetMovePP(string moveName)
    {
        LoadMovesByName(moveName);
        return moves.pp;
    }

    public int GetMovePower(string moveName)
    {
        LoadMovesByName(moveName);
        return moves.power;
    }

    public string GetMoveType(string moveName)
    {
        LoadMovesByName(moveName);
        return moves.type;
    }

    public int GetMoveAccuracy(string moveName)
    {
        LoadMovesByName(moveName);
        return moves.accuracy;
    }

    public string GetMoveCategory(string moveName)
    {
        LoadMovesByName(moveName);
        return moves.category;
    }
    #endregion

    
}
