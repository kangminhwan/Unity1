using UnityEngine;

public sealed class cTimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 1.0f;

    private void Update()
    {
        if (Time.timeScale == 1) return;
        Time.timeScale += (1.0f / slowdownLength)*Time.deltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

    }

}
