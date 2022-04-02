using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager is responsible for:
///     - executing moves and calculate the damage         
///         - checking the pokemon and attck type
///     - managing the round-based system
///     - controlling the enemy AI
///     - checking if game is over (who won?)
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject PokemonInBattle_Enemy;
    public GameObject PokemonInBattle_Player;

    private bool gameOver;

    #region Unity

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
    }

    #endregion
}
