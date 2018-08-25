using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Orbit : MonoBehaviour
{
    //This spell effector causes a projectile to home in on the closest target
    GameObject selectedEnemy;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(HomingRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator HomingRoutine()
    {
        //Small delay before the effect takes place
        //Determine the closest enemy
        SelectNewTarget();
        yield return new WaitForSeconds(.5f);
        while (true)
        {
            if (selectedEnemy != null)
            {
                //Rotate the projectile until it faces the enemy
                Vector3 targetRot = selectedEnemy.transform.position;
                targetRot.z = 0;

                Vector3 objPos = transform.position;
                targetRot.x = targetRot.x - objPos.x;
                targetRot.y = targetRot.y - objPos.y;

                float angle = Mathf.Atan2(targetRot.y, targetRot.x) * Mathf.Rad2Deg;
                Quaternion newRot = Quaternion.Euler(new Vector3(0, 0, angle));

                transform.rotation = Quaternion.Lerp(transform.rotation, newRot, .25f);

                //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if (!selectedEnemy.activeInHierarchy)
                {
                    SelectNewTarget();
                }

            }
            yield return new WaitForSeconds(.05f);

        }
    }

    void SelectNewTarget()
    {
        //Determine the closest enemy
        List<GameObject> enemyList = new List<GameObject>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Player"))
        {
            enemyList.Add(enemy);
        }

        float minDistance = 1000;

        for (int i = 0; i < enemyList.Count; i++)
        {
            float distance = Vector2.Distance(enemyList[i].transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                selectedEnemy = enemyList[i];
            }
        }

        if (enemyList.Count < 1)
        {
            selectedEnemy = null;
        }

    }
}
