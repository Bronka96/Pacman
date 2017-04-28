using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour {

    public List<GameObject> bonusList;

    void Start()
    {
        for (int i = 0; i < bonusList.Count; i++)
        {
            bonusList[i] = Instantiate(bonusList[i]);
            bonusList[i].SetActive(false);
        }
    }

    public GameObject GetBonus()
    {
        for (int i = 0; i < bonusList.Count; i++)
        {
            if (!bonusList[i].activeInHierarchy)
                return bonusList[i];
        }
        GameObject bonus = (GameObject)Instantiate(bonusList[Random.Range(0, bonusList.Count)]);
        bonus.SetActive(false);
        bonusList.Add(bonus);
        return bonus;
    }
}
