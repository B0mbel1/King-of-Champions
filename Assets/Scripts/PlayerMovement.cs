using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Reference")]
    [Tooltip("Drag object-child with graphic and bones")]
    [SerializeField] private Transform graphicsTransform;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(graphicsTransform == null & transform.childCount > 0)
        {
            graphicsTransform = transform.GetChild(0);
        }
    }

    void Update()
    {
        if (movement.x > 0)
        {
            graphicsTransform.localScale = new Vector3(1f,1f,1f);
        }
        else if(movement.x < 0)
        {
            graphicsTransform.localScale = new Vector3(-1f,1f,1f);
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}