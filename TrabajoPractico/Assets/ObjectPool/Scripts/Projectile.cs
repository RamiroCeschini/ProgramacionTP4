using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public string projectileType;
    public Transform target;
    public bool lockOn;

    public float speed = 1;
    public float turnSpeed = 1;
    public bool catapult;

    public float knockBack = 0.1f;
    public float boomTimer = 1;


    public ParticleSystem explosion;

    private void OnEnable()
    {
        if (catapult)
        {
            lockOn = true;
        }

        if (projectileType == "Single" && target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private void Update()
    {
        CheckExplotion();

        if (projectileType == "Catapult")
        {
            if (lockOn)
            {
                Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);

                transform.GetComponent<Rigidbody>().velocity = Vo;
                lockOn = false;
            }
        }else if(projectileType == "Dual")
        {
            Vector3 dir = target.position - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);


            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }else if (projectileType == "Single")
        {
            float singleSpeed = speed * Time.deltaTime;
            transform.Translate(transform.forward * singleSpeed * 2, Space.World);
        }
    }

    private void CheckExplotion()
    {
        if (target == null)
        {
            Explotion();
            return;
        }

        if (transform.position.y < -0.2F)
        {
            Explotion();
        }

        boomTimer -= Time.deltaTime;
        if (boomTimer < 0)
        {
            Explotion();
        }
    }

    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Vector3 dir = other.transform.position - transform.position;
            Vector3 knockBackPos = other.transform.position + (dir.normalized * knockBack);
            knockBackPos.y = 1;
            other.transform.position = knockBackPos;
            Explotion();
        }
    }

    public void Explotion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = new Vector3(100, 100, 100);
        boomTimer = 10;
    }
}
