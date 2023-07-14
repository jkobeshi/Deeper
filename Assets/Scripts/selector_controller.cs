using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class selector_controller : MonoBehaviour
{
    public static selector_controller instance;
    public int current_index = 0;
    public Canvas canvas;
    public VideoPlayer player;
    public AudioClip param_change;
    public AudioClip select_audio;
    [SerializeField] GameObject Item_Slots_Container;
    bool UIBool = true;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Item_Slots_Container = GameObject.Find("Canvas/Item_Slots");
        StartCoroutine(selector_blink());
    }
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Item_Slots_Container.SetActive(!UIBool);
            UIBool = !UIBool;
        }
        if (PlayerMovement.instance.shop_active && (PlayerMovement.instance.clones.Count > 0))
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetAxis("Mouse ScrollWheel") < 0f))
            {
                AudioSource.PlayClipAtPoint(param_change, Camera.main.transform.position, .25f * GameControl.sound_effects_vol);
                current_index = (current_index + 1) % PlayerMovement.instance.clones.Count;
                gameObject.GetComponent<Canvas>().GetComponent<RectTransform>().localPosition = new Vector3(-80, (current_index * -50) + 5);
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetAxis("Mouse ScrollWheel") > 0f))
            {
                AudioSource.PlayClipAtPoint(param_change, Camera.main.transform.position, .25f * GameControl.sound_effects_vol);
                if (current_index == 0)
                    current_index = PlayerMovement.instance.clones.Count - 1;
                else
                    current_index -= 1;
                gameObject.GetComponent<Canvas>().GetComponent<RectTransform>().localPosition = new Vector3(-80, (current_index * -50) + 5);
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (inventory.instance.cash >= inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index].cost)
                {
                    // if buying an item for the first time, activate the utility UI elements
                    if (inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index].type != 'u') {
                        if (!inventory.instance.firstItemAcquired && UIBool) {
                            inventory.instance.firstItemAcquired = true;
                            Item_Slots_Container.SetActive(true);
                        }
                    }
                    if (PlayerMovement.instance.clones[current_index].GetComponent<Text>().color != Color.red)
                    {
                        AudioSource.PlayClipAtPoint(select_audio, Camera.main.transform.position, .25f * GameControl.sound_effects_vol);
                        inventory.instance.Quant[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index] += 1;
                        PlayerMovement.instance.inShop.GetComponent<Shop>().QuantBought[PlayerMovement.instance.relateToShop[current_index]] += 1;
                        inventory.instance.cash -= inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index].cost;
                        if (inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index].type != 'u')
                        {
                            inventory.instance.current_index = PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index;
                        }
                        Item_Slots_Container.GetComponent<ItemUIManager>().PurchaseItem(inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index]);
                        Item_Slots_Container.GetComponent<ItemUIManager>().UpdateQuantityAfterUse(inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index], inventory.instance.Quant[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index]);
                        if (((inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index].type == 'u') &&
                            ((PlayerMovement.instance.inShop.GetComponent<Shop>().MaxQuant[PlayerMovement.instance.relateToShop[current_index]] <=
                            PlayerMovement.instance.inShop.GetComponent<Shop>().QuantBought[PlayerMovement.instance.relateToShop[current_index]])
                            || (inventory.instance.NumBoughtForUpgrades[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index] >=
                            inventory.instance.MaxBoughtForUpgrades[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index]))) ||
                            ((inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index].type == 'p') &&
                            inventory.instance.Quant[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index] > 0 ))
                        {
                            PlayerMovement.instance.clones[current_index].GetComponent<Text>().color = Color.red;
                        }
                    }
                }
            }
            player.clip = inventory.instance.utilities[PlayerMovement.instance.clones[current_index].GetComponent<InvenIndex>().index].tutorial_vid;
        }
    }

    IEnumerator selector_blink(){
        while (true){
            gameObject.GetComponent<Image>().color = new Color(0,0,1,-1);
            yield return new WaitForSeconds(.5f);
            gameObject.GetComponent<Image>().color = new Color(0,0,1,1);
            yield return new WaitForSeconds(.5f);
        }
    }
}
