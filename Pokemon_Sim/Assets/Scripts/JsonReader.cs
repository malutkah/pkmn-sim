using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonfile;
    public TextAsset jsonfile_moves;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.ClearDeveloperConsole();
            ShowMoves();
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Debug.ClearDeveloperConsole();
            ShowPokemons();
        }


    }

    void ShowMoves()
    {
        Moves movesInJson = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);

        foreach (MoveData m in movesInJson.moves)
        {
            Debug.Log($"Found attack: {m.Ename} of type {m.Type}");

            if (m.Id == 5)
            {
                break;
            }
        }
    }

    void ShowPokemons()
    {
        Pokemons pkmnInJson = JsonConvert.DeserializeObject<Pokemons>(jsonfile.text);


        foreach (PokemonData pkmn in pkmnInJson.pkmns)
        {
            Debug.Log($"Found pokemon: {pkmn.name} with poke dex ID: {pkmn.id}");
        }
    }
}
