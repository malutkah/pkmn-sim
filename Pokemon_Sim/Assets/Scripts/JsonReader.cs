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
        //Moves movesInJson = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);
        Moves jMoves = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);

        foreach (var m in jMoves.moves)
        {
            for (int i = 0; i < m.move_list.Count; i++)
            {
                Debug.Log($"Found attack: {m.move_list[0]} of type {m.move_list[1]}");
            }
        }
    }

    void ShowPokemons()
    {
        var pkmnInJson = JsonConvert.DeserializeObject<PokemonData>(jsonfile.text);

        Debug.Log($"Found pokemon: {pkmnInJson.name.english} with poke dex ID: {pkmnInJson.id}");

        //foreach (var pkmn in pkmnInJson.)
        //{
        //}
    }
}
