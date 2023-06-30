using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LeftTeleportManager : MonoBehaviour
{
    [SerializeField] InputActionAsset actionAsset;
    [SerializeField] XRRayInteractor rayInteractor;
    [SerializeField] XRInteractorLineVisual reticle;
    [SerializeField] TeleportationProvider teleportationProvider;
    InputAction thumbstick;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;
        reticle.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        thumbstick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (thumbstick.ReadValue<Vector2>() != Vector2.zero)
        {
            return;
        }

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            reticle.enabled = false;
            isActive = false;
            return;
        }

        if (!hit.collider.gameObject.CompareTag("Teleport"))
        {
            rayInteractor.enabled = false;
            reticle.enabled = false;
            isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
        };

        teleportationProvider.QueueTeleportRequest(request);
        rayInteractor.enabled = false;
        reticle.enabled = false;
        isActive = false;
    }

    void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        reticle.enabled = true;
        isActive = true;
    }

    void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        reticle.enabled = false;
        isActive = false;
    }
}
