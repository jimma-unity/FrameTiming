using UnityEngine;
using TMPro;

public class FrameTimes : MonoBehaviour
{
    public TMP_Text gpuFrameTime;
    public TMP_Text cpuMainThreadPresentWaitTime;

    static FrameTiming[] m_frameTimings = new FrameTiming[1];

    static FrameTiming m_sum = new FrameTiming();

    private int frameCount = 0;

    private void Update()
    {
        bool success = UpdateFrameTimings();
        if (!success)
            return;

        var f = m_frameTimings[0];

        if (frameCount > 1200)
            ResetAverage();

        m_sum.cpuFrameTime += f.cpuFrameTime;
        m_sum.cpuMainThreadFrameTime += f.cpuMainThreadFrameTime;
        m_sum.cpuMainThreadPresentWaitTime += f.cpuMainThreadPresentWaitTime;
        m_sum.cpuRenderThreadFrameTime += f.cpuRenderThreadFrameTime;
        m_sum.gpuFrameTime += f.gpuFrameTime;
        frameCount++;

        gpuFrameTime.text =
        (m_sum.cpuFrameTime / frameCount).ToString("F2") + "\n"
        + (m_sum.cpuMainThreadFrameTime / frameCount).ToString("F2") + "\n"
        + (m_sum.cpuMainThreadPresentWaitTime / frameCount).ToString("F2") + "\n"
        + (m_sum.gpuFrameTime / frameCount).ToString("F2") + "\n";

        bool UpdateFrameTimings()
        {
            FrameTimingManager.CaptureFrameTimings();
            return FrameTimingManager.GetLatestTimings(1, m_frameTimings) > 0;
        }
    }

    void ResetAverage()
    {
        m_sum = new FrameTiming();
        frameCount = 0;
    }
}
