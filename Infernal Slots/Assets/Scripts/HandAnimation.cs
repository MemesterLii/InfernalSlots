using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    [SerializeField] InputActionProperty pinch;
    [SerializeField] InputActionProperty fist;
    [SerializeField] Animator animator;
    string trigger = "Trigger";
    string grip = "Grip";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinch.action.ReadValue<float>();
        animator.SetFloat(trigger, triggerValue);

        float gripValue = fist.action.ReadValue<float>();
        animator.SetFloat (grip, gripValue);
    }
}
