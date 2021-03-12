using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public List<GameObject> Team;

    private void Awake()
    {
        Team = new List<GameObject>();
        DontDestroyOnLoad(gameObject);
    }

    public void SetTeam(GameObject TeamMember)
    {
        Team.Add(TeamMember);
    }

    public List<GameObject> GetTeam()
    {
        return Team;
    }
}
