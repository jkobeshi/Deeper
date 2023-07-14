using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenIndicator : MonoBehaviour
{
    public GameObject Indicator;
    public GameObject Target;
    int layer_mask;

    Renderer rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Renderer>();
        Target = GameObject.Find("Player");
        layer_mask = LayerMask.GetMask("BoundaryBox");
    }

    // Update is called once per frame
    void Update()
    {
        if (!rd.isVisible) {
            if (Target != null) {
                if (!Indicator.activeSelf) {
                    Indicator.SetActive(true);
                }

                Vector2 direction = Target.transform.position - transform.position;

                RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layer_mask);

                if (ray.collider != null) {
                    Indicator.transform.position = ray.point;
                }
            }
        }
        else {
            if (Indicator.activeSelf) {
                Indicator.SetActive(false);
            }
        }
    }
}
