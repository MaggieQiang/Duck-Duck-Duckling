using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    [SerializeField] private float cooldown = 0f;
    private float nextShootTime = 0f;

    void Update()
    {
        if (Time.time < nextShootTime)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = firePoint.position.z;

            Vector2 direction = (mousePos - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);

            if (Audio.Instance != null)
                Audio.Instance.ShootSound();

            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                nextShootTime = Time.time + cooldown;
            }
        }
    }

    public void SetCooldown(float value)
    {
        cooldown = value;
    }
}
