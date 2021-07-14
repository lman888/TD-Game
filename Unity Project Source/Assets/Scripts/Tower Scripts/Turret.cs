using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform m_target;
    private Enemy m_enemyTarget;

    [Header("General")]
    public float m_range = 2.0f;

    [Header("Use Bullets(default)")]
    public float m_fireRate = 1.0f;
    private float m_fireCountDown = 0.0f;

    [Header("Use Laser")]
    public bool m_useLaser = false;
    public int m_damageOverTime;
    public float m_slowAmount = 0.5f;
    public LineRenderer m_lineRenderer;
    public ParticleSystem m_impactEffect;
    public Light m_impactLight;

    [Header("Unity Setup Fields")]
    public string m_enemyTag = "Enemy";
    public GameObject m_bulletPrefab;
    public Transform m_firePoint;
    public Transform m_partToRotate;
    public float m_turnSpeed = 10.0f;

    private void Start()
    {
        /* Makes sure this is called 0.5 times each second*/
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    void UpdateTarget()
    {
        /* Finds all of our enemies */
        GameObject[] m_enemies = GameObject.FindGameObjectsWithTag(m_enemyTag);
        /* Set up the Shortest Distance between the tower and the enemy */
        float m_shortestDist = Mathf.Infinity;
        /* GameObject reference to our closest enemy */
        GameObject m_nearestEnemy = null;

        /* Iterate through each Enemy GameObject */
        foreach (GameObject enemy in m_enemies)
        {
            /* Obtain the distance between each anamy and our turret */
            float m_distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            /* If the distance to the enemy is shorter then any distance we have detected before */
            if (m_distanceToEnemy < m_shortestDist)
            {
                /* Set the shortest Distance to this enemy */
                m_shortestDist = m_distanceToEnemy;
                /* Assign this enemy to be the closest enemy */
                m_nearestEnemy = enemy;
            }
        }

        if (m_nearestEnemy != null && m_shortestDist <= m_range)
        {
            m_target = m_nearestEnemy.transform;
            m_enemyTarget = m_nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            m_target = null;
        }

    }

    private void Update()
    {
        if (m_target == null)
        {
            if (m_useLaser)
            {
                if (m_lineRenderer.enabled)
                {
                    m_lineRenderer.enabled = false;

                    m_impactEffect.Stop();
                    m_impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (m_useLaser)
        {
            Laser();
        }
        else
        {
            if (m_fireCountDown <= 0)
            {
                Shoot();
                m_fireCountDown = 1f / m_fireRate;
            }

            m_fireCountDown -= Time.deltaTime;
        }
    }

    void Laser()
    {

        m_enemyTarget.TakeDamage(m_damageOverTime * Time.deltaTime);
        m_enemyTarget.Slow(m_slowAmount);

        if (!m_lineRenderer.enabled)
        {
            m_lineRenderer.enabled = true;

            m_impactEffect.Play();
            m_impactLight.enabled = true;
        }

        m_lineRenderer.SetPosition(0, m_firePoint.position);
        m_lineRenderer.SetPosition(1, m_target.position);

        /* Direction that points back to our turret */
        Vector3 m_dir = m_firePoint.position - m_target.position;

        /* Moves where the Particle System emits when it hits the object that it is targeting */
        m_impactEffect.transform.position = m_target.position + m_dir.normalized * 0.03f;

        /* Rotates the Particle system to look in the direction of the Firepoint */
        m_impactEffect.transform.rotation = Quaternion.LookRotation(m_dir);
    }

    void LockOnTarget()
    {
        Vector3 m_dir = m_target.position - transform.position;
        Quaternion m_lookRot = Quaternion.LookRotation(m_dir);
        Vector3 m_rotation = Quaternion.Lerp(m_partToRotate.rotation, m_lookRot, Time.deltaTime * m_turnSpeed).eulerAngles;
        m_partToRotate.rotation = Quaternion.Euler(0.0f, m_rotation.y, 0.0f);
    }

    private void Shoot()
    {
        GameObject m_bulletGameObject = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        Bullet bullet = m_bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(m_target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_range);
    }
}
