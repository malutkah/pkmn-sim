using System.Collections.Generic;
using UnityEngine;

public class LoadPokemon : MonoBehaviour
{
    public GameObject reader;
    public GameObject PokemonPrefab;

    private JsonReader jReader;

    private SpriteRenderer spriteRenderer;
    private Sprite pkmn_sprite;

    private GameObject pkmnGO;
    private GameObject PokemonGO_parent;

    private int spriteCount = 152;

    private float step = 1.5f;
    private float startX = 1.5f;
    private float startY = 3.5f;

    private Vector3 EndOfField = new Vector3(1.5f, -4, 0.0f);
    private Vector3 EndOfRow = new Vector3(6.0f, 3.8f, 0.0f);

    List<string> pkmnSprite_names = new List<string>();

    private void Start()
    {
        jReader = reader.GetComponent<JsonReader>();

        spriteRenderer = PokemonPrefab.GetComponent<SpriteRenderer>();

        PokemonGO_parent = GameObject.Find("Pokemon");

        CreatePokemons();
    }

    /// <summary>
    /// DONE: INSTANTIATE GAMEOBJECT PREFAB WITH SPRITE IMAGES
    /// /// </summary>

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

        int amountPkmn = 7; // 6
        for (int i = 1; i < amountPkmn; i++)
        {
            pkmnGO = Instantiate(PokemonPrefab);

            pkmnGO.transform.localScale = new Vector3(3f, 3f, 1f);
            pkmnGO.transform.position = new Vector3(startX * (i - 1) - step, 3.8f, 0f);

            if (pkmnGO.transform.position.x == 6.0f) // or i == 6
            {
                // beginn a new row
                pkmnGO.transform.position = new Vector3(startX * (i - 1) - step, 3.8f - step, 0f);
            }
            else
            {
                pkmnGO.transform.position = new Vector3(startX * (i - 1) - step, 3.8f, 0f);
            }

            //pkmnGO.transform.SetParent(PokemonGO_parent.transform);


            pkmnGO.name = pkmnSprite_names[i - 1];

            SpriteRenderer tmp = pkmnGO.GetComponent<SpriteRenderer>();
            tmp.sprite = spriteList[i - 1];

        }
    }
}