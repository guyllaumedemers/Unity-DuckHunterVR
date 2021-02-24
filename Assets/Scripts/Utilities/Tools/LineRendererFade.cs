using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererFade : MonoBehaviour
{
    #region Singleton
    private static LineRendererFade instance;

    private LineRendererFade() { }

    public static LineRendererFade Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LineRendererFade();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion
    public IEnumerator FadeLineRenderer(LineRenderer lineRenderer)
    {
        Gradient lineRendererGradient = new Gradient();
        float fadeSpeed = 3f;
        float timeElapsed = 0f;
        float alpha = 1f;

        while (timeElapsed < fadeSpeed)
        {
            alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeSpeed);

            lineRendererGradient.SetKeys
            (
                lineRenderer.colorGradient.colorKeys,
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1f) }
            );
            lineRenderer.colorGradient = lineRendererGradient;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(lineRenderer.gameObject);
    }
}
