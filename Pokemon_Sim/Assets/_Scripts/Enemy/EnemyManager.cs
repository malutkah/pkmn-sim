using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject PkmnBox_1;
    public GameObject Team;
    
    public List<GameObject> enemyTeam;

    private PokemonInfoHolder infoHolder;
    private PokemonOnClickHandler clickHandler;

    #region Unity

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    #region Enemy team generation

    public void GenerateTeam()
    {
        GameObject pokemonFromBox;
        for (int i = 0; i < PlaySettings.instance.TeamSize; i++)
        {
            var rnd = Random.Range(0, PkmnBox_1.transform.childCount);

            for (int j = 0; j < PkmnBox_1.transform.childCount; j++)
            {
                if (j == rnd)
                {
                    pokemonFromBox = PkmnBox_1.transform.GetChild(j).gameObject;
                    enemyTeam.Add(pokemonFromBox);
                    SetPokemonInfo(pokemonFromBox);
                    pokemonFromBox.transform.SetParent(Team.transform);
                    break;
                }
            }
        }
    }

    private void SetPokemonInfo(GameObject pkmn)
    {
        clickHandler = pkmn.GetComponent<PokemonOnClickHandler>();
        clickHandler.PokemonOnClick(pkmn);
    }

    #endregion
}
