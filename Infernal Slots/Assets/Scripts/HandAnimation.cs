using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    //Two inputs that the player can use to animate the hands (trigger and grip respectively)
    [SerializeField] InputActionProperty trigger;
    [SerializeField] InputActionProperty grip;

    //Animator. Controls the animations based on inputed values from player controllers
    [SerializeField] Animator animator;

    //Grip audio
    [SerializeField] AudioSource source;

    //Input name variables
    string pinch = "Trigger";
    string fist = "Grip";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Always read the value of the trigger on the player's controller and tell the animator to animate the pinch animation accordingly
        float triggerValue = trigger.action.ReadValue<float>();
        animator.SetFloat(pinch, triggerValue);

        //Always read the value of the trigger on the player's controller and tell the animator to animate the pinch animation accordingly
        float gripValue = grip.action.ReadValue<float>();
        animator.SetFloat(fist, gripValue);

        if (!grip.action.IsPressed())
        {
            source.Play();
        }
    }
}
