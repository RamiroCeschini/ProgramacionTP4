using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    [SerializeField] protected GameObject currentTarget;
    [SerializeField] protected Transform turreyHead;

    [SerializeField] protected float attackDist;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float shootCoolDown;
    private float timer;
    [SerializeField] protected float loockSpeed;


    [SerializeField] protected Vector3 randomRot;
    [SerializeField] protected Animator animator;

    [SerializeField] protected Transform muzzleMain;
    [SerializeField] protected GameObject muzzleEff;
    [SerializeField] protected GameObject bullet;
  

    protected void StartTurret()
    {
        InvokeRepeating("CheckForTarget", 0, 0.5f);

        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }

    protected void UpdateState()
    {
        if (currentTarget != null)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTargetDist > attackDist)
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRotate();
        }
    }

    protected void UpdateCoolDownTimer()
    {

        timer += Time.deltaTime;
        if (timer >= shootCoolDown)
        {
            if (currentTarget != null)
            {
                timer = 0;

                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                }

                Shoot(currentTarget);
            }
        }
    }

    private void CheckForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Player")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    public void IdleRotate()
    {
        bool refreshRandom = false;
        
        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), loockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }

    protected virtual void FollowTarget() { }
    protected virtual void Shoot(GameObject go) {}
    public virtual void GetBulletFromPool(string bulletType, Transform muzzle){}
}
