using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //The coin's falling sound
    [SerializeField] AudioSource source;

    //The coin's shine and poof animation
    [SerializeField] ParticleSystem shine;
    [SerializeField] ParticleSystem poof;

    //The coin's rigidbody
    [SerializeField] Rigidbody rb;

    //Whether the coin is in possession (in the coin bucket or in player's hands) or not
    bool inPossession = true;

    //Whether the coin is allowed to reset or not
    bool canReset = true;

    //The coin's original spawn point
    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the coin's spawn point as the original place it is at
        startingPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //If the coin falls too far down, respawn it
        if (gameObject.transform.position.y < -10)
        {
            Respawn();
        }

        //If the coin is not in possession and is allowed to respawn, start a respawn countdown
        if (!inPossession && canReset)
        {
            StartCoroutine(ResetPosition());
        }
    }

    //Whenever the coin is within the trigger of the coin bucket or player hands, the coin is in possession
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("CoinBucket"))
        {
            inPossession = true;
        }
    }

    //If the coin leaves a trigger, the coin is no longer in possession
    private void OnTriggerExit(Collider other)
    {
        inPossession = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        source.Play();
    }

    //Start a countdown to reset the coin, during which time the coin can not be told to reset. If the coin is not back in possession by the end of the countdown, respawn the coin
    IEnumerator ResetPosition()
    {
        canReset = false;
        yield return new WaitForSeconds(5f);
        if (!inPossession)
        {
            Respawn();
        }
        canReset = true;
    }

    //Make sure the coin's velocity is zero, respawn the coin, and play its shine animation
    //BTW, when you set a transform.position, you MUST set it to a Vector3, not another transform.position. Why? Idk.
    void Respawn()
    {
        poof.Play();
        rb.velocity = Vector3.zero;
        gameObject.transform.position = startingPosition;
        shine.Play();
    }
}
