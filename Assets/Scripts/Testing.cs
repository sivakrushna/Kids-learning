using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Testing : MonoBehaviour
{
    public Button testButton;
    public TextMeshProUGUI welcomeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        welcomeText.text = "Welcome to the game!";
        Debug.Log("Game Started");
        testButton.onClick.AddListener(Play);
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Game is running");
    }
    [SerializeField]
    public void Play()
    {
        Debug.Log("Testing Button Pressed");
        welcomeText.text = "Welcome to the Game !";
    }
}
