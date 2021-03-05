using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPokemon : MonoBehaviour
{
    public Moves moves = new Moves();
    public GameObject reader;
    JsonReader jReader;

    void Start()
    {
        jReader = reader.GetComponent<JsonReader>();

        jReader.ShowMoves(moves);
    }

    /// <summary>
    /// UNDONE: INSTANTIATE GAMEOBJECT WITH SPRITE IMAGES
    /// 
    /// </summary>

}
