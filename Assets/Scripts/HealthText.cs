using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3 (0, 75f,0 );
    public float timeToFade = 1f;

    RectTransform textTransform;
    TextMeshProUGUI tmp;

    private float timeElapsed = 0f;
    private Color startColor;
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        tmp = GetComponent<TextMeshProUGUI>();
        startColor = tmp.color;
    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime / 2 ;

        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            tmp.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);

        }
        else
        {
            Destroy(gameObject);
        }

    }
}
