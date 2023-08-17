using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Game_Controller : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Audio_Manager.instance.Play(SoundName.ButtonClick);
    }

}
