using System;
using UnityEngine;

public class ShowPokemonInfo : MonoBehaviour
{
    string pkmn_name = "";
    int pkmn_id = 0;

    pokemon pokemon;

    public GameObject JsonReader;
    JsonReader reader;

    void Start()
    {
        reader = JsonReader.GetComponent<JsonReader>();
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
    }


}