using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform[] cameraPoints;
    [SerializeField] private float moveSpeed = 5f;
    public int currentPointIndex = 1;
    private Rigidbody2D rb;
    private Vector2 startTouchPosition, endTouchPosition;
    [SerializeField] private float swipeThreshold;

    [Header("LOJA")]
    public bool storeOpen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.MovePosition(cameraPoints[1].position);
    }

    void Update()
    {
        if (Food.instance.isBeingDragged || Toy.instance.isBeingDragged)
        {
            return;
        }
        else
        {
            MoveCamera();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    private void MoveCamera()
    {
        MoveCameraUp();
        MoveCameraLeft();
        MoveCameraRight();
    }
    public void MoveCameraLeft()
    {
        Swipe();
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended && startTouchPosition.x < endTouchPosition.x - swipeThreshold)
        {
            if (currentPointIndex > 0) currentPointIndex -= 1;
            StopAllCoroutines();
            StartCoroutine(MoveToPosition(gameObject, cameraPoints[currentPointIndex].position, moveSpeed));
        }
    }

    public void MoveCameraRight()
    {
        Swipe();
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended && startTouchPosition.x > endTouchPosition.x + swipeThreshold)
        {
            if (currentPointIndex < 2) currentPointIndex += 1;
            StopAllCoroutines();
            StartCoroutine(MoveToPosition(gameObject, cameraPoints[currentPointIndex].position, moveSpeed));
        }
    }

    public void MoveCameraUp()
    {
        Swipe();
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended && startTouchPosition.y < endTouchPosition.y - swipeThreshold)
        {
            storeOpen = true;
            //StartCoroutine(MoveToPosition(gameObject, cameraPoints[currentPointIndex].position, moveSpeed));
        }
    }

    private void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
        }
    }

    private IEnumerator MoveToPosition(GameObject go, Vector2 movePosition, float s)
    {
        while ((Vector2)go.transform.position != movePosition)
        {
            go.transform.position = Vector2.MoveTowards(go.transform.position, movePosition, s * Time.deltaTime);
            startTouchPosition = Vector2.zero;
            endTouchPosition = Vector2.zero;
            yield return null;
        }

    }
}
