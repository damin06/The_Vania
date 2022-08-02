using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class gameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI LifeText;
    [SerializeField] TextMeshProUGUI CoinText;
    public int coinCounter;

    public void AddInScore(int point)
    {
        coinCounter += point;
        CoinText.text = coinCounter.ToString();
    }
    private void Start()
    {
        CoinText.text = coinCounter.ToString();
        LifeText.text =playerLives.ToString();
    }
    private void Awake()
    {
        int numGameSession = FindObjectsOfType<gameSession>().Length;
        if(numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PrecessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }

        void ResetGameSession()
        {
            FindObjectOfType<SceenPersist>().ResetPersists();
            SceneManager.LoadScene(0);
            Destroy(gameObject);    
        }

        void TakeLife() 
        {
           playerLives--;
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            LifeText.text = playerLives.ToString();
        }
    }
}
