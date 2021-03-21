using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField]
    private GameObject TeamBox;

    private GameObject Settings;

    public List<GameObject> Team;

    [HideInInspector]
    public Vector3 PokemonOrigPos;

    [HideInInspector]
    public GameObject ClickedPokemon;

    private PlaySettings teamInfo;

    private int size;

    private Button add, remove;

    private Color buttonColor;


    private void Awake()
    {
        Team = new List<GameObject>();
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        teamInfo = GameObject.Find("Settings_Handler").GetComponent<PlaySettings>();

        add = GameObject.Find("ButtonAddToTeam").GetComponent<Button>();
        remove = GameObject.Find("ButtonRemoveFromTeam").GetComponent<Button>();

        buttonColor = add.image.color;

        size = teamInfo.TeamSize;

        if (Team.Count > 0)
        {
            Team.Clear();
        }
    }

    #region Team Stuff
    public void RemovePokemonFromTeam()
    {
        if (Team.Count > 0)
        {
            GameObject pkmnToRemove = Team.Single(r => r.gameObject.name == ClickedPokemon.name);

            if (pkmnToRemove != null)
            {
                pkmnToRemove.tag = teamInfo.NotInTeam;
                Team.Remove(pkmnToRemove);

                #region buttons
                remove.enabled = false;
                remove.image.color = new Color(255.0f, 255.0f, 255.0f, 0.1f);

                add.enabled = true;
                add.image.color = buttonColor;
                #endregion
            }
        }
    }

    public void AddPokemonToTeam()
    {
        if (ClickedPokemon != null)
        {
            if (Team.Count < size)
            {
                if (ClickedPokemon.tag == teamInfo.InTeam)
                {
                    Debug.Log("This Pokemon is already in your team!");
                }
                else
                {
                    Team.Add(ClickedPokemon);
                    ClickedPokemon.tag = teamInfo.InTeam;

                    Debug.Log($"there are {size - Team.Count} postions left");

                    PlaceTeamMembersInBox(ClickedPokemon);

                    #region buttons
                    add.enabled = false;
                    add.image.color = new Color(255.0f, 255.0f, 255.0f, 0.1f);

                    remove.enabled = true;
                    remove.image.color = buttonColor;
                    #endregion
                }
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
    #endregion


    private void PlaceTeamMembersInBox(GameObject member)
    {
        // check if pokemon is already in team

        /// if max pokemon is not reached
        ///    - get free team positions
        ///    - move pokemon to available positions 
        /// 
        /// Set new parent
        /// 
        /// team slot position is tied to the index in 'Team'       
        ///     - Team[0].transform.postiton = new Vector3(-80.0f, 180.0f);

        member.transform.SetParent(TeamBox.transform);

        member.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        //member.transform.position = new Vector3(-80.0f, 180.0f);
    }

    private void RemoveTeamMemeberFromBox()
    {
        Team[0].transform.position = PokemonOrigPos;
    }
}
