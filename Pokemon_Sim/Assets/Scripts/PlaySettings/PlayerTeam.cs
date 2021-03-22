using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject Parent;

    [HideInInspector]
    public GameObject ClickedPokemon;

    private PlaySettings teamInfo;

    private int size;

    private Button add, remove;

    private Color buttonColor;

    private Vector3 oldPos;
    private Vector3 TeamPos1, TeamPos2, TeamPos3, TeamPos4, TeamPos5, TeamPos6;

    private bool TeamIsFull = false;
    private IDictionary<GameObject, Vector3> pokemonPositions = new Dictionary<GameObject, Vector3>();

    private void Awake()
    {
        Team = new List<GameObject>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Scene currenScene = SceneManager.GetActiveScene();

        teamInfo = GameObject.Find("Settings_Handler").GetComponent<PlaySettings>();

        if (currenScene.name == "PokemonBox")
        {
            add = GameObject.Find("ButtonAddToTeam").GetComponent<Button>();
            remove = GameObject.Find("ButtonRemoveFromTeam").GetComponent<Button>();

            Parent = GameObject.Find("Container");

            buttonColor = add.image.color;

            size = teamInfo.TeamSize;

            InitTeamPosititons();
        }

        if (Team.Count > 0)
        {
            Team.Clear();
            pokemonPositions.Clear();
        }
    }

    #region Team Stuff
    public void RemovePokemonFromTeam()
    {
        if (Team.Count > 0)
        {
            if (Team.Count == size)
            {
                TeamIsFull = false;
            }

            GameObject pkmnToRemove = Team.Single(r => r.gameObject.name == ClickedPokemon.name);

            if (pkmnToRemove != null)
            {
                pkmnToRemove.tag = teamInfo.NotInTeam;
                RemoveTeamMemeberFromBox(pkmnToRemove);

                Team.Remove(pkmnToRemove);

                if (pokemonPositions.ContainsKey(pkmnToRemove))
                {
                    pokemonPositions.Remove(pkmnToRemove);
                }

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
                // never going to happen, but just in case
                if (ClickedPokemon.tag == teamInfo.InTeam)
                {
                    Debug.Log("This Pokemon is already in your team!");
                }
                else
                {
                    Team.Add(ClickedPokemon);
                    oldPos = PokemonOrigPos;

                    pokemonPositions.Add(ClickedPokemon, oldPos);

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
                TeamIsFull = false;
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
        /// team slot position is tied to the index in 'Team'       
        ///     - Team[0].transform.postiton = new Vector3(-80.0f, 180.0f);

        member.transform.SetParent(TeamBox.transform);

        foreach (var item in Team)
        {
            if (item == member)
            {
                if (Team.IndexOf(member) == 0)
                {
                    member.transform.localPosition = TeamPos1;
                }

                if (Team.IndexOf(member) == 1)
                {
                    member.transform.localPosition = TeamPos2;
                }

                if (Team.IndexOf(member) == 2)
                {
                    member.transform.localPosition = TeamPos3;
                }

                if (size == 6)
                {
                    if (Team.IndexOf(member) == 3)
                    {
                        member.transform.localPosition = TeamPos4;
                    }

                    if (Team.IndexOf(member) == 4)
                    {
                        member.transform.localPosition = TeamPos5;
                    }

                    if (Team.IndexOf(member) == 5)
                    {
                        member.transform.localPosition = TeamPos6;
                    }
                }
            }
        }

    }

    private void RemoveTeamMemeberFromBox(GameObject member)
    {
        member.transform.SetParent(Parent.transform, false);
        member.transform.localPosition = pokemonPositions[member];
    }

    private void InitTeamPosititons()
    {
        TeamPos1 = new Vector3(-80.0f, 180.0f, 0.0f);
        TeamPos2 = new Vector3(80.0f, 180.0f, 0.0f);
        TeamPos3 = new Vector3(-80.0f, 0.0f, 0.0f);

        if (size == 6)
        {
            TeamPos4 = new Vector3(80.0f, 0.0f, 0.0f);
            TeamPos5 = new Vector3(-80.0f, -180.0f, 0.0f);
            TeamPos6 = new Vector3(80.0f, -180.0f, 0.0f);
        }
    }
}
