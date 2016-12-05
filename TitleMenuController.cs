using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuController : MonoBehaviour
{
    public void OnStartGameButtonPress()
    {
        SceneManager.LoadScene("Main Game Scene", LoadSceneMode.Single);
    }

    public void OnControlsButtonPress()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnExitGameButtonPress()
    {
        Application.Quit();
    }
}
