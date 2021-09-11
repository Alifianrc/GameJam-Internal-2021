using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemySpawnPoint;
    public GameObject playerBulletDestroyer;
    public GameObject enemyBulletDestroyer;

    private float enemySpawnCooldown = 3f;
    private int maxEnemyCount = 5;
    private int enemyCount;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text gameOverScore;
    private int score;
    private int highScore;
    SaveData theData;

    private void Start()
    {
        StartCoroutine(EnemySpawner());
        theData = SaveGame.LoadData();
        enemyCount = 0;
        score = 0;
        scoreText.text = score.ToString();
        highScore = theData.highScore;
        highScoreText.text = highScore.ToString();
    }

    
    private void Update()
    {
        
    }

    private IEnumerator EnemySpawner()
    {
        while (true)
        {
            if(enemyCount <= maxEnemyCount)
            {
                Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
                enemyCount++;
            }
            yield return new WaitForSeconds(enemySpawnCooldown);
        }
    }

    public void EnemyKilled()
    {
        score += 10;
        scoreText.text = score.ToString();
        enemyCount--;
        if(score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();
            theData.highScore = highScore;
            SaveGame.SaveProgress(theData);
        }
    }

    public void GameIsOver()
    {
        gameOverPanel.SetActive(true);
        gameOverScore.text = score.ToString();
    }

    // UI -------------------------------------------------------------------------------
    public void PauseGame()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
