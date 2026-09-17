using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class FPS : MonoBehaviour
{
    [SerializeField] private TMP_Text fps;
    [SerializeField] private TMP_Text targetRate;
    [SerializeField] private TMP_Text vSyncCount;
    [SerializeField] private TMP_Text frameInterval;
    private float currentFps;
    private float smoothedFps;
    private float smoothingFactor = 0.1f;

    protected void Start()
    {
        this.targetRate.text = $"Target FPS: {Application.targetFrameRate}";
        this.vSyncCount.text = $"VSync: {QualitySettings.vSyncCount}";
        this.frameInterval.text = $"Render Frame Interval: {OnDemandRendering.renderFrameInterval}";
    }

    private void Update()
    {
        this.currentFps = 1f / Time.unscaledDeltaTime;
        this.smoothedFps = (smoothingFactor * currentFps) + (1f - smoothingFactor) * smoothedFps;
        this.fps.text = "FPS: " + Mathf.Round(smoothedFps);
    }
}
