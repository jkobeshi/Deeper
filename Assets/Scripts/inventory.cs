using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class inventory : MonoBehaviour
{
    public class Item {
        public string name;
        public GameObject item;
        public Sprite icon;
        public float cost;
        public VideoClip tutorial_vid;
        public char type;
        public float cd;
        public bool bought;
        public Item(string a, GameObject d, Sprite img, float e, VideoClip vid, char f, float g, bool b) {
            this.name = a; this.item = d; this.icon = img; this.cost = e; this.tutorial_vid = vid; type = f; cd = g; bought = b;
        }
    }
    public static inventory instance;
    private GameObject used_utility;
    [SerializeField] GameObject light_beam;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject radar;
    [SerializeField] GameObject battery;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject lightcapacity;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject DrillHardness;
    [SerializeField] GameObject SaveUpdog;
    [SerializeField] GameObject KillUpdog;
    [SerializeField] PlayerMovement PlayerMovementComponent;
    public Image utilityIcon;
    [SerializeField] GameObject cooldownDisplay;
    [SerializeField] ItemUIManager UI_Tile_Manager;
    public List <Item> utilities = new List<Item>();
    public List<int> Quant = new List<int>();
    public List<bool> onCd = new List<bool>();
    public List<int> NumBoughtForUpgrades = new List<int>();
    public List<int> MaxBoughtForUpgrades = new List<int>();
    public List<Vector3> dispTimeScale = new List<Vector3>();
    public float cash;
    public int current_index = 0;
    public int hardness = 0;
    public bool Shield = false;
    public bool shieldBought = false;
    public bool firstItemAcquired = false;
    async void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // cooldownDisplay = GameObject.Find("Canvas/Item_Slots/Active_Item_Slot/CooldownBar");
        UI_Tile_Manager = GameObject.Find("Canvas/Item_Slots").gameObject.GetComponent<ItemUIManager>();
        //name, quantity, max quantity, gameobject, cost to use
        utilities.Add(new Item("Mining Beam", laser, laser.gameObject.GetComponent<ImageStore>().getImage(), 125 * 10, laser.gameObject.GetComponent<ImageStore>().getVideo(), 'p', 20f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(0);
        utilities.Add(new Item("Radar", radar, radar.gameObject.GetComponent<ImageStore>().getImage(), 5* 10, radar.GetComponent<ImageStore>().GetComponent<ImageStore>().getVideo(), 'p', 8f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(0);
        utilities.Add(new Item("Shock Stun", light_beam, light_beam.gameObject.GetComponent<ImageStore>().getImage(), 100* 10, light_beam.gameObject.GetComponent<ImageStore>().getVideo(), 'p', 17f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(0);
        utilities.Add(new Item("Battery Pack", battery, battery.gameObject.GetComponent<ImageStore>().getImage(), 30* 10, battery.gameObject.GetComponent<ImageStore>().getVideo(), 'q', 5f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(0);
        utilities.Add(new Item("Torch", flashlight, flashlight.gameObject.GetComponent<ImageStore>().getImage(), 10* 10, flashlight.gameObject.GetComponent<ImageStore>().getVideo(), 'q', 5f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(0);
        utilities.Add(new Item("Lightbulb", lightcapacity, lightcapacity.gameObject.GetComponent<ImageStore>().getImage(), 150* 10, lightcapacity.gameObject.GetComponent<ImageStore>().getVideo(), 'u', 0f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(1);
        utilities.Add(new Item("Shield", shield, shield.gameObject.GetComponent<ImageStore>().getImage(), 100* 10, shield.gameObject.GetComponent<ImageStore>().getVideo(), 'u', 0f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(3);
        utilities.Add(new Item("Drill", DrillHardness, DrillHardness.gameObject.GetComponent<ImageStore>().getImage(), 175* 10, DrillHardness.gameObject.GetComponent<ImageStore>().getVideo(), 'u', 0f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(2);
        utilities.Add(new Item("Save Updog", SaveUpdog, SaveUpdog.gameObject.GetComponent<ImageStore>().getImage(), 0, SaveUpdog.gameObject.GetComponent<ImageStore>().getVideo(), 'u', 0f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(1);
        utilities.Add(new Item("Remove Updog", KillUpdog, KillUpdog.gameObject.GetComponent<ImageStore>().getImage(), 0, KillUpdog.gameObject.GetComponent<ImageStore>().getVideo(), 'u', 0f, false));
        Quant.Add(0); onCd.Add(false); dispTimeScale.Add(Vector3.zero); NumBoughtForUpgrades.Add(0); MaxBoughtForUpgrades.Add(1);
        PlayerMovementComponent = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {

        // if (onCd[current_index])
        // {
        //     cooldownDisplay.transform.localScale = dispTimeScale[current_index];
        // }
        // else
        // {
        //     cooldownDisplay.transform.localScale = new Vector3(cooldownDisplay.transform.localScale.x, 0, cooldownDisplay.transform.localScale.z);
        // }
        for(int i = 0; i < utilities.Count; ++i)
        {
            if ((utilities[i].type == 'u') && (Quant[i] > 0))
            {
                Quant[i]--;
                used_utility = GameObject.Instantiate(utilities[i].item, gameObject.transform.position, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !PlayerMovementComponent.shop_active && !GameControl.instance.gameOver) {
            string item_name_to_switch_to = UI_Tile_Manager.GetItemNameAtIndex(0);
            ChangeCurrentIndexGivenName(item_name_to_switch_to); UseIndex();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !PlayerMovementComponent.shop_active && !GameControl.instance.gameOver) {
            string item_name_to_switch_to = UI_Tile_Manager.GetItemNameAtIndex(1);
            ChangeCurrentIndexGivenName(item_name_to_switch_to); UseIndex();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !PlayerMovementComponent.shop_active && !GameControl.instance.gameOver) {
            string item_name_to_switch_to = UI_Tile_Manager.GetItemNameAtIndex(2);
            ChangeCurrentIndexGivenName(item_name_to_switch_to); UseIndex();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !PlayerMovementComponent.shop_active && !GameControl.instance.gameOver) {
            string item_name_to_switch_to = UI_Tile_Manager.GetItemNameAtIndex(3);
            ChangeCurrentIndexGivenName(item_name_to_switch_to); UseIndex();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && !PlayerMovementComponent.shop_active && !GameControl.instance.gameOver) {
            string item_name_to_switch_to = UI_Tile_Manager.GetItemNameAtIndex(4);
            ChangeCurrentIndexGivenName(item_name_to_switch_to); UseIndex();
        }
    }
    void UseIndex()
    {
        if (!PlayerMovementComponent.shop_active && !GameControl.instance.gameOver)
        {
            if (!onCd[current_index] && (Quant[current_index] > 0))
            {
                if (utilities[current_index].type == 'q')
                {
                    Quant[current_index]--;
                    UI_Tile_Manager.UpdateQuantityAfterUse(utilities[current_index], Quant[current_index]);
                    if (utilities[current_index].name == "Torch")
                    {
                        used_utility = GameObject.Instantiate(utilities[current_index].item, gameObject.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        used_utility = GameObject.Instantiate(utilities[current_index].item, gameObject.transform.position + new Vector3(0, 0, -9f), Quaternion.identity);
                    }
                    StartCoroutine(StartCooldown(current_index));
                }
                else if (utilities[current_index].type == 'p')
                {
                    used_utility = GameObject.Instantiate(utilities[current_index].item, gameObject.transform.position + new Vector3(0, 0, -9f), Quaternion.identity);
                    StartCoroutine(StartCooldown(current_index));
                }
            }
        }
    }
    private void ChangeCurrentIndexGivenName(string item_name) {
        // Find the index with item that has matching name, then set current index to that
        if (item_name != "") {
            for (int i = 0; i < utilities.Count; ++i)
            {
                if (utilities[i].name == item_name) {
                    current_index = i;
                    break;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "CashPickup" && !PlayerMovement.instance.resMov)
        {
            collision.GetComponent<CashPickup>().PickupCash();
        }
    }
    public void Collect(GameObject Ore)
    {
        if (Ore.tag == "Breakable")
            cash += (1* 10);
        else if (Ore.tag == "Dirt2")
        {
            cash += 3 * 10;
        }
        else {
            int cashValue = 0;
            if (Ore.tag == "Coal") {
                cashValue = 10* 10;
            }
            if (Ore.tag == "Iron") {
                cashValue = 15* 10;
            }
            if (Ore.tag == "Gold") {
                cashValue = 40* 10;
            }
            if (Ore.tag == "Diamond") {
                cashValue = 100* 10;
            }
            if (Ore.tag == "Emerald") {
                cashValue = 120* 10;
            }
            if (Ore.tag == "Jade") {
                cashValue = 150* 10;
            }
            if (Ore.tag == "Ruby") {
                cashValue = 180* 10;
            }
            if (Ore.tag == "AlienOre") {
                cashValue = 500* 10;
            }
            cash += cashValue;
            if (cashValue > 0) {
                gameObject.GetComponent<MessageController>().DisplayMessage(("" + Ore.tag + " mined: + $" + cashValue), Color.green);
            }
        }
    }

    IEnumerator StartCooldown(int ind) {
        onCd[ind] = true;
        Debug.Log(utilities[ind]);
        float timeLeft = utilities[ind].cd;
        while (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            // dispTimeScale[ind] = new Vector3(cooldownDisplay.transform.localScale.x,
            //     timeLeft / utilities[ind].cd, cooldownDisplay.transform.localScale.z);
            // Change cooldown bar sprites bia ItemUIManager here
            UI_Tile_Manager.UpdateCooldownBar(utilities[ind], (timeLeft / utilities[ind].cd));
            yield return null;
        }
        // dispTimeScale[ind] = new Vector3(cooldownDisplay.transform.localScale.x,
        //         0, cooldownDisplay.transform.localScale.z);
        UI_Tile_Manager.UpdateCooldownBar(utilities[ind], 0);
        onCd[ind] = false;
    }
}
