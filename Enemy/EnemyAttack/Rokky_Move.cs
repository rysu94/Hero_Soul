using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rokky_Move : MonoBehaviour
{
    public float speed = 0;
    public bool isTriggered = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float x = -transform.up.x * speed;
        float y = -transform.up.y * speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Wall" && !isTriggered)
        {
            isTriggered = true;
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(Resources.Load("Prefabs/Enemies/Pebble"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount += 1;
            Destroy(gameObject);
        }
        else if(collider.tag == "Player" && !isTriggered)
        {
            isTriggered = true;
            DamageManager.PlayerDamage(30, TestCharController.player.gameObject, false);
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            //Play the player's getting hurt SFX
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
            Destroy(gameObject);
        }
    }
}
