using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    float fadeInTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginFade()
    {
        StartCoroutine(Fade(GetComponent<SpriteRenderer>()));
    }

    IEnumerator Fade(SpriteRenderer sprite)
    {
        Color tempColor = sprite.color;

        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeInTime;
            sprite.color = tempColor;

            if (tempColor.a >= 1f)
            {
                tempColor.a = 1f;
            }
            yield return null;
        }

        sprite.color = tempColor;
    }
}
