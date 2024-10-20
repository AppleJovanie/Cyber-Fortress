using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public int damage = 50;
    public GameObject ImpactEffect;
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        Damage(target);


        audioManager.PlaySfx(audioManager.bulletImpact);
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        Enemies e = enemy.GetComponent<Enemies>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
