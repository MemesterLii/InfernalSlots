using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LeftTeleportManager : MonoBehaviour
{
    //This script will only be used if the player uses the right controller to teleport

    //Library of User Inputs
    [SerializeField] InputActionAsset actionAsset;

    //The value of the thumbstick input axis
    InputAction thumbstick;

    //Whether teleportation mode is active or not
    bool isActive;

    //Pre-built framework that allows teleportation
    [SerializeField] TeleportationProvider teleportationProvider;

    //Visuals
    [SerializeField] XRRayInteractor rayInteractor;
    [SerializeField] XRInteractorLineVisual reticle;

    // Start is called before the first frame update
    void Start()
    {
        //Teleportation visuals being disabled
        rayInteractor.enabled = false;
        reticle.enabled = false;

        //Defines the player input that will activate teleportation mode
        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        //Defines the player input that will cancel teleportation mode
        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        //Defines the player input that will be tracked to determine further actions in Update()
        thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        thumbstick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //If the teleportation mode is not currently active, don't do anything
        if (!isActive)
        {
            return;
        }

        //If the teleportation mode is active but the thumbstick is still being moved, don't do anything
        if (thumbstick.ReadValue<Vector2>() != Vector2.zero)
        {
            return;
        }

        //If the teleportation mode is active and the thumbstick has returned to zero
        //but the player didn't aim at anything, disable teleport visuals and deactivate teleportation mode
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            reticle.enabled = false;
            isActive = false;
            return;
        }

        //If the teleportation mode is active, the thumbstick has returned to zero, and the player aimed at something
        //but that something is not a "teleport area", then disable teleport visuals and deactivate teleportation mode
        if (!hit.collider.gameObject.CompareTag("Teleport"))
        {
            rayInteractor.enabled = false;
            reticle.enabled = false;
            isActive = false;
            return;
        }

        //If this runs, that means that the teleportation mode is active, the thumbstick had returned to zero, the player
        //aimed at something, and that something was a "teleport area". Now this script is requesting a teleport to the
        //point the player aimed at
        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
        };

        teleportationProvider.QueueTeleportRequest(request);

        //After requesting a teleport, the script disables visuals and deactivates teleportation mode
        rayInteractor.enabled = false;
        reticle.enabled = false;
        isActive = false;
    }

    //If the player pushed the thumbstick forward or back (teleportation mode activation), teleport visuals will enable
    //and teleportation mode will activate
    void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        reticle.enabled = true;
        isActive = true;
    }

    //If the player pressed the grip (teleportation mode cancel), teleport visuals will disable and teleportation mode
    //will deactivate
    void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        reticle.enabled = false;
        isActive = false;
    }
}
