using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image m_img;

    public AnimationCurve m_curve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string a_scene)
    {
        StartCoroutine(FadeOut(a_scene));
    }

    IEnumerator FadeIn()
    {
        float m_t = 1.0f;
        
        while (m_t > 0.0f)
        {
            m_t -= Time.deltaTime;
            float m_a = m_curve.Evaluate(m_t);
            m_img.color = new Color(0.0f, 0.0f, 0.0f, m_a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string a_scene)
    {
        float m_t = 0.0f;

        while (m_t < 1.0f)
        {
            m_t += Time.deltaTime;
            float m_a = m_curve.Evaluate(m_t);
            m_img.color = new Color(0.0f, 0.0f, 0.0f, m_a);
            yield return 0;
        }

        SceneManager.LoadScene(a_scene);
    }
}
