using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune_Puzzle : MonoBehaviour
{

    public List<GameObject> runes = new List<GameObject>();
    public List<int> runeCombo = new List<int>();

    public int currentIndex = 0;

    public bool cleared = false;

    public GameObject error;

	// Use this for initialization
	void Start ()
    {
        currentIndex = 0;

		while(runeCombo.Count < 4)
        {
            int tempInt = Random.Range(0, 4);
            if(!runeCombo.Contains(tempInt))
            {
                runeCombo.Add(tempInt);
                print(tempInt);
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (currentIndex == 4 && !cleared)
        {
            cleared = true;
            StartCoroutine(DoneRoutine());
        }
    }

    public void DeactivateRunes()
    {
        currentIndex = 0;
        for(int i = 0; i < runes.Count; i++)
        {
            runes[i].GetComponent<Rune_Controller>().DeactivateRune();
        }
    }

    IEnumerator DoneRoutine()
    {
        yield return new WaitForSeconds(1f);
        DeactivateRunes();

        for(int i = 0; i < runeCombo.Count; i++)
        {
            runes[runeCombo[i]].GetComponent<AudioSource>().Play();
            runes[runeCombo[i]].GetComponent<Rune_Controller>().glow.SetActive(true);
            runes[runeCombo[i]].GetComponent<Rune_Controller>().particles.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);
        //Create a rune spawner
        GameObject.Find("Rune_Tile").transform.Find("Discovered_Rune").GetComponent<Animator>().Play("Rune_Hover");

        yield return new WaitForSeconds(6f);
        GameObject.Find("Rune_Tile").AddComponent<Rune_Pedastal_Controller>();


        /*  opens sealed doors
        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount--;
        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount <= 0 && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
        {
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear = true;
            GetComponent<AudioSource>().Play();
        }
        */
    }
}
