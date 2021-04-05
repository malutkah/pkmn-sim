using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogic : MonoBehaviour
{
    public void SentPokemonIntoBattle(GameObject playerPokemon, GameObject battleStation, PlaySettings settings)
    {
        playerPokemon.tag = settings.InBattle;
        playerPokemon.transform.SetParent(battleStation.transform);
        playerPokemon.transform.localPosition = new Vector3(0f, .1f, -9720f);
        playerPokemon.transform.localScale = new Vector3(1.3f, 1.3f, 108);
    }

    #region switchting pokemon
    private void SwitchPlayerPokemon(GameObject pokemonInTeam, GameObject battleStation, PlaySettings settings, IDictionary<GameObject, Transform> pokemonParent)
    {
        GameObject pokemonInBattle = GameObject.FindWithTag(settings.InBattle);

        pokemonInBattle.tag = settings.InBattleTeam;
        pokemonInBattle.transform.SetParent(pokemonParent[pokemonInBattle]);

        pokemonInTeam.tag = settings.InBattle;

        SentPokemonIntoBattle(pokemonInTeam, battleStation, settings);
    }
    #endregion
}
