using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    /* Objects Speed */
    public float m_startSpeed = 10.0f;
    [HideInInspector]
    public float m_speed;

    public float m_startHealth = 100;
    private float m_health;

    public int m_worth = 50;

    public GameObject m_deathEffect;

    [Header("Unity Stuff")]
    public Image m_healthBar;

    private bool m_isDead = false;

    private void Start()
    {
        m_speed = m_startSpeed;
        m_health = m_startHealth;
    }

    public void TakeDamage(float a_amount)
    {
        m_health -= a_amount;

        m_healthBar.fillAmount = m_health / m_startHealth;

        if (m_health <= 0 && !m_isDead)
        {
            Die();
        }
    }

    public void Slow(float a_pct)
    {
        m_speed = m_startSpeed * (1.0f - a_pct);
    }

    void Die()
    {
        m_isDead = true;

        PlayerStats.m_money += m_worth;
        GameObject m_effect = Instantiate(m_deathEffect, transform.position, Quaternion.identity);
        Destroy(m_effect, 2f);

        WaveSpawner.m_enemiesAlive--;
        Destroy(gameObject);
    }

}
