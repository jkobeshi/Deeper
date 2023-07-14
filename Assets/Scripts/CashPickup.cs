using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPickup : MonoBehaviour
{
    public float cashDropped = 0;
    public AudioClip cash_picked_up;
    bool firstGameOver;
    int gameOverCount;
    void Start()
    {
        firstGameOver = false;
        gameOverCount = 0;
        cashDropped = Mathf.Round(inventory.instance.cash / 2);
    }
    public void Update()
    {
        if (!GameControl.instance.gameOver)
        {
            firstGameOver = true;
        }
        if(GameControl.instance.gameOver && firstGameOver)
        {
            Destroy(gameObject);
        }
    }
    public void PickupCash()
    {
        AudioSource.PlayClipAtPoint(cash_picked_up, Camera.main.transform.position, GameControl.sound_effects_vol);
        inventory.instance.gameObject.GetComponent<MessageController>().DisplayMessage(("Retrieved: $" + cashDropped), Color.green);
        inventory.instance.cash += cashDropped;
        Destroy(gameObject);
    }
}
