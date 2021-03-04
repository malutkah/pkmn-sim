using Newtonsoft.Json;
using UnityEngine;


public class JsonReader : MonoBehaviour
{
    //public TextAsset jsonfile;
    //public TextAsset jsonfile_moves;

    public Moves moves = new Moves();



    private void Start()
    {
        TextAsset jsonfile_moves = Resources.Load("moves") as TextAsset;

        if (jsonfile_moves != null)
        {
            moves = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);
            //moves = JsonConvert.DeserializeObject<Moves>(jsonExample);

            foreach (moves item in moves.moves)
            {
                Debug.Log($"found attack: {item.ename} of type {item.type}");
            }

            //Debug.Log(jsonfile_moves.text);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            // ShowMoves();
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            // ShowPokemons();
        }
    }

    /*
    void ShowMoves()
    {
        //Moves movesInJson = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);
        //MoveData jMoves = JsonConvert.DeserializeObject<MoveData>(jsonfile_moves.text);

        string jsonExample = @"{
                                ""moves"": [
                                    {
                                        ""accuracy"": 100, 
                                        ""category"": ""physical"", 
                                        ""ename"": ""Pound"", 
                                        ""id"": 1, 
                                        ""power"": 40, 
                                        ""pp"": 35, 
                                        ""type"": ""Normal""
                                    },
                                    {
                                        ""accuracy"": 100, 
                                        ""category"": ""physical"", 
                                        ""ename"": ""Karate Chop"", 
                                        ""id"": 2,             
                                        ""power"": 50, 
                                        ""pp"": 25, 
                                        ""type"": ""Fighting""
                                    }
                                ]
                            }";

        Moves jMoves = JsonConvert.DeserializeObject<Moves>(jsonfile_moves.text);
        //Moves jMoves = JsonConvert.DeserializeObject<Moves>(jsonExample);

        // Debug.Log(jMoves.moves[0].Ename);

        foreach (var m in jMoves.moves)
        {
            Debug.Log($"Found attack: {m.Ename} of type {m.Type}");
        }
    }

    void ShowPokemons()
    {
        var pkmnInJson = JsonConvert.DeserializeObject<Pokemons>(jsonfile.text);

        foreach (var item in pkmnInJson.pkmns)
        {
            Debug.Log($"Found pokemon: {item.Name.english} with poke dex ID: {item.id}");
        }

        //foreach (var pkmn in pkmnInJson.)
        //{
        //}
    }
    */

}

