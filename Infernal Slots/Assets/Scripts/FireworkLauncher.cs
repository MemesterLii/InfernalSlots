using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireworkLauncher : MonoBehaviour
{
    [SerializeField] GameObject fireworks;
    [SerializeField] AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        fireworks.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOff()
    {
        fireworks.SetActive(true);
        source.Play();
    }
}
