using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour {

    public bool isCollecting = false;
    public bool active = false;

    public AudioSource item;
    public SpriteRenderer sprite;

    public Rigidbody2D itemRB;

    public int itemID;

	// Use this for initialization
	void Start ()
    {
        item = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(StartRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(active)
        {
            //Item Magnetism
            float distanceToPlayer = Vector3.Distance(transform.position, TestCharController.player.transform.position);
            if (distanceToPlayer <= .65)
            {
                itemRB = GetComponent<Rigidbody2D>();
                Vector2 itemVec = TestCharController.player.transform.position - transform.position;
                itemRB.velocity = (itemVec.normalized) * .15f;
            }
        }
	}

    IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(1f);
        active = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!isCollecting)
            {
                StartCoroutine(CollectItemRoutine());
            }
        }
    }

    IEnumerator CollectItemRoutine()
    {
        isCollecting = true;


        //Arcana Collection
        if (itemID == 1)
        {
            item.Play();
            sprite.color = new Color(1f, 1f, 1f, 0);
            InventoryManager.fireArcana++;
            yield return new WaitForSeconds(.05f);
            print("Picked up " + GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName);
            Destroy(gameObject);
            yield return null;
        }
        else if (itemID == 2)
        {
            item.Play();
            sprite.color = new Color(1f, 1f, 1f, 0);
            InventoryManager.waterArcana++;
            yield return new WaitForSeconds(.05f);
            print("Picked up " + GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName);
            Destroy(gameObject);
            yield return null;
        }
        else if (itemID == 3)
        {
            item.Play();
            sprite.color = new Color(1f, 1f, 1f, 0);
            InventoryManager.earthArcana++;
            yield return new WaitForSeconds(.05f);
            print("Picked up " + GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName);
            Destroy(gameObject);
            yield return null;
        }
        else if (itemID == 4)
        {
            item.Play();
            sprite.color = new Color(1f, 1f, 1f, 0);
            InventoryManager.windArcana++;
            yield return new WaitForSeconds(.05f);
            print("Picked up " + GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName);
            Destroy(gameObject);
            yield return null;
        }
        else if (itemID == 5)
        {
            item.Play();
            sprite.color = new Color(1f, 1f, 1f, 0);
            InventoryManager.lifeArcana++;
            yield return new WaitForSeconds(.05f);
            print("Picked up " + GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName);
            Destroy(gameObject);
            yield return null;
        }

        //Not Arcana Collections
        else if(itemID > 5)
        {
            int itemIndex = InventoryManager.SearchItem(itemID);

            //if the item is found increase quantity by 1
            if (itemIndex != -1 && InventoryManager.playerInventory[itemIndex].stackable && InventoryManager.playerInventory[itemIndex].itemQuantity < 99)
            {
                item.Play();
                sprite.color = new Color(1f, 1f, 1f, 0);
                InventoryManager.playerInventory[itemIndex].itemQuantity++;
                yield return new WaitForSeconds(.05f);
                print("Picked up " + GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName);
                GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                Destroy(gameObject);
                yield return null;
            }


            //if it isnt found, check if there is an empty inv space and add it
            for (int i = 0; i < InventoryManager.playerInventory.Length; i++)
            {
                if (InventoryManager.playerInventory[i].itemName == "")
                {
                    item.Play();
                    sprite.color = new Color(1f, 1f, 1f, 0);
                    InventoryManager.playerInventory[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID];
                    InventoryManager.playerInventory[i].itemQuantity++;
                    yield return new WaitForSeconds(.05f);
                    print("Picked up " + GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName);
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    Destroy(gameObject);
                    yield return null;
                }
            }
        }
        isCollecting = false;
        
        

    }
}
