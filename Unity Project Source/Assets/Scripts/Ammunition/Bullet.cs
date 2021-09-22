using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float m_speed = 70.0f;
    public float m_explosionRadius = 0.0f;
    public GameObject m_impactEffect;

    public int m_damage = 20;

    private Transform m_target;

    public void Seek(Transform a_target)
    {
        m_target = a_target;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = m_target.position - transform.position;
        float distanceThisFrame = m_speed * Time.deltaTime;

        /* Potentially use Collisions (Collisions are a bit finicky so care) */
        /* Using Distance calculations are more reliable but costs more performance */

        /* Look up Square Mag and use that potentially instead */

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }


    private void HitTarget()
    {
        GameObject m_effectInstance = Instantiate(m_impactEffect, transform.position, transform.rotation);
        Destroy(m_effectInstance, 3.0f);

        if (m_explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(m_target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] m_hitObjects = Physics.OverlapSphere(transform.position, m_explosionRadius);

        foreach (Collider collider in m_hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform a_enemy)
    {
        Enemy e = a_enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(m_damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }
}
