using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public bool Reloading = false;
    public static GameControl instance;
    public bool gameOver = false;
    bool handlingDeath = false;
    static public float sound_effects_vol = 1f;
    static public float screen_shake_mult = 1f;
    public GameObject scene_change_object;
    public bool Win = false;
    public int shield_health = 0;
    public int shield_max = 0;
    public ParticleSystem blood;
    public AudioClip shield_break;
    public GameObject mine_prefab;
    public GameObject mine_two_prefab;
    public GameObject battery_prefab;
    public GameObject CashPickup;
    static public List <Vector3> mines_to_respawn = new List<Vector3>();
    static public List <Vector3> boss_mines_to_respawn = new List<Vector3>();
    static public List<Vector3> batteries_to_respawn = new List<Vector3>();
    [SerializeField] GameObject DeathOverlayCanvas;
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
        DontDestroyOnLoad(this.gameObject);
        //Cursor.visible = false;
    }
    void Update()
    {
        // if (gameOver && !handlingDeath)
        // {
        //     handlingDeath = true;
        //     StartCoroutine(KillnReset());
        // }
        if (GameObject.Find("Scene Change") != null){
            scene_change_object = GameObject.Find("Scene Change");
        }
        if (Win) {
            Win = false;
            StartCoroutine(WinLevel());
        }
    }

    public void HandleDeathAndReset(string killedBy) {
        if (!handlingDeath) {
            if (shield_health == 0 || (killedBy == "Running out of light"))
            {
                gameOver = true;
                handlingDeath = true;
                GameObject.Instantiate(CashPickup, PlayerMovement.instance.gameObject.transform.position, Quaternion.identity);
                StartCoroutine(KillnReset(killedBy));
            }
            else
            {
                AudioSource.PlayClipAtPoint(shield_break, Camera.main.transform.position, GameControl.sound_effects_vol);
                shield_health -= 1;
            }
        }
    }

    public IEnumerator StartGame(){
        int total_blocks = 0;
        while (true){
            int how_many_to_spawn = Random.Range(1, 8);
            if (total_blocks + how_many_to_spawn > 95){
                how_many_to_spawn = 96 - total_blocks;
            }
            total_blocks += how_many_to_spawn;
            for (int i = 0; i < how_many_to_spawn; i++){
                while (true){
                    int random_block = Random.Range(0, 96);
                    if (!scene_change_object.transform.GetChild(random_block).gameObject.activeSelf){
                        scene_change_object.transform.GetChild(random_block).gameObject.SetActive(true);
                        scene_change_object.transform.GetChild(random_block).gameObject.GetComponent<random_sprite_non_flicker>().randomize();
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(.03f);
            if (total_blocks > 95){
                break;
            }
        }
        Reloading = true;
        SceneManager.LoadScene("main");
        total_blocks = 0;
        while (true){
            int how_many_to_spawn = Random.Range(1, 8);
            if (total_blocks + how_many_to_spawn > 95){
                how_many_to_spawn = 96 - total_blocks;
            }
            total_blocks += how_many_to_spawn;
            for (int i = 0; i < how_many_to_spawn; i++){
                while (true){
                    int random_block = Random.Range(0, 96);
                    if (scene_change_object.transform.GetChild(random_block).gameObject.activeSelf){
                        scene_change_object.transform.GetChild(random_block).gameObject.SetActive(false);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(.03f);
            if (total_blocks > 95){
                break;
            }
        }      
    }

    IEnumerator KillnReset(string killedBy)
    {
        GameObject.FindGameObjectWithTag("WormHead").GetComponent<HeadDamage>().health = 3;
        ParticleSystem clone;
        PlayerMovement.instance.resMov = true;
        clone = GameObject.Instantiate(blood, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
        yield return new WaitForSeconds(0.1f);
        Destroy(clone);
        GameObject DeathOverlay = Instantiate(DeathOverlayCanvas, GameObject.Find("Canvas").transform);
        DeathOverlay.gameObject.GetComponent<DeathOverlayController>().ConfigureOverlay((int)inventory.instance.cash, (int)Mathf.Round(inventory.instance.cash / 2), killedBy);
        inventory.instance.cash = Mathf.Round(inventory.instance.cash /= 2);
        while (true) {
            if (Input.GetKeyDown(KeyCode.E)) {
                break;
            }
            yield return null;
        }
        int total_blocks = 0;
        while (true){
            int how_many_to_spawn = Random.Range(1, 8);
            if (total_blocks + how_many_to_spawn > 95){
                Debug.Log("heres the bug");
                how_many_to_spawn = 96 - total_blocks;
            }
            total_blocks += how_many_to_spawn;
            for (int i = 0; i < how_many_to_spawn; i++){
                while (true){
                    int random_block = Random.Range(0, 96);
                    if (!scene_change_object.transform.GetChild(random_block).gameObject.activeSelf){
                        scene_change_object.transform.GetChild(random_block).gameObject.SetActive(true);
                        scene_change_object.transform.GetChild(random_block).gameObject.GetComponent<random_sprite_non_flicker>().randomize();
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(.03f);
            if (total_blocks > 95){
                break;
            }
        }
        
        Destroy(DeathOverlay);
        PlayerMovement.instance.resMov = false;
        gameOver = false;
        handlingDeath = false;
        PlayerMovement.instance.gameObject.transform.position = PlayerMovement.instance.checkpoint;
        for (int i = 0; i < mines_to_respawn.Count; i++){
            GameObject.Instantiate(mine_prefab, mines_to_respawn[i], Quaternion.identity);
        }
        mines_to_respawn.Clear();
        for (int i = 0; i < boss_mines_to_respawn.Count; i++){
            GameObject.Instantiate(mine_two_prefab, boss_mines_to_respawn[i], Quaternion.identity);
        }
        boss_mines_to_respawn.Clear();
        for (int i = 0; i < batteries_to_respawn.Count; i++)
        {
            GameObject.Instantiate(battery_prefab, batteries_to_respawn[i], Quaternion.identity);
        }
        batteries_to_respawn.Clear();
        if (inventory.instance.shieldBought)
        {
            shield_health = shield_max;
        }
        total_blocks = 0;
        while (true){
            int how_many_to_spawn = Random.Range(1, 8);
            if (total_blocks + how_many_to_spawn > 95){
                Debug.Log("heres the bug");
                how_many_to_spawn = 96 - total_blocks;
            }
            total_blocks += how_many_to_spawn;
            for (int i = 0; i < how_many_to_spawn; i++){
                while (true){
                    int random_block = Random.Range(0, 96);
                    if (scene_change_object.transform.GetChild(random_block).gameObject.activeSelf){
                        scene_change_object.transform.GetChild(random_block).gameObject.SetActive(false);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(.03f);
            if (total_blocks > 95){
                break;
            }
        }        
    }
    public IEnumerator WinLevel()
    {
        Reloading = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 2);
    }
}
