using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System.Collections;
using System.Collections.Generic;


public class FishCode : MonoBehaviour
{
    public FishSpawner fish_spawner;
    public GameObject game_area;
    public float speed;
    
    void Update()
    {
       if (game_area == null || fish_spawner == null)
            return;
       Move();
    }

    void Move()
    {
        transform.position += transform.up * (Time.deltaTime * speed);

        float distance = Vector3.Distance(transform.position, game_area.transform.position);
        if (distance > fish_spawner.death_circle_radius)
        {
            RemoveFish();
        }
    }
    
    void RemoveFish()
    {
        Destroy(gameObject);
        if (fish_spawner != null) //this is to prevent some possible errors
            fish_spawner.fish_count -= 1;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MotherDuckCode player = other.gameObject.GetComponent<MotherDuckCode>();
            if (player != null) //preventing errors
                player.IncreaseScore(1);
            if (fish_spawner != null)
                fish_spawner.fish_count -= 1; //reduce fish count to have live count of fish
            if (Audio.Instance != null)
            Audio.Instance.SlurpSound();
            Destroy(gameObject);
        }
    }
}
