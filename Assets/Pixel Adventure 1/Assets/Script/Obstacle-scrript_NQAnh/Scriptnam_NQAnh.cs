using UnityEngine;

public class Scriptnam_NQAnh : MonoBehaviour
{
    public float speed = 1f;        
    public float leftLimit = -4f;   
    public float rightLimit = 2f;   

    private int direction = 1;      

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= rightLimit)
        {
            direction = -1;
            Flip();
        }
        else if (transform.position.x <= leftLimit)
        {
            direction = 1;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}
