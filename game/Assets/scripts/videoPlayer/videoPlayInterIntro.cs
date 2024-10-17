using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoPlayInterIntro : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer videoPlayer;
    // public string mainMenuSceneName;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("GroundLevel");
    }
}
