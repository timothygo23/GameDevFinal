using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    public float Range = 100;
    public float ShootingDelay = 2.0f;
    private Camera _camera;
    private ParticleSystem _particle;
    private LayerMask _shootableMask;
    private float _timer;

    void Start()
    {
        _camera = Camera.main;
        _particle = GetComponentInChildren<ParticleSystem>();
        //Cursor.lockState = CursorLockMode.Locked;
        _shootableMask = LayerMask.GetMask("Shootable");
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && _timer >= ShootingDelay)
        {
            _timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Range, _shootableMask))
        {        
            print("hit " + hit.collider.gameObject);
            _particle.Play();
            ZombieHealth health = hit.collider.GetComponent<ZombieHealth>();

            if (health != null)
            {
                health.TakeDamage(1);
                //if(health.Health < 1)
                //    for(int i = 0; i< 5; i++)
                //        Destroy(hit.collider);
            }
        }
    }
}