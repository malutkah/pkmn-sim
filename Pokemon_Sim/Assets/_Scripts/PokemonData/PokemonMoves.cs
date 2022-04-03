using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PokemonMoves : MonoBehaviour
{
    public string[] MoveNames = new string[4];

    private pokemon pokemon;
    private moves _moves;
    private JsonReader reader;

    private int pkmn_id = 0;
    private string name_eng;

    private int move_name;

    private string pkmn_type1 = "";
    private string pkmn_type2 = "";

    private void Start()
    {
        reader = GameObject.Find("reader").GetComponent<JsonReader>();

        pokemon = new pokemon();
        _moves = new moves();

        LoadPokemon();
        SaveMoveNames();
    }

    private void LoadPokemon()
    {
        pkmn_id = Convert.ToInt32(name.Substring(0, 3));

        pokemon = reader.GetPokemons().pokemon.Find(p => p.id == pkmn_id);

        name_eng = pokemon.name.english;

        pkmn_type1 = pokemon.type[0];

        if (pokemon.type.Count == 2)
        {
            pkmn_type2 = pokemon.type[1];
        }
    }
    
    private void LoadMovesByName(string moveName)
    {
        _moves = reader.GetMoves().moves.Find(m => m.ename == moveName);
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

    public moves GetMoveByName(string moveName)
    {
        LoadMovesByName(moveName);
        return _moves;
    }

    public int GetMovePP(string moveName)
    {
        LoadMovesByName(moveName);
        return _moves.pp;
    }

    public int GetMovePower(string moveName)
    {
        LoadMovesByName(moveName);
        return _moves.power;
    }

    public string GetMoveType(string moveName)
    {
        LoadMovesByName(moveName);
        return _moves.type;
    }

    public int GetMoveAccuracy(string moveName)
    {
        LoadMovesByName(moveName);
        return _moves.accuracy;
    }

    public string GetMoveCategory(string moveName)
    {
        LoadMovesByName(moveName);
        return _moves.category;
    }
    #endregion

    
}
