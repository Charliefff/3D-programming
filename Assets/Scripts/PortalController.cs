using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject magicCircle;
    public GameObject magicCircleLite;

    private float fadeDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SetVisibility(magicCircle, false);
        magicCircleLite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)){
            FadeIn();
            magicCircleLite.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.M)){
            FadeOut();
            magicCircleLite.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {   
        if(other.name == "PlayerHandle")
        {
            FadeOut();
            magicCircleLite.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerHandle")
        {
            FadeIn();
            magicCircleLite.SetActive(false);
        }
    }    

    public void FadeIn()
    {
        StartCoroutine(FadeMaterialAlpha(1f, 0f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeMaterialAlpha(0f, 1f));
    }

    private IEnumerator FadeMaterialAlpha(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        Renderer[] renderers = magicCircle.GetComponentsInChildren<Renderer>();

        Color[] originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
                originalColors[i] = renderers[i].material.color;
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.HasProperty("_Color"))
                {
                    Color newColor = originalColors[i];
                    newColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
                    renderers[i].material.color = newColor;
                }
            }
            yield return null;
        }
    }

    private void SetVisibility(GameObject obj, bool isVisible)
    {
        float alpha = isVisible ? 1.0f : 0.0f;
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        
        foreach (Renderer r in renderers)
        {
            if (r.material.HasProperty("_Color"))
            {
                Color color = r.material.color;
                color.a = alpha;
                r.material.color = color;
            }
        }
    }
}
