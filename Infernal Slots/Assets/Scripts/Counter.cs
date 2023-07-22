using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    //Game Manager and audio source
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource source;

    //The text on the slot machine displaying the player's debt and a name variable for "Coin"
    [SerializeField] TextMeshPro debt;
    string coin = "Coin";

    void Awake()
    {
        //When the player loads in, set their debt to 100 coins
        debt.text = "Debt: 100 Coins";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //If the object that entered the counter's trigger is a coin, tell the game manager that a coin has been inserted
        //and pass the coin in as the argument
        if (other.gameObject.CompareTag(coin))
        {
            gameManager.CoinInserted(other.gameObject);
        }
    }

    //Allows other scripts to update the debt displayed
    public void UpdateDebt(int remainingDebt)
    {
        source.Play();
        debt.text = "Debt: " + remainingDebt + " Coins";
    }
}
