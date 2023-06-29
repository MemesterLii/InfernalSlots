using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTester : MonoBehaviour
{
    [SerializeField] GameObject thisThing;
    [SerializeField] GameObject thatThing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A thing has entered this thing's trigger.");

        if (other.gameObject == thatThing)
        {
            Debug.Log("That thing has entered this thing's trigger.");
        }
    }
}
