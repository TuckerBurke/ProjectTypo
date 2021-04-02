using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private float fadeOutTimer;
    private float currentAlpha;
    private float fadeOutLength;

    // Start is called before the first frame update
    void Start()
    {
        currentAlpha = 1.0f;
        fadeOutLength = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutTimer >= fadeOutLength)
        {
            Destroy(gameObject.transform.root.gameObject);
        }

        Color thisColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(thisColor.r,thisColor.g,thisColor.b,currentAlpha);

        currentAlpha -= Time.deltaTime / fadeOutLength;
        fadeOutTimer += Time.deltaTime;
    }
}
