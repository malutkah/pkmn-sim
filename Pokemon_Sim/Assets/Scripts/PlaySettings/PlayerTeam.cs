using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Team;

    [SerializeField]
    private Button FightButton;

    [SerializeField]
    private GameObject TeamBox;

    [HideInInspector]
    public Vector3 PokemonOrigPos;
        
    private GameObject Parent;

    [HideInInspector]
    public GameObject ClickedPokemon;

    private PlaySettings teamInfo;

    private GameObject settings;

    private int size;

    private Button add, remove;

    private Color originalButtonColor;

    private Vector3 oldPos;
    private Vector3 teamPos1, teamPos2, teamPos3, teamPos4, teamPos5, teamPos6;

    private IDictionary<GameObject, Vector3> pokemonPositions = new Dictionary<GameObject, Vector3>();

    #region Unity
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(TeamBox);
        Team = new List<GameObject>();
    }

    private void Update()
    {
        if (Team.Count == size)
        {
            FightButton.interactable = true;
        }
        else
        {
            FightButton.interactable = false;
        }        
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

            originalButtonColor = add.image.color;

            size = teamInfo.TeamSize;

            InitTeamPosititons();
        }

        if (Team.Count > 0)
        {
            Team.Clear();
            pokemonPositions.Clear();
        }
    }   
    #endregion

    #region Team Stuff
    public void RemovePokemonFromTeam()
    {
        if (Team.Count > 0)
        {
            GameObject pkmnToRemove = Team.Single(r => r.gameObject.name == ClickedPokemon.name);

            if (pkmnToRemove != null)
            {
                pkmnToRemove.tag = teamInfo.NotInTeam;

                MovePokemonPositionUp(pkmnToRemove);

                RemoveTeamMemeberFromBox(pkmnToRemove);
                
                Team.Remove(pkmnToRemove);

                if (pokemonPositions.ContainsKey(pkmnToRemove))
                {
                    pokemonPositions.Remove(pkmnToRemove);
                }
                
                EnableButtonsAddPokemonToTeam(true);
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

                    PlaceTeamMembersInBox(ClickedPokemon);
                    
                    EnableButtonsAddPokemonToTeam(false);
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

    #region Pokemon placement
    private void PlaceTeamMembersInBox(GameObject member)
    {
        member.transform.SetParent(gameObject.transform);

        foreach (var item in Team)
        {
            if (item == member)
            {
                if (Team.IndexOf(member) == 0)
                {
                    member.transform.localPosition = teamPos1;
                }

                if (Team.IndexOf(member) == 1)
                {
                    member.transform.localPosition = teamPos2;
                }

                if (Team.IndexOf(member) == 2)
                {
                    member.transform.localPosition = teamPos3;
                }

                if (size == 6)
                {
                    if (Team.IndexOf(member) == 3)
                    {
                        member.transform.localPosition = teamPos4;
                    }

                    if (Team.IndexOf(member) == 4)
                    {
                        member.transform.localPosition = teamPos5;
                    }

                    if (Team.IndexOf(member) == 5)
                    {
                        member.transform.localPosition = teamPos6;
                    }
                }
            }
        }

    }

    private void RemoveTeamMemeberFromBox(GameObject member)
    {
        member.transform.SetParent(Parent.transform, false);
        member.transform.localPosition = pokemonPositions[member];
        member.transform.localScale = new Vector3(300.0f, 300.0f, 1.0f);
    }

    private void MovePokemonPositionUp(GameObject removedMember)
    {
        int memberId = Team.IndexOf(removedMember);

        bool canMoveUp = (memberId != Team.Count) ? canMoveUp = true : canMoveUp = false;

        if (canMoveUp)
        {
            // current member gets position of the predecessor

            for (int i = Team.Count - 1; i > memberId; i--)
            {
                GameObject poke = Team[i];

                //Vector3 oldPos = Team[Team.IndexOf(poke) - 1].transform.position;

                poke.transform.position = Team[Team.IndexOf(poke) - 1].transform.position;
            }
        }
    }
    #endregion

    #region Other
    private void InitTeamPosititons()
    {
        teamPos1 = new Vector3(6.2f, -0.45f, 0.0f);
        teamPos2 = new Vector3(7.5f, -.45f, 0.0f);
        teamPos3 = new Vector3(6.2f, -2.15f, 0.0f);

        if (size == 6)
        {
            teamPos4 = new Vector3(7.5f, -2.15f, 0.0f);
            teamPos5 = new Vector3(6.2f, -3.75f, 0.0f);
            teamPos6 = new Vector3(7.5f, -3.75f, 0.0f);
        }
    }
    
    private void EnableButtonsAddPokemonToTeam(bool addButtonEnabled)
    {
        add.enabled = addButtonEnabled;
        remove.enabled = !addButtonEnabled;

        if (addButtonEnabled)
        {
            add.image.color = originalButtonColor;

            remove.image.color = new Color(255.0f, 255.0f, 255.0f, 0.1f);
        }
        else
        {
            add.image.color = new Color(255.0f, 255.0f, 255.0f, 0.1f);

            remove.image.color = originalButtonColor;
        }
    }

    public void SwitchToFightScene()
    {
        SceneManager.LoadScene("BattleScene");
    }
    #endregion
}
