using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Whether the slot machine can be spun or not and the remaining debt the player has
    bool coinInserted = false;
    int remainingDebt = 100;

    //The coin counter's, the handle's, and the reels' scripts
    [SerializeField] Counter coinCounter;
    [SerializeField] Handle handle;
    [SerializeField] Reel[] reels;

    [SerializeField] FireworkLauncher launcher;
    [SerializeField] GameObject ascensionLight;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] FadeIn winScreen;
    int ascensionRate = 2;

    //The coin prefab and coin spawn points
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject[] coinSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        ascensionLight.SetActive(false);

        //Give the player 10 coins to start off with
        SpawnCoins(10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Once a coin has been inserted into the coin counter, this function will run
    public void CoinInserted(GameObject coin, bool instaWin)
    {
        //If there is not already a coin inserted, then destroy the given coin, decrease debt by 1 coin,
        //update the debt display, allow the player to pull the handle, and register that a coin has been inserted
        if (!coinInserted)
        {
            Destroy(coin);
            if (instaWin)
            {
                remainingDebt = 0;
                coinCounter.UpdateDebt(remainingDebt);
            }
            else
            {
                remainingDebt -= 1;
                coinCounter.UpdateDebt(remainingDebt);
            }

            handle.EnableGrab();
            coinInserted = true;
        }
    }

    //If the player pulls the handle, this function will run
    public void SpinReceived()
    {
        //Spin each reel in the reel array
        foreach (Reel reel in reels)
        {
            reel.Spin();
        }

        //Begin the StopReels() coroutine
        StartCoroutine(StopReels());
    }

    //Allows other scripts to call the spawning of coins
    public void SpawnCoins(int amount)
    {
        //Upon call, begin the CoinSpawnCooldown coroutine with the given amount of coins to spawn
        StartCoroutine(CoinSpawnCooldown(amount));
    }

    IEnumerator StopReels()
    {
        int[] results = new int[3];
        //For all the reels, wait for a second, get a random result, and then tell that reel to stop on the given result
        foreach (Reel reel in reels)
        {
            int reelIndex = System.Array.IndexOf(reels, reel);
            yield return new WaitForSeconds(1);
            int result = Random.Range(0, 10);
            results[reelIndex] = result;
            reel.StopSpin(result);
        }

        if (results.Length > results.Distinct().Count())
        {
            SpawnCoins(3);
            if(results.Distinct().Count() == 1)
            {
                SpawnCoins(17);
            }
        }

        //Allow the next coin to be inserted by the player
        coinInserted = false;
    }

    IEnumerator CoinSpawnCooldown(int amount)
    {
        //For each coin to spawn, get a random spawn point among the coinSpawnPoints array, spawn in a coin there,
        //and wait 0.2 seconds before spawning the next coin
        for (int i = 0; i < amount; i++)
        {
            int spawnLocation = Random.Range(0, coinSpawnPoints.Length);
            GameObject coin = Instantiate(coinPrefab, coinSpawnPoints[spawnLocation].transform.position, coinSpawnPoints[spawnLocation].transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void GameOver(bool playerVictorious)
    {
        if (playerVictorious)
        {
            launcher.SetOff();
            StartCoroutine(Ascension());
        }
    }

    IEnumerator Ascension()
    {
        yield return new WaitForSeconds(5);
        ascensionLight.SetActive(true);
        playerRb.useGravity = false;
        playerRb.velocity = Vector2.up * ascensionRate;
        yield return new WaitForSeconds(5);
        winScreen.BeginFade();
    }
}
