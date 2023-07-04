using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] ParticleSystem shine;
    [SerializeField] Rigidbody rb;
    bool inPossession = true;
    bool canReset = true;
    Transform startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = gameObject.transform;
        Debug.Log(startingPosition.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -10)
        {
            Debug.Log("Below y-level -10");
            Respawn();
        }

        if (!inPossession && canReset)
        {
            StartCoroutine("ResetPosition");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!inPossession)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("CoinBucket"))
            {
                inPossession = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (inPossession)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("CoinBucket"))
            {
                inPossession = false;
            }
        }
    }

    IEnumerator ResetPosition()
    {
        Debug.Log("Respawn Count Down");
        canReset = false;
        yield return new WaitForSeconds(5f);
        if (!inPossession)
        {
            Respawn();
        }
        canReset = true;
    }

    void Respawn()
    {
        Debug.Log("Respawning");
        rb.velocity = Vector3.zero;
        gameObject.transform.position = new Vector3(startingPosition.position.x, startingPosition.position.y, startingPosition.position.z);
        shine.Play();
    }
}
