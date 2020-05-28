
using UnityEngine;
using UnityEngine.Audio;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.05f;
    public float slowDownLength = 5f;
    AudioSource[] sources;
    void Update()
    {
        sources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if ( Input.GetKeyDown(KeyCode.F))
        {
            slowMotion();
        }
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        foreach (AudioSource audioSource in sources)
        {
            audioSource.pitch = Time.timeScale;
        }

    }
    public void slowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        foreach (AudioSource audioSource in sources)
        {
            audioSource.pitch = Time.timeScale * 0.2f;
        }
    }
}
