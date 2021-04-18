using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlaySettings : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI progressText;
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private GameObject loadingScreen;

    [HideInInspector]
    public int level = 0;

    [HideInInspector]
    public int TeamSize = 0;

    private InputField field;

    #region Tags for pokemon
    [HideInInspector]
    public string InTeam = "InTeam";
    [HideInInspector]
    public string NotInTeam = "NotInTeam";
    [HideInInspector]
    public string InBattle = "InBattle";
    [HideInInspector]
    public string InBattleTeam = "InBattleTeam"; // isn't battling but in the team
    [HideInInspector]
    public string Dead = "Dead";
    #endregion

    [HideInInspector]
    public GameObject LastClickedPokemon;

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
        //SceneManager.LoadScene("PokemonBox");
        StartCoroutine(LoadAsync("PokemonBox"));
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
        //SceneManager.LoadScene("PokemonBox");
        StartCoroutine(LoadAsync("PokemonBox"));
    }
    #endregion

    private void Fight_OnClick()
    {
        //SceneManager.LoadScene("BattleScene");
        StartCoroutine(LoadAsync("BattleScene"));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float loadingProgress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = loadingProgress;

            progressText.text = $"{loadingProgress * 100}%";

            yield return null;
        }
    }
    
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
   

}
