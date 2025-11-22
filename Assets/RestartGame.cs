using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    // Restart the current scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game pressed!");

#if UNITY_EDITOR
        // This allows quitting to work in the editor (stops Play Mode)
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // This quits the built application
        Application.Quit();
#endif
    }
}
