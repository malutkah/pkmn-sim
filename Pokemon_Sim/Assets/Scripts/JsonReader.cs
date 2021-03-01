using Newtonsoft.Json;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonfile;
    public TextAsset jsonfile_moves;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ShowMoves();
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ShowPokemons();
        }
    }

    void ShowMoves()
    {
        Moves movesInJson = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);

        foreach (var m in movesInJson.moves)
        {
            Debug.Log($"Found attack: {m.move_list[0]} of type {m.move_list[1]}");
        }
    }

    void ShowPokemons()
    {
        Pokemons pkmnInJson = new Pokemons();
        pkmnInJson = JsonConvert.DeserializeObject<Pokemons>(jsonfile.text);

        foreach (var pkmn in pkmnInJson.pkmns)
        {
            Debug.Log($"Found pokemon: {pkmn.name.english} with poke dex ID: {pkmn.id}");
        }
    }
}
