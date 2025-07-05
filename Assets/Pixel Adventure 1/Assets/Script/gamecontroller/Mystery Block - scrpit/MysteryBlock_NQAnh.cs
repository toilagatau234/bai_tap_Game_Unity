using UnityEngine;

public class MysteryBlock_NQAnh : MonoBehaviour
{
    public GameObject coinPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_NQAnh"))
        {
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
       
        Vector3 spawnPosition = transform.position + new Vector3(0f, 1f, 0f);
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
}
