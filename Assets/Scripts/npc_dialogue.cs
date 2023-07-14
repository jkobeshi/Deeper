using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc_dialogue : MonoBehaviour
{
    public List<string> dialogue = new List<string>();
    public int index = 0;
    public Text dialogue_text;
    public bool dialogue_finished;

    [SerializeField] GameObject UpgradeObject;
    bool HasGivenUpgrade = false;
    // Start is called before the first frame update

    void Start(){
        dialogue_text = gameObject.GetComponent<Text>();
        dialogue_finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            index ++;
            if (index == dialogue.Count){
                index = 0;
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            index = index % dialogue.Count;
        }
        if (index == dialogue.Count - 1 && !HasGivenUpgrade) {
            if (UpgradeObject != null) {
                if (UpgradeObject.name == "Ending Choice"){
                    Instantiate(UpgradeObject, new Vector3(9, -194, 0), Quaternion.identity);
                }
                else{
                    Instantiate(UpgradeObject, GameObject.Find("Player").GetComponent<Transform>().position, Quaternion.identity);
                }
                HasGivenUpgrade = true;
            }
            else {
                Debug.Log("Did not assign an upgrade object to instantiate");
            }
            dialogue_finished = true;

        }
        dialogue_text.text = dialogue[index];
    }
}
