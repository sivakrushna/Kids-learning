using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class WakeUpController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Button wakeButton;
    public Animator childAnimator;
    public GameObject starRewardPrefab; // UI prefab (e.g., Image with star sprite)
    public GameObject smileRewardPrefab; // UI prefab
    public Canvas canvas; // Assign main Canvas in Inspector

    private float timeLeft = 10f;
    private bool tapped = false;

    public GameObject nextFrame;
    public GameObject camPosition;

    void Start()
    {
        wakeButton.onClick.AddListener(OnWakeUpTap);
        StartCoroutine(Countdown());
        starRewardPrefab.SetActive(false); // Initially hide reward prefab
        smileRewardPrefab.SetActive(false); // Initially hide smile reward prefab
        Camera.main.transform.position = camPosition.transform.position; // Set camera position
    }

    void OnWakeUpTap()
    {
        if (tapped) return;
        tapped = true;
        childAnimator.SetTrigger("Wake");

        GameObject reward = null;
        if (timeLeft > 5f)
        {
            //reward = Instantiate(starRewardPrefab, canvas.transform); // Parent to Canvas
            starRewardPrefab.SetActive(true); // Activate star reward prefab
        }
        else if (timeLeft > 0f)
        {
            reward = Instantiate(smileRewardPrefab, canvas.transform); // Parent to Canvas
            smileRewardPrefab.SetActive(true); // Activate smile reward prefab
        }
        if (reward != null)
        {
            // Set position in UI space (e.g., center of screen)
            RectTransform rect = reward.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero; // Center of Canvas
            // Optionally, set size or scale
            rect.sizeDelta = new Vector2(100, 100); // Adjust size as needed
        }
        StartCoroutine(ProceedToNext());
    }

    System.Collections.IEnumerator Countdown()
    {
        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeLeft).ToString() + "s";
            yield return null;
        }
        if (!tapped) StartCoroutine(ProceedToNext()); // No reward
    }

   IEnumerator ProceedToNext()
    {
        yield return new WaitForSeconds(3f); // Optional delay before proceeding
        //SceneManager.LoadScene("BrushingSetup");
        nextFrame.SetActive(true); // Activate next frame if needed
        starRewardPrefab.SetActive(false); // Initially hide reward prefab
        smileRewardPrefab.SetActive(false); // Initially hide smile reward prefab
        this.gameObject.SetActive(false); // Hide this controller
    }
}