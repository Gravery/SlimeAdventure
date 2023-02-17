using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public Life playerLife;
    List<HeartDisplay> hearts = new List<HeartDisplay>();


    void Start()
    {
        DrawHearts();
    }


    public void DrawHearts()
    {
        ClearHearts();
        //Debug.Log(playerLife.GetMaxHealth());
        float maxHealthRemainder = playerLife.GetMaxHealth() % 2;
        int heartsToMake = (int)((playerLife.GetMaxHealth() / 2) + maxHealthRemainder);
        //Debug.Log(heartsToMake);


        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerLife.GetHealth() - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HeartDisplay heartComponent = newHeart.GetComponent<HeartDisplay>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform )
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartDisplay>();
    }
}
