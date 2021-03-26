using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonMoves : MonoBehaviour
{
    [SerializeField]
    private string[] moveNames = new string[2];

    private moves[] PokeMoves = new moves[2];

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

        if (pokemon.name.english == "Bulbasaur")
        {
            BulbasaurAttacks();
            for (int i = 0; i < PokeMoves.Length; i++)
            {
                Debug.Log($"{PokeMoves[i].ename}");
            }
        }

        //LoadMovesByName("Tackle");
        //Debug.Log($"the attack name is {moves.ename} with a power of {moves.power}!");
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

    private void LoadMovesByType(string type)
    {
        moves = reader.GetMoves().moves.Find(m => m.type == type);
    }

    private void LoadMovesByName(string m_name)
    {
        moves = reader.GetMoves().moves.Find(m => m.ename == m_name);
    }

    // load attacks to pokemon and add to array
    private void BulbasaurAttacks()
    {
        LoadMovesByName("Tackle");
        PokeMoves[0] = moves;
        moveNames[0] = moves.ename;

        LoadMovesByName("Vine Whip");        
        PokeMoves[1] = moves;
        moveNames[1] = moves.ename;
    }
}
