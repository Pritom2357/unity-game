using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // public string mainMenuSceneName;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

