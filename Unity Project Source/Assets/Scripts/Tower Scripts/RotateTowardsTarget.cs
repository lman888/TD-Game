using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTarget : MonoBehaviour
{
    private OrbTurret _fatherObject;
    private void Start()
    {
        _fatherObject = GetComponentInParent<OrbTurret>();
    }

    // Update is called once per frame
    void Update()
    {
        LockOnTarget();
    }

    void LockOnTarget()
    {
        if (_fatherObject.GetTarget() == null)
            return;

        Vector3 m_dir = _fatherObject.GetClosestTarget() - transform.position;
        Quaternion m_lookRot = Quaternion.LookRotation(m_dir);
        Vector3 m_rotation = Quaternion.Lerp(transform.rotation, m_lookRot, Time.deltaTime * 2).eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, m_rotation.y, 0.0f);
    }


}
