using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTester : MonoBehaviour
{
    //This is a collision tester. Only to be used when I'm losing my mind over whether two things are colliding or not.

    //We're checking whether this game object is triggering the collider of the gameobject this script is attached to
    [SerializeField] GameObject thatThing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When a thing enters this thing's collider, check if that thing is the thing that we're searching for. If yes, tell me.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A thing has entered this thing's trigger.");

        if (other.gameObject == thatThing)
        {
            Debug.Log("That thing has entered this thing's trigger.");
        }
    }
}
