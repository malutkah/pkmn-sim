using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public List<GameObject> Team;

    private void Awake()
    {
        Team = new List<GameObject>();
        DontDestroyOnLoad(gameObject);
    }

    public void RemovePokemonFromTeam(GameObject TeamMember)
    {
        GameObject pkmnToRemove = Team.Single(r => r.gameObject.name == TeamMember.name);

        if (pkmnToRemove != null)
        {
            Team.Remove(pkmnToRemove);
        }
    }

    public void AddPokemonToTeam(GameObject TeamMember)
    {
        Team.Add(TeamMember);
    }

    public List<GameObject> GetTeam()
    {
        return Team;
    }
}
