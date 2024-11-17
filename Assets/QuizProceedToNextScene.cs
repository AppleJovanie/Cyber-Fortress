using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizProceedToNextScene : MonoBehaviour
{
    public void Proceed()
    {
        // Load the next scene by incrementing the current scene's build index
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex); // Proceed to the next scene
    }

    public void TryAgain()
    {
        // Load the previous scene by decrementing the current scene's build index
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(previousSceneIndex); // Go back to the previous scene
    }
}
