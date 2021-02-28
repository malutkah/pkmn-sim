using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonfile;
    public TextAsset jsonfile_moves;

    void Start()
    {
        Pokemons pkmnInJson = JsonUtility.FromJson<Pokemons>(jsonfile.text);
        Moves movesInJson = JsonUtility.FromJson<Moves>(jsonfile_moves.text);

        foreach (MoveData m in movesInJson.moves)
        {
            Debug.Log($"Found attack: {m.ename} of type {m.type}");
        }
        
        //foreach (PokemonData pkmn in pkmnInJson.pkmns)
        //{
        //    Debug.Log($"Found pokemon: {pkmn.name} with poke dex ID: {pkmn.id}");
        //}
    }
}
