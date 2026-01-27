using TMPro;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    private float fps;
    public TextMeshProUGUI FPSCounterText;

    private void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        FPSCounterText.text = "FPS: " + fps.ToString();
    }
}
