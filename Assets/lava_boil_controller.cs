using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava_boil_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(manager());
    }

    // Update is called once per frame
    private IEnumerator manager(){
        while(true){
            int num_boiling = Random.Range(1, 4);
            for (int i = 0; i < num_boiling; i++){
                StartCoroutine(gameObject.transform.GetChild(Random.Range(0, 30)).gameObject.GetComponent<lava_boil>().boil());
            }
            int random_time_wait = Random.Range(2, 5);
            yield return new WaitForSeconds(random_time_wait);
        }
    }
}
