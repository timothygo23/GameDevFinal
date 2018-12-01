using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    private const string SHOOTABLE_LAYER = "Zombie";

    [SerializeField] private ParticleSystem onHitParticle;
    
    public float Range = 100;
    public float ShootingDelay = 2.0f;
    private Camera _camera;
    private LayerMask _shootableMask;
    private float _timer;

    private Vector3 target = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    void Start()
    {
        _camera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        _shootableMask = LayerMask.GetMask(SHOOTABLE_LAYER);
        _timer = 0;

        onHitParticle.Stop();
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
        Ray ray = _camera.ScreenPointToRay(target);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Range, _shootableMask))
        {        
            print("hit " + hit.collider.gameObject);

            onHitParticle.Stop();

            Vector3 onHitPosition = hit.point;
            onHitParticle.transform.position = onHitPosition;

            onHitParticle.Play();

            ZombieHealth health = hit.collider.GetComponent<ZombieHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }
}