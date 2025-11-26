using UnityEngine;
using System.Collections.Generic;

public class BabyDucksCode : MonoBehaviour
{
    [Header("Setup")]
    public GameObject babyDuckPrefab;
    public Transform motherDuck;
    [Header("Follow Settings")]
    public float followSpeed = 5f;
    public float followDistance = 1f;

    private List<Transform> babyDucks = new List<Transform>(); //container of all ducks
    private int duckCount = 0;

    public void addDuck()
    {
        duckCount++;
        Debug.Log("Baby duck added! Total: " + duckCount);

        GameObject newDuck = Instantiate(babyDuckPrefab);
        Transform duckTransform = newDuck.transform;

        duckTransform.position = motherDuck.position - motherDuck.up * (1f + duckCount);
        babyDucks.Add(duckTransform);
    }

    public void removeAllDucks()
    {
        foreach (Transform duck in babyDucks) //advanced for loop
            Destroy(duck.gameObject);

        babyDucks.Clear(); //clears the list
        duckCount = 0;
    }

        public void SetDuckColor(Color color)
    {
        if (babyDuckPrefab != null)
        {
            var r = babyDuckPrefab.GetComponent<SpriteRenderer>();
            if (r != null)
                r.color = color;
        }

        foreach (Transform duck in babyDucks)
        {
            var r = duck.GetComponent<SpriteRenderer>();
            if (r != null)
                r.color = color;
        }
    }


    void Update()
    {
        FollowChain();
    }

    private void FollowChain()
    {
        if (babyDucks.Count == 0) return;

        for (int i = 0; i < babyDucks.Count; i++)
        {
            Transform duck = babyDucks[i];
            Transform target = (i == 0) ? motherDuck : babyDucks[i - 1];
            Vector3 desiredPos = target.position - target.up * followDistance;

            duck.position = Vector3.Lerp(duck.position, desiredPos, followSpeed * Time.deltaTime); //to make the process smoother
            duck.up = Vector3.Lerp(duck.up, target.up, followSpeed * Time.deltaTime);
        }
    }

    public void removeDuck(Transform duckTransform)
    {
        if (babyDucks.Contains(duckTransform))
        {
            babyDucks.Remove(duckTransform);
            duckCount--;
        }

        if (duckTransform != null)
            Destroy(duckTransform.gameObject);
    }
}
