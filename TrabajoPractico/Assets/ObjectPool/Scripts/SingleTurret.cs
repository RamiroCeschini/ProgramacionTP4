using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTurret : MissileTurret
{
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
        turreyHead.forward = targetDir;
    }

    protected override void Shoot(GameObject go)
    {
        Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
        GetBulletFromPool("Single", muzzleMain);
    }
}
