using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField]
    private GameObject TeamBox;

    private GameObject Settings;

    public List<GameObject> Team;

    [HideInInspector]
    public GameObject ClickedPokemon;

    private PlaySettings teamInfo;

    private int size;

    private void Awake()
    {
        Team = new List<GameObject>();
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        teamInfo = GameObject.Find("Settings_Handler").GetComponent<PlaySettings>();

        size = teamInfo.TeamSize;

        if (Team.Count > 0)
        {
            Team.Clear();
        }
    }

    public void RemovePokemonFromTeam()
    {
        if (Team.Count > 0)
        {
            GameObject pkmnToRemove = Team.Single(r => r.gameObject.name == ClickedPokemon.name);

            if (pkmnToRemove != null)
            {
                Team.Remove(pkmnToRemove);
            }
        }
    }

    public void AddPokemonToTeam()
    {
        if (ClickedPokemon != null)
        {
            if (Team.Count < size)
            {
                Team.Add(ClickedPokemon);
                //Debug.Log($"Added: {PotentialTeamMember.name} to the team.");
                Debug.Log($"there are {size - Team.Count} postions left");

                // check if pokemon is already in team

                /// if max pokemon is not reached
                ///    - get free team positions
                ///    - move pokemon to available positions 
            }
            else
            {
                Debug.Log("Your Team is full!");
            }
        }
    }

    public List<GameObject> GetTeam()
    {
        return Team;
    }

    private void PlaceTeamMembersInBox(GameObject member)
    {

    }
}
