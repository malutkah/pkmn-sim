﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaySettings : MonoBehaviour
{
    [HideInInspector]
    public int level = 0;

    [HideInInspector]
    public int TeamSize = 0;

    private InputField field;

    #region Tags for pokemon
    public string InTeam = "InTeam";
    public string NotInTeam = "NotInTeam";
    public string InBattle = "InBattle";
    public string InBattleTeam = "InBattleTeam"; // is battling but in the team
    public string Dead = "Dead";
    #endregion

    private void Start()
    {
        field = GameObject.Find("Input_MaxLvl").GetComponent<InputField>();
    }

    /// <summary>
    /// UNDONE: TODO: BUTTON CLICKS
    /// 
    #region BUTTON CLICKS
    public void VsSix_OnClick()
    {
        // set team size
        TeamSize = 6;

        // get max level
        GetLevelFromEdit();

        // don't destroy on load: Team & Settings_Handler
        DontDestroyOnLoad(gameObject);

        // Change Scenes
        SceneManager.LoadScene("PokemonBox");
    }

    public void VsThree_OnClick()
    {
        // set team size
        TeamSize = 3;

        // get max level
        GetLevelFromEdit();

        // don't destroy on load: Team & Settings_Handler
        DontDestroyOnLoad(gameObject);

        // Change Scenes
        SceneManager.LoadScene("PokemonBox");
    }
    #endregion

    /// DONE: TODO: GET MAX LEVEL FROM INPUT FIELD
    /// 
    public void GetLevelFromEdit()
    {
        if (field.text == "")
        {
            field.text = "0";
        }

        level = Convert.ToInt32(field.text);

        if (level <= 0)
        {
            level = 50;
            field.text = level.ToString();
        }
        
        if (level > 100)
        {
            level = 100;
            field.text = level.ToString();
        }
    }

    /// UNDONE: TODO: SAVE SETTINGS GLOBAL (DON'T DESTROY ON LOAD)
    /// UNDONE: PASS ON MAX LEVEL
    /// </summary>
    /// 

}