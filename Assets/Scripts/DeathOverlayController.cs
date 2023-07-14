using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathOverlayController : MonoBehaviour
{
    [SerializeField] GameObject MoneyLeft;
    [SerializeField] GameObject MoneyRight;
    [SerializeField] GameObject KilledBy;
    // Start is called before the first frame update
    public void ConfigureOverlay(int startingMoney, int endingMoney, string nameOfSourceOfDeath) {
        MoneyLeft.gameObject.GetComponent<Text>().text = ("$" + startingMoney);
        MoneyRight.gameObject.GetComponent<Text>().text = ("$" + endingMoney);
        KilledBy.gameObject.GetComponent<Text>().text = ("Killed by: " + nameOfSourceOfDeath);
    }
}
