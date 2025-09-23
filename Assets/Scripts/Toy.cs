using UnityEngine;

public class Toy : MonoBehaviour
{
    [Header("Toy Info")]
    [SerializeField] private string toyName;
    [SerializeField] private float happinessValue;

    public bool isBeingDragged { get; set; }
    private Rigidbody2D rb;
    private bool isPlacedInAnimal = false;
    private GameObject animalToPlayWith;

    public static Toy instance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isBeingDragged = false;
        instance = this;
    }

    void Update()
    {
        Move();
        PlayWith();
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

    public void PlayWith()
    {
        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);

            if (isPlacedInAnimal && touch.phase == TouchPhase.Ended && animalToPlayWith != null)
            {
                Destroy(gameObject);
                if (animalToPlayWith != null)
                {
                    animalToPlayWith.GetComponent<Animal>().PlayWithToy(happinessValue);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");

        if (collision.gameObject.CompareTag("Animal") && !isPlacedInAnimal)
        {
            isPlacedInAnimal = true;
            animalToPlayWith = collision.gameObject;
        }
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Animal") && isPlacedInAnimal)
        {
            isPlacedInAnimal = false;
        }
    }

}
