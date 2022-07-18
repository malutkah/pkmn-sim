using UnityEngine;

public enum Tier
{
    EASY,
    MEDIUM,
    HIGH,
    BEST
}

public class EnemyAI : MonoBehaviour
{
    // choose if
    // attack
    // or
    // switch pokemon
    // tiers:
    // easy: chooses random attack
    // medium: chooses attack which does the most damage
    // high: same as medium and knows when to switch pokemon
    // best: medium and high + also chooses stat-moves
    public Tier tier;

    private GameObject enemyPokemonInBattle;

    private PokemonInfoHolder enemyInfoHolder;

    private PokemonMoves enemyMoves;


    #region Setup

    public void Execute()
    {
        switch (tier)
        {
            case Tier.EASY:
                ChooseAttack_Easy();
                break;

            case Tier.MEDIUM:
                Debug.Log("Tier Medium not implemented");
                break;

            case Tier.HIGH:
                Debug.Log("Tier High not implemented");
                break;

            case Tier.BEST:
                Debug.Log("Tier Best not implemented");
                break;
        }
    }

    public void SetEnemyInBattle(GameObject pkmn)
    {
        enemyPokemonInBattle = pkmn;
        enemyInfoHolder = enemyPokemonInBattle.GetComponent<PokemonInfoHolder>();
        enemyMoves = enemyPokemonInBattle.GetComponent<PokemonMoves>();
    }

    #endregion

    #region TIER: Easy

    private void ChooseAttack_Easy()
    {
        var randomMove = UnityEngine.Random.Range(0, 3);
        var moveName = enemyMoves.MoveNames[randomMove];
        GameManager.instance.ExecuteEnemyAttack(moveName);
    }

    #endregion
}
