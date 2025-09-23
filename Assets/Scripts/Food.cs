using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Food Info")]
    [SerializeField] private string foodName;
    [SerializeField] private float nutritionValue;

    public bool isBeingDragged { get; set; }
    private Rigidbody2D rb;
    private bool isPlacedInAnimalMouth = false;
    private GameObject animalToBeFed;

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
        FeedFood();
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

    public void FeedFood()
    {
        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);

            if (isPlacedInAnimalMouth && touch.phase == TouchPhase.Ended && animalToBeFed != null)
            {
                Destroy(gameObject);
                animalToBeFed.GetComponent<Animal>().FeedAnimal(nutritionValue);
            }
        }
    }

    public bool GetIsBeingDragged()
    {
        return isBeingDragged;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");

        if (collision.gameObject.CompareTag("Animal") && !isPlacedInAnimalMouth)
        {
            isPlacedInAnimalMouth = true;
            animalToBeFed = collision.gameObject;
            Debug.Log(isPlacedInAnimalMouth + animalToBeFed.name);
        }
    }
}
