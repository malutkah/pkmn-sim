using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTestBattle : MonoBehaviour
{
    public GameObject PokemonPrefab;
    public GameObject BattleStationPlayer;
    public GameObject BattleStationEnemy;


    void Start()
    {
        CreatePlayerPokmeon();
        CreateEnemyPokmeon();
    }

    void Update()
    {
        
    }

    private void CreatePlayerPokmeon()
    {
        Sprite newSprite = Resources.Load<Sprite>("sprites/009MS");
        string pkmnSpriteName = newSprite.name;

        var pkmnGo = Instantiate(PokemonPrefab, new Vector3(0f, 0f), Quaternion.identity);
        pkmnGo.transform.SetParent(BattleStationPlayer.transform);
        pkmnGo.transform.localScale = new Vector3(300.0f, 300.0f, 1.0f);

        pkmnGo.name = pkmnSpriteName;
    }

    private void CreateEnemyPokmeon()
    {

    }
}
