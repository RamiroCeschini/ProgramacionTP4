using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : TurretAI
{
    public override void GetBulletFromPool(string bulletType, Transform muzzle)
    {
        GameObject missleGo = ObjectPool.SharedInstance.GetPooledObject(bulletType);
        Projectile projectile = missleGo.GetComponent<Projectile>();


        projectile.target = transform.GetComponent<MissileTurret>().currentTarget.transform;
        projectile.projectileType = bulletType;


        if (missleGo != null)
        {
            missleGo.transform.position = muzzle.transform.position;
            missleGo.transform.rotation = muzzle.transform.rotation;
            missleGo.SetActive(true);
        }
    }
}
