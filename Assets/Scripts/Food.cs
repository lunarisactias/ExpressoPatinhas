using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Food Info")]
    [SerializeField] private string foodName;
    [SerializeField] private float nutritionValue;

    public bool isBeingDragged { get; set; }
    private Rigidbody2D rb;

    public static Food instance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isBeingDragged = false;
        instance = this;
    }

    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Animal"))
        {
            Animal animal = collision.gameObject.GetComponent<Animal>();
            if (animal != null)
            {
                animal.Feed(nutritionValue);
                Destroy(gameObject);
            }
        }
    }

    private void Move()
    {
        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (touch.phase == TouchPhase.Began)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isBeingDragged = true;
                }
            }

            if (isBeingDragged && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                rb.MovePosition(touchPosition);
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isBeingDragged = false;
            }
        }
    }

    public bool GetIsBeingDragged()
    {
        return isBeingDragged;
    }
}
