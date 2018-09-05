using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Asteroid
{
    public GameObject asteroid1, asteroid2, asteroid3;
}

[System.Serializable]
public class GiantAsteroid
{
    public GameObject asteroid1, asteroid2, asteroid3;
}

[System.Serializable]
public class Levels
{
    public int level1, level2, level3, level4, level5, level6;
}

[System.Serializable]
public class EnemiesSpawnLocation
{
    public float positionMinZ, positionMaxZ;
}

[System.Serializable]
public class Enemies
{
    public GameObject enemies;
    public GameObject block;
    public GameObject enemiesShots;

}
public class gameController : MonoBehaviour
{
    public Asteroid asteroids;
    public GiantAsteroid giantAsteroids;
    public Levels levels;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int level;
    public int longueurWall;
    public float wallSpeed;
    public EnemiesSpawnLocation enemiesSpawnLocation;
    public Enemies enemies;

    public Text scoreText;
    public Text restartText;
    public Text gameoverText;
    public Text explosionText;

    private GameObject asteroid;
    private GameObject giantAsteroid;
    private bool gameover;
    private bool restart;
    private int score;
    private int explosionCount;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
        score = 0;
        UpdateScore();
        restartText.text = "";
        gameoverText.text = "";
        explosionText.text = "nombre d'explosion : " + explosionCount.ToString() + " (appuyez sur CTL pour utiliser)";
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            level = CalculerLevel(score);
            for (int i = 0; i < hazardCount; i++)
            {
                AsteroidGenerater(asteroids.asteroid1, asteroids.asteroid2, asteroids.asteroid3);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (level == 1)
                {
                    Instantiate(asteroid, spawnPosition, spawnRotation);
                }
                if (level == 2 || level == 3)
                {
                    if (i == 0)
                    {
                        AsteroidGenerater(giantAsteroids.asteroid1, giantAsteroids.asteroid2, giantAsteroids.asteroid3);
                        Instantiate(asteroid, spawnPosition, spawnRotation);
                    }
                    else
                    {
                        Instantiate(asteroid, spawnPosition, spawnRotation);
                    }
                }
                else if (level == 4 || level == 5 || level == 6)
                {
                    if (1 == (int) Random.Range(0.0f, 10.0f))
                    {
                        AsteroidGenerater(giantAsteroids.asteroid1, giantAsteroids.asteroid2, giantAsteroids.asteroid3);
                        Instantiate(asteroid, spawnPosition, spawnRotation);
                    }
                    else
                    {
                        Instantiate(asteroid, spawnPosition, spawnRotation);
                    }

                    int chanceWall = (int) Random.Range(0.0f, 30.0f);
                    if (chanceWall == 1)
                    {
                        float tamponSpawnWait = spawnWait;
                        float tamponWaveWait = waveWait;

                        spawnWait = wallSpeed;
                        waveWait = wallSpeed;

                        for (int wall = 0; wall < longueurWall; wall++)
                        {
                            for (int j = 0; j < hazardCount; j++)
                            {
                                AsteroidGenerater(asteroids.asteroid1, asteroids.asteroid2, asteroids.asteroid3);
                                Vector3 spawnPosition2 = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                                Quaternion spawnRotation2 = Quaternion.identity;
                                Instantiate(asteroid, spawnPosition2, spawnRotation2);
                                yield return new WaitForSeconds(spawnWait);
                            }
                            yield return new WaitForSeconds(waveWait);
                        }
                        spawnWait = tamponSpawnWait;
                        waveWait = tamponWaveWait;
                    }
                    if (level == 5 || level == 6)
                    {
                        int enemyChance = (int)Random.Range(0.0f, 30.0f);
                        if (enemyChance == 1)
                        {
                            float xPosition;
                            int sideChoice = (int)Random.Range(0.0f, 2.0f);
                            if (sideChoice == 1)
                            {
                                xPosition = -8.0f;
                            }
                            else
                            {
                                xPosition = 8.0f;
                            }
                            float zPosition = (int)Random.Range(enemiesSpawnLocation.positionMinZ, enemiesSpawnLocation.positionMaxZ);
                            Vector3 spawnPositionEnemy = new Vector3(xPosition, 0, zPosition);
                            Quaternion spawnRotationEnemy = Quaternion.identity;
                            spawnRotationEnemy.y = 180;
                            Instantiate(enemies.enemies, spawnPositionEnemy, spawnRotationEnemy);
                        }
                        if (level == 6)
                        {
                            if ((int)Random.Range(0.0f, 50.0f) == 1)
                            {
                                float xPosition;
                                int sideChoice = (int)Random.Range(0.0f, 2.0f);
                                if (sideChoice == 1)
                                {
                                    xPosition = -8.0f;
                                }
                                else
                                {
                                    xPosition = 8.0f;
                                }
                                float zPosition = (int)Random.Range(enemiesSpawnLocation.positionMinZ, enemiesSpawnLocation.positionMaxZ);
                                Vector3 spawnPositionEnemy = new Vector3(xPosition, 0, zPosition);
                                Quaternion spawnRotationEnemy = Quaternion.identity;
                                spawnRotationEnemy.y = 180;
                                Instantiate(enemies.block, spawnPositionEnemy, spawnRotationEnemy);
                            }
                        }
                    }
                }
                yield return new WaitForSeconds(spawnWait);
            }

            
            yield return new WaitForSeconds(waveWait);
            if (gameover)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void AddExplosionCount(int i)
    {
        explosionCount += i;
        updateExplosionCount();
    }

    private void updateExplosionCount()
    {
        explosionText.text = "nombre d'explosion : " + explosionCount.ToString() + " (appuyez sur CTL pour utiliser)";
    }

    public void GameOver()
    {
        gameoverText.text = "Game Over!";
        gameover = true;
    }

    public GameObject AsteroidGenerater(GameObject asteroid1, GameObject asteroid2, GameObject asteroid3)
    {
        float chose = ((Random.value * 3) % 3);
        if (chose < 1)
        {
            asteroid = asteroid1;
            return asteroid1;
        }
        else if (chose < 2)
        {
            asteroid = asteroid2;
            return asteroid2;
        }
        else
        {
            asteroid = asteroid3;
            return asteroid3;
        }
        
    }

    public int CalculerLevel(int score)
    {
        if (score <= levels.level1)
        {
            return 1;
        }
        else if (score <= levels.level2)
        {
            return 2;
        }
        else if (score <= levels.level3)
        {
            return 3;
        }
        else if (score <= levels.level4)
        {
            return 4;
        }
        else if (score <= levels.level5)
        {
            return 5;
        }
        else
        {
            return 6;
        }
    }
}
