using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnClick : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] int count;
    const int distanceToTake = 4;
    PlayerInventory inventory;
    public int ID => id;
    public int Count => count;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if(Vector3.Distance(transform.position, inventory.transform.position) < distanceToTake)
        {
            var wasTaken = inventory.PutToInventory(ID, count);
            if(wasTaken) Destroy(gameObject);
        }
    }
}
