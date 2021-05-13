using System.Collections.Generic;
using UnityEngine;

public class LoadPokemon : MonoBehaviour
{
    [SerializeField]
    private GameObject PokemonPrefab;

    private SpriteRenderer spriteRenderer;
    private Sprite pkmn_sprite;

    private GameObject pkmnGO;
    private GameObject PkmnBox_1;
    private GameObject PkmnBox_2;

    private int spriteCount = 152;

    public float x_Start, y_Start;
    public float x_Space, y_Space;
    public int PokemonInARow;
    public int AmountOfPokemon;

    private Vector3 EndOfField = new Vector3(1.5f, -4, 0.0f);
    private Vector3 EndOfRow = new Vector3(6.0f, 3.8f, 0.0f);

    List<string> pkmnSprite_names = new List<string>();

    /// <summary>
    /// DONE: GET LEVEL
    /// DONE: GET TEAM SIZE
    /// DONE: SHOW POKEMON TEAM SLOTS
    /// DONE: ADD POKEMON TO TEAM
    /// DONE: CALCULATE POKEMON STATS
    /// </summary>

    private void Start()
    {
        spriteRenderer = PokemonPrefab.GetComponent<SpriteRenderer>();

        PkmnBox_1 = GameObject.Find("Container");

        CreatePokemons();
    }

    void CreatePokemons()
    {
        var spriteCounter = Resources.LoadAll("sprites", typeof(Sprite));
        // spriteCount = 152; //spriteCounter.Length;

        List<Sprite> spriteList = new List<Sprite>();

        Sprite newSprite;

        for (int i = 1; i < spriteCount; i++)
        {
            string number = i.ToString();
            if (i < 10)
            {
                newSprite = Resources.Load<Sprite>($"sprites/00{number}MS");
                spriteList.Add(Resources.Load<Sprite>($"sprites/00{number}MS"));
                pkmnSprite_names.Add(newSprite.name);
            }

            if (i < 100 && i >= 10)
            {
                newSprite = Resources.Load<Sprite>($"sprites/0{number}MS");
                spriteList.Add(Resources.Load<Sprite>($"sprites/0{number}MS"));
                pkmnSprite_names.Add(newSprite.name);
            }

            if (i >= 100)
            {
                newSprite = Resources.Load<Sprite>($"sprites/{number}MS");
                spriteList.Add(Resources.Load<Sprite>($"sprites/{number}MS"));
                pkmnSprite_names.Add(newSprite.name);
            }
        }

        PlacePokemonInBox(spriteList);

    }

    private void PlacePokemonInBox(List<Sprite> spriteList)
    {
        int amountPokemon = (PokemonInARow + AmountOfPokemon) - 5;

        for (int i = 0; i < amountPokemon; i++)
        {
            Vector3 position = new Vector3(x_Start + (x_Space * (i % PokemonInARow)), y_Start + (-y_Space * (i / PokemonInARow)));

            pkmnGO = Instantiate(PokemonPrefab, position, Quaternion.identity);

            pkmnGO.transform.localScale = new Vector3(300.0f, 300.0f, 1.0f);

            pkmnGO.transform.SetParent(PkmnBox_1.transform, false);

            pkmnGO.name = pkmnSprite_names[i];

            SpriteRenderer tmp = pkmnGO.GetComponent<SpriteRenderer>();
            tmp.sprite = spriteList[i];
        }
    }
}