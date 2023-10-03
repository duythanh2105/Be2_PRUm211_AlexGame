using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class play_intro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string menuSceneName = "MenuStart";

    private bool hasVideoEnded = false;

    void Start()
    {
        // Đăng ký sự kiện khi video kết thúc
        videoPlayer.loopPointReached += OnVideoEnd;

        // Phát video intro
        videoPlayer.Play();
    }

    void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        // Khi video kết thúc, chuyển đổi đến scene menu
        SceneManager.LoadScene(menuSceneName);
    }

    // Hàm này sẽ không cần thiết nữa
    /*
    void Update()
    {
        // Kiểm tra khi video intro đã phát xong
        if (!videoPlayer.isPlaying)
        {
            // Chuyển đổi đến scene menu
            SceneManager.LoadScene(menuSceneName);
        }
    }
    */

    void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi script bị hủy
        videoPlayer.loopPointReached -= OnVideoEnd;
    }
}