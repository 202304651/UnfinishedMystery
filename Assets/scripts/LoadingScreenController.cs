using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreenController : MonoBehaviour
{
    public Image doorImage;
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;
    public TextMeshProUGUI loadingText;

    public string nextSceneName = "Level1";

    public float minimumLoadingTime = 2f;
    public float openDoorDelay = 1f;

    private AsyncOperation asyncLoad;

    void Start()
    {
        if (doorImage == null || closedDoorSprite == null || openDoorSprite == null || loadingText == null)
        {
            Debug.LogError("LoadingScreenController: Missing references!");
            return;
        }

        StartCoroutine(LoadSceneRoutine());
    }

    IEnumerator LoadSceneRoutine()
    {
        doorImage.sprite = closedDoorSprite;

        float timer = 0f;

        asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        if (asyncLoad == null)
        {
            Debug.LogError("Failed to load scene: " + nextSceneName);
            yield break;
        }

        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f || timer < minimumLoadingTime)
        {
            timer += Time.deltaTime;

            float sceneProgress = asyncLoad.progress / 0.9f;
            float timeProgress = timer / minimumLoadingTime;
            float progress = Mathf.Min(sceneProgress, timeProgress);
            progress = Mathf.Clamp01(progress);

            int percentage = Mathf.RoundToInt(progress * 100f);
            loadingText.text = "Loading... " + percentage + "%";

            yield return null;
        }

        loadingText.text = "Loading... 100%";

        doorImage.sprite = openDoorSprite;

        yield return new WaitForSeconds(openDoorDelay);

        asyncLoad.allowSceneActivation = true;
    }
}