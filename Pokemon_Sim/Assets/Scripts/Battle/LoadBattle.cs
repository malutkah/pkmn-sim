using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBattle : MonoBehaviour
{
    [SerializeField]
    private Image PlayerTeamSlot1;
    [SerializeField]
    private Image PlayerTeamSlot2;
    [SerializeField]
    private Image PlayerTeamSlot3;
    [SerializeField]
    private Image PlayerTeamSlot4;
    [SerializeField]
    private Image PlayerTeamSlot5;
    [SerializeField]
    private Image PlayerTeamSlot6;

    private PlayerTeam playerTeam;
    private List<GameObject> teamList;

    private int teamSize = 0;

    private void Awake()
    {
        playerTeam = GameObject.Find("TeamHandler").GetComponent<PlayerTeam>();

        if (playerTeam.Team != null)
        {
            teamList = playerTeam.GetTeam();
            teamSize = teamList.Count;
        }

    }

    private void Start()
    {
        LoadPlayerPokemonInImageSlots();
    }

    private void LoadPlayerPokemonInImageSlots()
    {
        for (int i = 0; i < teamSize; i++)
        {
            if (i == 0)
            {
                teamList[i].transform.SetParent(PlayerTeamSlot1.transform);
                teamList[i].transform.localPosition = new Vector3(0f, 0f, 0f);
                teamList[i].transform.localScale = new Vector3(300f, 300f, 1f);
            }

            if (i == 1)
            {
                teamList[i].transform.SetParent(PlayerTeamSlot2.transform);
                teamList[i].transform.localPosition = new Vector3(0f, 0f, 0f);
                teamList[i].transform.localScale = new Vector3(300f, 300f, 1f);
            }

            if (i == 2)
            {
                teamList[i].transform.SetParent(PlayerTeamSlot3.transform);
                teamList[i].transform.localPosition = new Vector3(0f, 0f, 0f);
                teamList[i].transform.localScale = new Vector3(300f, 300f, 1f);
            }

            if (teamSize == 6)
            {
                if (i == 3)
                {
                    teamList[i].transform.SetParent(PlayerTeamSlot4.transform);
                    teamList[i].transform.localPosition = new Vector3(0f, 0f, 0f);
                    teamList[i].transform.localScale = new Vector3(300f, 300f, 1f);
                }

                if (i == 4)
                {
                    teamList[i].transform.SetParent(PlayerTeamSlot5.transform);
                    teamList[i].transform.localPosition = new Vector3(0f, 0f, 0f);
                    teamList[i].transform.localScale = new Vector3(300f, 300f, 1f);
                }

                if (i == 5)
                {
                    teamList[i].transform.SetParent(PlayerTeamSlot6.transform);
                    teamList[i].transform.localPosition = new Vector3(0f, 0f, 0f);
                    teamList[i].transform.localScale = new Vector3(300f, 300f, 1f);
                }
            }
        }
    }

}
