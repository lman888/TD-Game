using System.Collections.Generic;
using UnityEngine;

public class OrbTurret : MonoBehaviour
{
    private Transform m_target;
    private Enemy m_enemyTarget;

    [Header("General")]
    [SerializeField]
    private float m_range = 2.0f;

    [Header("Use Bullets(default)")]
    [SerializeField]
    private float _fireRate = 1.0f;
    [SerializeField]
    private float m_fireCountDown = 0.0f;

    [Header("Use Laser")]
    public bool m_useLaser = false;
    [SerializeField]
    private int m_damageOverTime;
    [SerializeField]
    private float m_slowAmount = 0.5f;
    [SerializeField]
    private LineRenderer m_lineRenderer;
    [SerializeField]
    private ParticleSystem m_impactEffect;
    [SerializeField]
    private Light m_impactLight;

    [Header("Unity Setup Fields")]
    public string m_enemyTag = "Enemy";
    [SerializeField]
    private GameObject m_bulletPrefab;
    [SerializeField]
    private Transform m_firePoint;
    [SerializeField]
    private Animation m_shootAnim;
    public bool m_isOrbLauncher;

    [SerializeField]
    public TurretUpgrades _turretSettings;

    [SerializeField]
    public List<GameObject> _models;

    Bullet _bullet;

    [SerializeField]
    private int _currentLevel;

    [HideInInspector] public TurretBluePrint _turretBluePrint;

    private TurretUpgrades.UpgradeDetails _upgradeDetails;

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    private void Start()
    {
        /* Makes sure this is called 0.5 times each second*/
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);

        for (int i = 0; i < _models.Count; i++)
        {
            //Instantiate(_models[i], gameObject.transform.position, Quaternion.identity);
            _models[i].SetActive(false);
        }

        UpdateModel();
    }

    public void IncreaseTurretLevel()
    {
        _currentLevel++;
    }

    private void Awake()
    {
        _bullet = m_bulletPrefab.GetComponent<Bullet>();
        _currentLevel = 0;
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

    void Update()
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

        UpdateTarget();
        
        if (m_useLaser)
        {
            Laser();
        }
        else if(m_isOrbLauncher)
        {
            OrbLauncher();
        }
        else
        {
            if (m_fireCountDown <= 0)
            {
                if (m_shootAnim != null)
                {
                    m_shootAnim.Play();
                }
                Shoot();
                m_fireCountDown = 1f / _fireRate;
            }

            m_fireCountDown -= Time.deltaTime;
        }
    }

    public Vector3 GetClosestTarget()
    {
        return m_target.position;
    }

    public Transform GetTarget()
    {
        return m_target;
    }

    void Laser()
    {

        m_enemyTarget.TakeDamage(m_damageOverTime * Time.deltaTime);
        m_enemyTarget.Slow(m_slowAmount);

        if (!m_lineRenderer.enabled)
        {
            m_lineRenderer.enabled = true;

            m_impactEffect.gameObject.SetActive(true);
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

    void OrbLauncher()
    {
        if (m_fireCountDown <= 0)
        {
            if (m_shootAnim != null)
            {
                m_shootAnim.Play();
            }

            GameObject m_bulletGameObject = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
            _bullet = m_bulletGameObject.GetComponent<Bullet>();

            if (_bullet != null)
                _bullet.Seek(m_target);
            m_fireCountDown = 1f / _fireRate;
        }

        m_fireCountDown -= Time.deltaTime;
    }    

    private void Shoot()
    {
        GameObject m_bulletGameObject = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        _bullet = m_bulletGameObject.GetComponent<Bullet>();

        SetDamage();

        if (_bullet != null)
            _bullet.Seek(m_target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_range);
    }

    public void UpdateModel()
    {
        _models[_currentLevel].SetActive(true);
    }

    public void SetUpgradeLevel(int level)
    {
        _currentLevel = level;
        _upgradeDetails = _turretSettings.GetUpgradeDetails(level);
        SetModelForUpgrade(level);
    }

    public void UpgradeTurret()
    {
        Debug.Log("Turret Upgraded");
    }

    //Vector3 UpdateTurretPosition()
    //{
    //    return transform.position;
    //}

    private void SetModelForUpgrade(int level)
    {
        var modelIndex = level;
        if (modelIndex >= _models.Count)
        {
            Debug.LogWarning($"Upgrade model does not exist for {level}");
            modelIndex = 0;
        }

        for (int i = 0; i < _models.Count; i++)
        {
            _models[0].SetActive(false);
            _models[i].SetActive(i == modelIndex);
            _models[modelIndex].GetComponent<Transform>().position = _models[0].gameObject.transform.position;
        }
    }

    void SetDamage()
    {
        _bullet.m_damage = _turretSettings.upgradeDetails[_currentLevel].Damage;
        m_slowAmount = _turretSettings.upgradeDetails[_currentLevel].SlowPower;
        _fireRate = _turretSettings.upgradeDetails[_currentLevel].ShootSpeed;
    }
}
