using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby_Manager : MonoBehaviour
{
    public void SinglePlayerGame()
    {
        SceneManager.LoadScene(1);
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }
    public void TwoPlayerGame()
    {
        SceneManager.LoadScene(2);
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }
    public void OptionsMenu()
    {
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }
}
