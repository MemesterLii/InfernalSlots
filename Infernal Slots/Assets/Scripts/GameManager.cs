using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool canSpin = false;
    int remainingDebt = 100;

    [SerializeField] Counter coinCounter;
    [SerializeField] Handle handle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CoinInserted()
    {
        if (!canSpin)
        {
            remainingDebt -= 1;
            coinCounter.UpdateDebt(remainingDebt);

            handle.EnableGrab();
            canSpin = true;
        }
    }

    public void SpinReceived()
    {

    }
}
