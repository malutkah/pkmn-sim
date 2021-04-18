using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI progressText;
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private GameObject loadingScreen;


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
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
}
