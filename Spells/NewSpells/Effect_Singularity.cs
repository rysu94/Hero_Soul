using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Singularity : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Singularity());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator Singularity()
    {
        Instantiate(Resources.Load("Prefabs/Particle/Singularity_Effect"), transform);
        while (true)
        {
            enemyList.Clear();
            Vector2 pos = transform.position;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {

                if (Vector2.Distance(pos, enemy.transform.position) <= 1.5f)
                {
                    enemyList.Add(enemy);
                }
            }

            for (int i = 0; i < enemyList.Count; i++)
            {
                Vector2 enemyPos = enemyList[i].transform.position;
                Vector2 newPos = (pos - enemyPos).normalized * .02f;

                enemyList[i].transform.position = (new Vector2(enemyPos.x + newPos.x, enemyPos.y + newPos.y));


            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
