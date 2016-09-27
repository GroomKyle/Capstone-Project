using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour {

    /// <summary>
    /// A function that exits the game if playing it, but returns to editor if viewing from Unity Editor
    /// </summary>
	public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
