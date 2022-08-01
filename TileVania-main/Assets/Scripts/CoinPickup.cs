using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinPickup : MonoBehaviour
{
   
    [SerializeField] AudioClip CoinFX;
    [SerializeField] int point = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            FindObjectOfType<gameSession>().AddInScore(point);
            AudioSource.PlayClipAtPoint(CoinFX,Camera.main.transform.position);
            Destroy(gameObject);  
        }
    }
}
