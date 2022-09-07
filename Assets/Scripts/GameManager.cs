using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] GameObject[] enemyPrefabs = new GameObject[2];
    [SerializeField] float spawnPosY;
    [SerializeField] float spawnPosXRange;

    private int waveNumber = 0;
    private bool isSpawningWave = false;

    [SerializeField] private int m_enemyCount;
    public int enemyCount {
        get { return m_enemyCount; }
        set {
            if (value < 0) {
                Debug.Log ("GameManager.enemyCount cannot be negative!");
            }
            else {
                m_enemyCount = value;
            }
        }
    }

    private void Start() {
        instance = this;
        enemyCount = 0;
    }

    private void Update() {
        playerHealthText.text = $"Health: {Player.instance.health}";
        if (enemyCount == 0 && !isSpawningWave) {
            isSpawningWave = true;
            StartCoroutine (WaveTimer ());
        }
    }

    IEnumerator WaveTimer() {
        yield return new WaitForSeconds (3.0f);
        SpawnEnemyWave ();
        isSpawningWave = false;
    }

    private void SpawnEnemyWave() {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++) {
            Vector3 spawnPos = new Vector3 (Random.Range (-spawnPosXRange, spawnPosXRange), spawnPosY, 0);
            GameObject enemyPrefab = enemyPrefabs[Random.Range (0, enemyPrefabs.Length)];
            Instantiate (enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
            enemyCount++;
        }
    }
}
