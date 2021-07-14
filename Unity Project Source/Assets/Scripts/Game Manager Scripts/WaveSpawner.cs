using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int m_enemiesAlive = 0;

    public Wave[] m_waves;
    public Transform m_spawnPoint;
    public Text m_waveCountDownText;
    public Text m_currentRound;
    public float m_timeBetweenWaves = 5.0f;

    private float m_countDown = 5.0f;
    private int m_waveIndex;

    public GameManager m_gameManager;

    private void Update()
    {

        if (m_enemiesAlive > 0)
        {
            return;
        }

        if (m_waveIndex == m_waves.Length)
        {
            m_gameManager.WinLevel();
            this.enabled = false;
        }

        /* Count down timer for enemy spawning */
        if (m_countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            m_countDown = m_timeBetweenWaves;
            return;
        }

        m_countDown -= Time.deltaTime;

        /* Makes sure Countdown Timer will never be 0 */
        m_countDown = Mathf.Clamp(m_countDown, 0.0f, Mathf.Infinity);

        m_waveCountDownText.text = string.Format("{0:00.00}", m_countDown);
    }

    /* Spawns enemies based on the wave index number */
    IEnumerator SpawnWave()
    {
        PlayerStats.m_rounds++;

        m_currentRound.text = PlayerStats.m_rounds.ToString();

        Wave wave = m_waves[m_waveIndex];

        m_enemiesAlive = wave.m_count;

        for (int i = 0; i < wave.m_count; i++)
        {
            SpawnEnemy(wave.m_enemy);
            yield return new WaitForSeconds(1.0f / wave.m_rate);
        }
        m_waveIndex++;
    }

    /* Spawns an enemy at the spawn point */
    void SpawnEnemy(GameObject a_enemy)
    {
        Instantiate(a_enemy, m_spawnPoint.position, m_spawnPoint.rotation);
    }
}
