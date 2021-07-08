using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    /* Target Object the Object heads towards */
    private Transform m_target;
    /* Current Waypoint we are pursuing */
    private int m_wayPointIndex = 0;

    private Enemy m_enemy;

    void Start()
    {
        m_enemy = GetComponent<Enemy>();

        /* Locate the First WayPoint in the Array */
        m_target = WayPoints.m_wayPoint[0];
    }

    void Update()
    {
        /* Obtains the Direction Vector between the Object and its Target */
        Vector3 m_dir = m_target.position - transform.position;
        transform.Translate(m_dir.normalized * m_enemy.m_speed * Time.deltaTime, Space.World);

        /* If the Distance is  */
        if (Vector3.Distance(transform.position, m_target.position) <= 0.1)
        {
            GetNextWayPoint();
        }

        m_enemy.m_speed = m_enemy.m_startSpeed;
    }

    void GetNextWayPoint()
    {
        /* When it reaches the final waypoint, we destroy the Game Object */
        if (m_wayPointIndex >= WayPoints.m_wayPoint.Length - 1)
        {
            EndPath();
            return;
        }

        /* Travels to the next waypoint after reaching the previous waypoint */
        m_wayPointIndex++;
        m_target = WayPoints.m_wayPoint[m_wayPointIndex];
    }

    void EndPath()
    {
        PlayerStats.m_lives--;
        WaveSpawner.m_enemiesAlive--;
        Destroy(gameObject);
    }

}
