using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Call this function when the button is pressed
    public void Quit()
    {
#if UNITY_EDITOR
        // If running in the editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running as a built application, quit
        Application.Quit();
#endif
    }
}
