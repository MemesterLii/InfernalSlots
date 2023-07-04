using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Handle : MonoBehaviour
{
    [SerializeField] XRGrabInteractable handle;
    [SerializeField] Transform handleTransform;

    private void Awake()
    {
        handle.enabled = false;
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
        handle.enabled = true;
    }

    void DisableGrab()
    {
        handleTransform.rotation = Quaternion.Euler(0, 0, 0);
        handle.enabled = false;
    }
}
