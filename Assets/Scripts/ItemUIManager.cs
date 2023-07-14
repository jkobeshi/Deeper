using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour
{
    public class Item_Slot {
        public Item_Slot(inventory.Item item = null, GameObject UI_tile = null, bool empty = true/*, bool equipped = false*/) {
            this.item = item;
            this.UI_tile = UI_tile;
            this.empty = empty;
            // this.equipped = equipped;
        }
        public inventory.Item item;
        public GameObject UI_tile;
        public bool empty;
        // public bool equipped;

        public void UpdateIcon() {
            this.UI_tile.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            this.UI_tile.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = this.item.icon;
        }

        public void UpdateName(int quantity = 0) {
            this.UI_tile.transform.Find("UtilityDisplayer").gameObject.GetComponent<Text>().text = this.item.name;
            if (this.item.type == 'q') {
                this.UI_tile.transform.Find("UtilityDisplayer").gameObject.GetComponent<Text>().text += ("\nQty: " + quantity);
            }
        }
    }
    [SerializeField] int num_item_slots = 5;
    [SerializeField] List<Item_Slot> item_array;
    [SerializeField] GameObject Item_Slots_Container;
    // [SerializeField] GameObject Active_Item_Tile;
    // int currently_equipped_index = -1;

    // Start is called before the first frame update
    private void Start() {
        Item_Slots_Container = GameObject.Find("Canvas/Item_Slots");
        // Active_Item_Tile = GameObject.Find("Canvas/Item_Slots/Active_Item_Slot");
        item_array = new List<Item_Slot>();
        // Initialize with empty item slots
        for (int i = 0; i < num_item_slots; ++i) {
            item_array.Add(new Item_Slot());
            item_array[i].UI_tile = Item_Slots_Container.transform.Find("Slot_" + (i + 1) + "_tile").gameObject;
        }
        gameObject.SetActive(false);
    }

    public void PurchaseItem(inventory.Item item) {
        // Find type of item
        if (item.type != 'u') {
            // See if we already have this item, if not add it to next open slot
            if (!IsItemPurchased(item)) {
                // Add icon
                int next_open_index = NextAvailableIndex();
                if (next_open_index == -1) {
                    Debug.Log("no free slots?");
                }
                else {
                    item_array[next_open_index].item = item;
                    item_array[next_open_index].empty = false;
                    item_array[next_open_index].UpdateIcon();
                    item_array[next_open_index].UpdateName(1);
                    // SwitchToItem(item);
                }
            }
        }
    }

    // This switches to the item in the tile underneath the currently equipped item
    // public string SwitchToNextVisibleIndex() {
    //     // Check if we an item available at another index
    //     int index_to_access;
    //     for (int i = 0; i < num_item_slots; ++i) {
    //         // Start looking at the index ahead of the current, hence + 1
    //         index_to_access = (i + currently_equipped_index + 1) % num_item_slots;
    //         if (!item_array[index_to_access].empty) {
    //             SwitchToIndex(index_to_access);
    //             return item_array[index_to_access].item.name;
    //         }
    //     }
    //     return "";
    // }

    // This switches to the item in the tile above the currently equipped item
    // public string SwitchToPreviousVisibleIndex() {
    //     // Check if we an item available at another index
    //     int index_to_access;
    //     for (int i = 0; i < num_item_slots; ++i) {
    //         // Start looking at the index ahead of the current, hence + 1
    //         index_to_access = (int)custom_modulo_because_c_sharp_sucks((currently_equipped_index - i - 1), num_item_slots);
    //         if (!item_array[index_to_access].empty) {
    //             SwitchToIndex(index_to_access);
    //             return item_array[index_to_access].item.name;
    //         }
    //     }
    //     return "";
    // }

    float custom_modulo_because_c_sharp_sucks(float a, float b) {
        return a - b * Mathf.Floor(a / b); // ???
    }

    public string GetItemNameAtIndex(int index) {
        if (!item_array[index].empty) {

        }
        return item_array[index].item.name;
    }
    // This switches to the item in the given index slot
    // public string SwitchToIndex(int index) {
    //     // Check if we have item at this index
    //     if (!item_array[index].empty) {
    //         if (currently_equipped_index > -1) {
    //             item_array[currently_equipped_index].equipped = false;
    //         }
    //         // DisplayActiveItemIcon(item_array[index].item);
    //         currently_equipped_index = index;
    //         item_array[currently_equipped_index].equipped = true;
    //         DisplayEquippedBorder(index);
    //         return item_array[currently_equipped_index].item.name;
    //     }
    //     else {
    //         Debug.Log("Trying to switch to index that has no item in it");
    //         return "";
    //     }
    // }

    // This equips an item directly given its name
    // public void SwitchToItem(inventory.Item item) {
    //     // Check if we have item
    //     if (IsItemPurchased(item)) {
    //         if (currently_equipped_index > -1) {
    //             item_array[currently_equipped_index].equipped = false;
    //         }
    //         // DisplayActiveItemIcon(item);
    //         currently_equipped_index = GetItemIndex(item);
    //         item_array[currently_equipped_index].equipped = true;
    //         DisplayEquippedBorder(currently_equipped_index);
    //     }
    //     else {
    //         Debug.Log("Not purchased");
    //     }
    // }

    public void UpdateCooldownBar(inventory.Item item, float percentage) {
        int item_slot_index = GetItemIndex(item);
        if (item_slot_index > -1) {
            GameObject cooldown_bar = item_array[item_slot_index].UI_tile.transform.Find("CooldownBar").gameObject;
            cooldown_bar.transform.localScale = new Vector3(cooldown_bar.transform.localScale.x, percentage, cooldown_bar.transform.localScale.z);
        }
    }

    public void UpdateQuantityAfterUse(inventory.Item item, int quantity) {
        int item_index = GetItemIndex(item);
        if (item_index > -1) {
            if (!item_array[item_index].empty) {
                Debug.Log("We made it!");
                item_array[item_index].UpdateName(quantity);
            }
            else {
                Debug.Log("Also bad?");
            }
        }
        else {
            Debug.Log("Bad!!");
        }
    }

    private bool IsItemPurchased(inventory.Item item) {
        foreach (Item_Slot item_slot in item_array) {
            if (!item_slot.empty && item_slot.item.name == item.name) {
                return true;
            }
        }
        return false;
    }

    // Returns -1 if item is not purchased
    private int GetItemIndex(inventory.Item item) {
        if (!IsItemPurchased(item)) {
            return -1;
        }
        else {
            for (int i = 0; i < num_item_slots; ++i) {
                if (!item_array[i].empty && item_array[i].item.name == item.name) {
                    return i;
                }
            }
            return -1;
        }
    }

    private int NextAvailableIndex() {
        for (int i = 0; i < num_item_slots; ++i) {
            if (item_array[i].empty) {
                return i;
            }
        }
        return -1;
    }

    // private void DisplayActiveItemIcon(inventory.Item item) {
    //     Active_Item_Tile.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = item.icon;
    // }

    // private void DisplayEquippedBorder(int index) {
    //     foreach(Item_Slot item_slot in item_array) {
    //         item_slot.UI_tile.transform.Find("Border").gameObject.GetComponent<Image>().enabled = false;
    //     }
    //     item_array[currently_equipped_index].UI_tile.transform.Find("Border").gameObject.GetComponent<Image>().enabled = true;
    // }
}
