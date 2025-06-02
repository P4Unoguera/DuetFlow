using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundStars : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatHeight = 5f;
    public float rotationSpeed = 45f;

    private Vector3 originalPosition;
    private string currentScene;

    void Start()
    {
        originalPosition = transform.position;
        currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

        // Title/Selection scenes
        if (currentScene == "Title" || currentScene == "Selection")
        {
            float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
        }
        // Competition scene
        else if (currentScene == "Competition Mode")
        {
            Competition();
        }
    }

    void Competition()
    {
        if (ScoreManager.Instance.score1 == ScoreManager.Instance.score2)
        {
            // Tie - float up/down
            float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
        }
        else if (ScoreManager.Instance.score1 > ScoreManager.Instance.score2)
        {
            // Player1 winning - rotate right
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Player2 winning - rotate left
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}