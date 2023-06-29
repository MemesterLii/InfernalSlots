using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Handle : MonoBehaviour
{
    [SerializeField] XRGrabInteractable handleEnd;

    private void Awake()
    {
        handleEnd.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableGrab()
    {
        handleEnd.enabled = true;
    }

    void DisableGrab()
    {
        handleEnd.enabled = false;
    }
}
