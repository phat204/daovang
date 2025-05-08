using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayBackgroundSound();
    }

    public void PlayGame() {
        AudioManager.Instance.StopBackgroundSound();
        SceneManager.LoadScene(0);
    }
}
