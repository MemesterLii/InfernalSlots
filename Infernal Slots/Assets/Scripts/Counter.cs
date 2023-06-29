using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshPro debt;
    string coin = "Coin";

    void Awake()
    {
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

    public void UpdateDebt(int remainingDebt)
    {
        debt.text = "Debt: " + remainingDebt + " Coins";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(coin))
        {
            gameManager.CoinInserted();
        }
    }
}
