using Newtonsoft.Json;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    private Moves moves = new Moves();
    private Pokemons pokemons = new Pokemons();

    private void Awake()
    {
        moves = InitializeMoves();
        pokemons = InitializePokemons();

        DontDestroyOnLoad(gameObject);
    }

    public Moves GetMoves()
    {
        return moves;
    }

    public Pokemons GetPokemons()
    {
        return pokemons;
    }

    Moves InitializeMoves()
    {
        TextAsset jsonfile_moves = Resources.Load("moves") as TextAsset;

        moves = new Moves();

        try
        {
            moves = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }

        return moves;
    }

    Pokemons InitializePokemons()
    {
        TextAsset jsonfile_pkmn = Resources.Load("pokedex_Gen1") as TextAsset;

        pokemons = new Pokemons();

        try
        {
            pokemons = JsonConvert.DeserializeObject<Pokemons>(jsonfile_pkmn.text);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }

        return pokemons;
    }
}