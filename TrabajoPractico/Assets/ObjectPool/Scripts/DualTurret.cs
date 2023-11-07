using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualTurret : MissileTurret
{
    [SerializeField] private Transform muzzleSub;
    private bool shootLeft = true;

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
        if (shootLeft)
        {
            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GetBulletFromPool("Dual", muzzleMain);
        }
        else
        {
            Instantiate(muzzleEff, muzzleSub.transform.position, muzzleSub.rotation);
            GetBulletFromPool("Dual", muzzleSub);
        }

        shootLeft = !shootLeft;
    }
}
