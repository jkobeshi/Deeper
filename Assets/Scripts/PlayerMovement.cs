using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    Rigidbody2D rb;
    float movespeed = 4f;
    float jumpHeight = 1.5f;
    float maxSpeed = 5f;
    public Vector2 checkpoint;
    public bool canJump = true;
    public bool resMov = false;
    public string direction = "";
    int SolidLayer = 1 << 3;
    public ParticleSystem winPart;
    public Canvas shopUIParentCanvas;
    public bool shop_enabled = false;
    public bool shop_active = false;
    public GameObject settings_button;
    public List<GameObject> clones = new List<GameObject>();
    public AudioClip shop_start;
    public AudioClip shop_stop;
    public GameObject ShopItemClone;
    public GameObject inShop;
    private bool on_cd = false;
    public List<int> relateToShop = new List<int>();
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
        checkpoint = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GameObject.FindGameObjectWithTag("EngineHum").GetComponent<AudioSource>().volume = .1f * GameControl.sound_effects_vol;
        if (Input.GetKeyDown(KeyCode.F) && shop_enabled && !shop_active){
            resMov = true;
            selector_controller.instance.current_index = 0;
            selector_controller.instance.gameObject.GetComponent<Canvas>().GetComponent<RectTransform>().localPosition = 
                new Vector3(-80, (selector_controller.instance.current_index * -50) + 5);
            settings_button.SetActive(false);
            shopUIParentCanvas.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            AudioSource.PlayClipAtPoint(shop_start, Camera.main.transform.position, .75f * GameControl.sound_effects_vol);
            shop_active = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && shop_active)
        {
            settings_button.SetActive(true);
            AudioSource.PlayClipAtPoint(shop_stop, Camera.main.transform.position, .75f * GameControl.sound_effects_vol);
            shop_active = false;
            shopUIParentCanvas.GetComponent<RectTransform>().localPosition = new Vector3(0, 5000, 0);
            resMov = false;
        }
        RaycastHit2D drHit = Physics2D.Raycast(transform.position + new Vector3(0.465f, 0, 0), Vector2.down, 0.51f, SolidLayer);
        RaycastHit2D dlHit = Physics2D.Raycast(transform.position + new Vector3(-0.465f, 0, 0), Vector2.down, 0.51f, SolidLayer);
        RaycastHit2D urHit = Physics2D.Raycast(transform.position + new Vector3(0.465f, 0, 0), Vector2.up, 1f, SolidLayer);
        RaycastHit2D ulHit = Physics2D.Raycast(transform.position + new Vector3(-0.465f, 0, 0), Vector2.up, 1f, SolidLayer);
        if ((drHit.collider != null || dlHit.collider != null) && (urHit.collider == null && ulHit.collider == null))
            canJump = true;
        else
            canJump = false;
        if (!resMov)
            Moving();
    }

    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal")
        {
            Destroy(other.gameObject); Win();
        }
        if (other.tag == "Shop"){
            other.gameObject.GetComponent<PromptPlayer>().showPrompt();
            shop_enabled = true; inShop = other.gameObject;
            List<int> itemsNShop = other.gameObject.GetComponent<Shop>().ItemsInShop;
            int ri = 0;
            for (int j = 0; j < 3; ++j)
            {
                for (int i = 0; i < itemsNShop.Count; ++i)
                {
                    if (((inventory.instance.utilities[itemsNShop[i]].type == 'p') && (j == 0)) ||
                        ((inventory.instance.utilities[itemsNShop[i]].type == 'u') && (j == 1)) ||
                        ((inventory.instance.utilities[itemsNShop[i]].type == 'q') && (j == 2)))
                    {
                        relateToShop.Add(i);
                        clones.Add(Instantiate(ShopItemClone, GameObject.FindGameObjectWithTag("UpgradeObject").transform.position
                            , Quaternion.identity, GameObject.FindGameObjectWithTag("UpgradeObject").transform));
                        clones[ri].GetComponent<Canvas>().GetComponent<RectTransform>().localPosition = new Vector3(-220f, ri * -50, 0);
                        clones[ri].GetComponent<InvenIndex>().index = itemsNShop[i];
                        if (((inventory.instance.utilities[clones[ri].GetComponent<InvenIndex>().index].type == 'u') 
                            && ((other.GetComponent<Shop>().MaxQuant[i] <= other.GetComponent<Shop>().QuantBought[i])
                            || inventory.instance.NumBoughtForUpgrades[clones[ri].GetComponent<InvenIndex>().index] >=
                            inventory.instance.MaxBoughtForUpgrades[clones[ri].GetComponent<InvenIndex>().index])) ||
                            ((inventory.instance.utilities[clones[ri].GetComponent<InvenIndex>().index].type == 'p') && inventory.instance.Quant[itemsNShop[i]] > 0))
                        {
                            clones[ri].GetComponent<Text>().color = Color.red;
                        }
                        ++ri;
                    }
                }
            }
        }
        if (other.tag == "SafeZone")
        {
            checkpoint = other.gameObject.transform.position;
        }
        if (other.gameObject.name == "enemy_light"){
            other.gameObject.GetComponentInParent<bot_movement>().outofsight = false;
            other.gameObject.GetComponent<Light>().intensity = 35f;
        }
        if (other.gameObject.name == "Mine Radius"){
            StartCoroutine(other.gameObject.GetComponent<mine_explod>().explode());
        }
    }
    void OnTriggerStay2D(Collider2D coll){
        if (coll.gameObject.name == "Mine Radius" && coll.gameObject.GetComponent<mine_explod>().exploded){
            if (on_cd == false){
                StartCoroutine(cooldown());
                if (inventory.instance != null){
                GameControl.instance.HandleDeathAndReset("Mine");
                }
            }
        }
    }

    private IEnumerator cooldown(){
        on_cd = true;
        yield return new WaitForSeconds(1);
        on_cd = false;
    }
    private async void OnTriggerExit2D(Collider2D collision)
    {
        if((collision.tag == "Shop"))
        {
            collision.gameObject.GetComponent<PromptPlayer>().destroyPrompt();
            shop_enabled = false;
            for(int i = 0; i < clones.Count; ++i)
            {
                Destroy(clones[i]);
            } clones.Clear();
            relateToShop.Clear();
        }
        if (collision.gameObject.name == "enemy_light"){
            collision.gameObject.GetComponentInParent<bot_movement>().outofsight = true;
            collision.gameObject.GetComponent<Light>().intensity = 10f;
        }
    }
    void Win()
    {
        resMov = true;
        Instantiate(winPart, transform.position, transform.rotation);
        gameObject.GetComponentInChildren<light_dimmer>().in_light = true;
        GameObject.FindGameObjectWithTag("light_upgrade").GetComponent<SpriteRenderer>().enabled =false;
        gameObject.GetComponentInChildren<Light>().enabled = false;
        StartCoroutine(ending());
    }

    private IEnumerator ending(){
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("destroy_on_diamond")){
            GameObject.Destroy(temp);
        }
        yield return new WaitForSeconds(3f);
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("build_on_diamond")){
            temp.GetComponent<BoxCollider2D>().enabled = true;
        }
        resMov = false;
    }

    void Moving()
    {
        Vector2 vel = rb.velocity;
        RaycastHit2D tHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.465f, 0), new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.51f, SolidLayer);
        RaycastHit2D dHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.465f, 0), new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.51f, SolidLayer);
        RaycastHit2D mHit = Physics2D.Raycast(transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.51f, SolidLayer);
        if (Input.GetAxisRaw("Horizontal") > 0){
            direction = "east";
        }
        else if (Input.GetAxisRaw("Horizontal") < 0){
            direction = "west";
        }
        if (Input.GetAxisRaw("Vertical") > 0){
            direction = "north";
        }
        else if (Input.GetAxisRaw("Vertical") < 0){
            direction = "south";
        }
        if ((Input.GetAxisRaw("Horizontal") != 0) && (tHit.collider == null) && (mHit.collider == null) && (dHit.collider == null))
            vel.x = Input.GetAxisRaw("Horizontal") * movespeed;
        /*
        if (((Input.GetAxisRaw("Vertical") > 0)) && canJump && (Input.GetAxisRaw("Horizontal") == 0))
        {
            canJump = false;
            vel.y = movespeed * jumpHeight;
        }*/
        rb.velocity = vel;
    }
}
