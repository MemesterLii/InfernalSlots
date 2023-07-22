using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class Handle : MonoBehaviour
{
    //Game Manager
    [SerializeField] GameManager gameManager;

    //The component of the handle that allows grabbing it
    [SerializeField] XRGrabInteractable handle;

    //Rigidbody and transform
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform handleTransform;

    //Whether the handle is down or not and the speed at which the handle should return to starting position
    bool handleDown = false;
    float speed = 5f;

    private void Awake()
    {
        //Make sure the player can't grab the handle and that the handle is not registered as down upon starting
        handle.enabled = false;
        handleDown = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //If the handle is rotated below a certain point and the handle is not already registered as down,
        //stop the player from grabbing the handle, register the handle as being in the down position, and tell
        //the game manager that the player has spun the slot machine
        if (handleTransform.rotation.x <= -0.7 && !handleDown)
        {
            DisableGrab();
            handleDown = true;
            gameManager.SpinReceived();
        }

        //If the handle is rotated above a certain point and the handle is registered as down,
        //stop rotating the handle and register the handle as being up
        if (handleTransform.rotation.x >= -0.001 && handleDown)
        {
            rb.angularVelocity = Vector3.zero;
            handleDown = false;
            rb.freezeRotation = true;
        }

        //If the handle is "down", rotate the handle back up at a given speed
        if (handleDown)
        {
            rb.angularVelocity = Vector3.right * speed;
        }
    }

    //Allows other scripts to allow the player to grab the handle
    public void EnableGrab()
    {
        handle.enabled = true;

        rb.angularVelocity = Vector3.zero;
        handleTransform.eulerAngles = Vector3.zero;
        rb.freezeRotation = false;
    }

    //Disables the ability of the player to grab the handle
    void DisableGrab()
    {
        handle.enabled = false;
    }
}
