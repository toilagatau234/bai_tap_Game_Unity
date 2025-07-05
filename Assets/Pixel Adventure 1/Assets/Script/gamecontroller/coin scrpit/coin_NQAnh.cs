using UnityEngine;

public class CoinSimple : MonoBehaviour
{
    private bool isCollected = false; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_NQAnh") && !isCollected) 
        {
            isCollected = true; 
            int coinValue = 5;
            game_controller_NQAnh.instance.CollectCoin(coinValue); 
            Destroy(gameObject);
        }
    }
}
