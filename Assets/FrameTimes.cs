using UnityEngine;
using TMPro;

public class FrameTimes : MonoBehaviour
{
    public TMP_Text gpuFrameTime;
    public TMP_Text cpuMainThreadPresentWaitTime;
    
    static FrameTiming[] m_frameTimings = new FrameTiming[1];
    
    private int frameCount = 0;
    
    private double runningSum = 0;
    private double runningSum2 = 0;
    
    private void Update()
    {
        bool success = UpdateFrameTimings();
        if (!success)
            return;
        
        // Update the running sum and frame count
        runningSum += m_frameTimings[0].gpuFrameTime;
        runningSum2 += m_frameTimings[0].cpuMainThreadPresentWaitTime;
        frameCount++;

        // Calculate the average
        double average = runningSum / frameCount;
        double average2 = runningSum2 / frameCount;

        gpuFrameTime.text = average.ToString("F2");
        cpuMainThreadPresentWaitTime.text = average2.ToString("F2");

        bool UpdateFrameTimings()
        {
            FrameTimingManager.CaptureFrameTimings();
            return FrameTimingManager.GetLatestTimings(1, m_frameTimings) > 0;
        }
    }
}
