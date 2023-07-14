using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class UpdateGraph : MonoBehaviour
{
    public static UpdateGraph instance;
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
    public void updtGraph()
    {
        AstarPath.active.Scan();
    }
}
