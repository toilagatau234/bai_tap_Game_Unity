using UnityEngine;

public class reward : MonoBehaviour
{

    //loại trái cây
    public enum FruitType
    {
        apple,   // Tên trái cây là 
        banana
    }


    public FruitType fruitType;
    public int points;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        switch (fruitType)
        {
            case FruitType.apple:
                points = 10;
                break;
            case FruitType.banana: 
                points = 15; 
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra nếu va chạm với nhân vật
        {
            // Gọi phương thức thêm điểm từ GameController
            GameController.instance.AddScore(points);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
