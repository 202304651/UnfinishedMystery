using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public Image doorImage;
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;
    public string nextSceneName = "Level1";

    public float closedDoorTime = 1.5f;
    public float openDoorTime = 1.0f;

    void Start()
    {
        StartCoroutine(LoadingSequence());
    }

    IEnumerator LoadingSequence()
    {
        // Start with closed door
        doorImage.sprite = closedDoorSprite;

        // Wait while loading screen is shown
        yield return new WaitForSeconds(closedDoorTime);

        // Change to open door
        doorImage.sprite = openDoorSprite;

        // Wait a little so player sees the opening
        yield return new WaitForSeconds(openDoorTime);

        // Go to the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}