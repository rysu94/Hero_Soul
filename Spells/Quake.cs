using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quake : MonoBehaviour
{

    public GameObject cameraObject;
    public List<GameObject> enemies = new List<GameObject>();


    // Use this for initialization
    void Start ()
    {
        cameraObject = GameObject.Find("Main Camera");
        StartCoroutine(QuakeRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator QuakeRoutine()
    {
        StartCoroutine(ShakeScreen(15f));
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            UpdateEnemies();
            if (enemies.Count > 0 && i % 2 == 0)
            {
                int enemyIndex = Random.Range(0, enemies.Count);
                float x = enemies[enemyIndex].transform.position.x;
                float y = enemies[enemyIndex].transform.position.y;
                Instantiate(Resources.Load("Prefabs/SpellFX/Rockslide"), new Vector2(x, y), Quaternion.identity);
            }
            else
            {
                float x = Random.Range(-2.3f, 2.3f);
                float y = Random.Range(-1.3f, 1f);
                Instantiate(Resources.Load("Prefabs/SpellFX/Rockslide"), new Vector2(x, y), Quaternion.identity);
            }


        }
    }

    void UpdateEnemies()
    {
        enemies.Clear();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
        }
    }

    IEnumerator ShakeScreen(float duration)
    {
        yield return new WaitForSeconds(.5f);
        float shakeDuration = duration;
        Vector2 originalPos = cameraObject.transform.position;
        while (shakeDuration > 0)
        {
            shakeDuration -= .01f;
            Vector2 newPos = new Vector2(originalPos.x + (Random.insideUnitCircle.x * .04f), originalPos.y);
            cameraObject.transform.position = new Vector3(newPos.x, newPos.y, -10);
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(.01f);
        cameraObject.transform.position = new Vector3(originalPos.x, originalPos.y, -10);
    }
}
