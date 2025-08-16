using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BrushingSetupController : MonoBehaviour
{
    public GameObject toothpaste; // 3D object with Draggable3D script
    public GameObject brush; // 3D object
    public TextMeshProUGUI timerText;
    public GameObject starRewardPrefab; // UI prefab for reward
    public Canvas canvas; // Assign main Canvas in Inspector

    private float timeLeft = 100f;
    private bool applied = false;
    public GameObject camPosition; // Camera position GameObject

    void Start()
    {
        if (toothpaste == null || brush == null || canvas == null || timerText == null)
        {
            Debug.LogError("Missing references in BrushingSetupController. Check Inspector assignments.");
            return;
        }

        // Ensure toothpaste has Draggable3D script
        if (toothpaste.GetComponent<Drag3DObject>() == null)
        {
            Debug.LogError("Toothpaste missing Draggable3D script.");
        }

        StartCoroutine(Countdown());
        Camera.main.transform.position = camPosition.transform.position; // Set camera position
    }

    void Update()
    {
        if (!applied && IsToothpasteOnBrush())
        {
            applied = true;
            float timeUsed = 10f - timeLeft;
            if (timeUsed <= 5f)
            {
                GameObject reward = Instantiate(starRewardPrefab, canvas.transform);
                RectTransform rect = reward.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector2.zero; // Center in Canvas
                rect.sizeDelta = new Vector2(100, 100); // Adjust size
            }
            ProceedToNext();
        }
    }

    bool IsToothpasteOnBrush()
    {
        // Check distance in world space
        float distance = Vector3.Distance(toothpaste.transform.position, brush.transform.position);
        Debug.Log("3D Distance between toothpaste and brush: " + distance);
        return distance < 0.15f; // Adjust threshold for world space (meters)
    }

    IEnumerator Countdown()
    {
        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeLeft).ToString() + "s";
            yield return null;
        }
        if (!applied) ProceedToNext();
    }

    void ProceedToNext()
    {
       // SceneManager.LoadScene("BrushingTeeth");
    }
}