using UnityEngine;

public class SetFPS : MonoBehaviour
{
    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        foreach (var r in resolutions)
        {
            Debug.Log(r.ToString());
        }

#if UNITY_SWITCH
        int HDwidth = 1920;
        int HDheight = 1080;
        RefreshRate rf60hz = new RefreshRate() { numerator = 60, denominator = 1 };
#endif

#if UNITY_PS5 || UNITY_GAMECORE
        int UHDwidth = 3840;
        int UHDheight = 2160;
        RefreshRate UHD120hz = new RefreshRate() { numerator = 120, denominator = 1 };
#endif  

#if UNITY_PS5 // Attempt to set to 4K 120hz
        //Screen.SetResolution(UHDwidth, UHDheight, true, -1);
        Screen.SetResolution(UHDwidth, UHDheight, FullScreenMode.FullScreenWindow, UHD120hz);
        QualitySettings.vSyncCount = 1;
        if (UnityEngine.PS5.Utility.SetVideoOutMode(UnityEngine.PS5.Utility.videoOutPortHandle, UnityEngine.PS5.Utility.VideoOutMode.HFR_119_88HZ) == 0)
            Debug.Log("VIDEO OUTPUT SET TO 120hz");
        else
            Debug.Log("VIDEO OUTPUT SET TO DEFAULT");
#elif UNITY_GAMECORE // Attempt to set to 4K 120hz
        QualitySettings.vSyncCount = 1;
        Screen.SetResolution(UHDwidth, UHDheight, FullScreenMode.FullScreenWindow, UHD120hz);
#elif UNITY_SWITCH // Attempt to set to 1080p 60hz
        QualitySettings.vSyncCount = 1;
        Screen.SetResolution(HDwidth, HDheight, FullScreenMode.FullScreenWindow, rf60hz);
#elif UNITY_STANDALONE_WIN
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 1;
        //var res = resolutions[resolutions.Length-1];
        //Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow, res.refreshRateRatio);
#endif
    }
}
