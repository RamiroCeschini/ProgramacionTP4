using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultTurret : TurretAI
{
    private Transform lockOnPos;

    void Start()
    {
        StartTurret();
    }

    void Update()
    {
        UpdateState();
        UpdateCoolDownTimer();
    }

    protected override void FollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);
    }
    protected override void Shoot(GameObject go)
    {
        lockOnPos = go.transform;
        Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
        GetBulletFromPool("Catapult", muzzleMain);
    }

    public override void GetBulletFromPool(string bulletType, Transform muzzle)
    {
        GameObject missleGo = ObjectPool.SharedInstance.GetPooledObject(bulletType);
        Projectile projectile = missleGo.GetComponent<Projectile>();

        projectile.target = lockOnPos;
        projectile.projectileType = bulletType;

        if (missleGo != null)
        {
            missleGo.transform.position = muzzle.transform.position;
            missleGo.transform.rotation = muzzle.transform.rotation;
            missleGo.SetActive(true);
        }
    }
}
