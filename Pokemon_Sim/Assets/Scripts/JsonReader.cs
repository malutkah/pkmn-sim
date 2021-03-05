using Newtonsoft.Json;
using UnityEngine;


public class JsonReader : MonoBehaviour
{
    //public TextAsset jsonfile;
    //public TextAsset jsonfile_moves;

    public Moves moves = new Moves();
    public Pokemons pokemons = new Pokemons();    

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ShowMoves(moves);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ShowPokemons();
        }
    }


    public void ShowMoves(Moves moves)
    {
        TextAsset jsonfile_moves = Resources.Load("moves") as TextAsset;
        moves = new Moves();

        if (jsonfile_moves != null)
        {
            moves = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);
            //moves = JsonConvert.DeserializeObject<Moves>(jsonExample);

            foreach (moves item in moves.moves)
            {
                Debug.Log($"found attack: {item.ename} of type {item.type}");
            }
        }
    }

    void ShowPokemons()
    {
        TextAsset jsonfile_pkmn = Resources.Load("pokedex_Gen1") as TextAsset;

        if (jsonfile_pkmn != null)
        {
            pokemons = JsonConvert.DeserializeObject<Pokemons>(jsonfile_pkmn.text);

            foreach (pokemon item in pokemons.pokemon)
            {
                Debug.Log($"found pokemon: {item.name.english} with base attack of {item.@base.attack}");
            }
        }

    }
}

