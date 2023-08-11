using UnityEngine;
using UnityEngine.Pool;

public abstract class PieceController : MonoBehaviour, IMoveable, IRotateable, IPlaceable
{
    public Vector2Int Dimensions => pieceConfig.dimensions;
    public bool HasBeenPlaced => hasBeenPlaced;

    [SerializeField] private PieceConfigSO pieceConfig;

    [SerializeField] private VoidEventSO piecePlacedEvent;
    [SerializeField] private BoolEventSO pieceDestroyedEvent;

    private Rigidbody2D rb;
    private bool hasFallenOffMap;
    private bool hasBeenPlaced;
    private MovementDirection movementDirection;
    protected IObjectPool<PieceController> piecesPool;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementDirection = MovementDirection.None;
        rb.velocity = new Vector2(0, pieceConfig.YSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HasBeenPlaced)
        {
            return;
        }

        Place();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasFallenOffMap)
        {
            return;
        }

        if (collision.gameObject.layer == Layers.FLOOR_LAYER)
        {
            pieceDestroyedEvent.RaiseEvent(!HasBeenPlaced);

            hasFallenOffMap = true;

            piecesPool.Release(this);
        }
    }

    private void Update()
    {
        if (!HasBeenPlaced && !IsPieceInsideMapLimits(movementDirection == MovementDirection.Right))
        {
            StopMove();
            return;
        }
    }

    /// <summary>
    /// Returns true if piece is inside the screen's borders
    /// </summary>
    /// <param name="isGoingRight"></param>
    /// <returns></returns>
    private bool IsPieceInsideMapLimits(bool isGoingRight)
    {
        int multiplier = isGoingRight ? 1 : -1;

        switch (transform.rotation.eulerAngles.z)
        {
            case 0:
            case 180:
                if (multiplier * transform.position.x + Dimensions.x / 2 > Constants.PIECES_POSITION_LIMITS.x)
                {
                    return false;
                }
                break;
            case 90:
            case 270:
                if (multiplier * transform.position.x + Dimensions.y / 2 > Constants.PIECES_POSITION_LIMITS.x)
                {
                    return false;
                }
                break;
        }

        return true;
    }

    public virtual void Place()
    {
        SetPieceLayer(Layers.LANDED_PIECES_LAYER);
        SetPiecePhysics();

        hasBeenPlaced = true;

        piecePlacedEvent.RaiseEvent();
    }

    private void SetPieceLayer(int layerIndex)
    {
        gameObject.layer = layerIndex;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layerIndex;
        }
    }

    private void SetPiecePhysics()
    {
        rb.mass = pieceConfig.Mass;
        rb.drag = pieceConfig.Drag;
        rb.angularDrag = pieceConfig.AngularDrag;
        rb.gravityScale = pieceConfig.GravityScale;
    }

    public void Move(MovementDirection movementDirection)
    {
        switch (movementDirection)
        {
            case MovementDirection.None:
            case MovementDirection.Up:
            case MovementDirection.Down:
                return;
            case MovementDirection.Left:
            case MovementDirection.Right:
                this.movementDirection = movementDirection;
                int direction = movementDirection == MovementDirection.Right ? 1 : -1;

                rb.velocity = new Vector2(direction * pieceConfig.XSpeed, pieceConfig.YSpeed);
                break;
        }

        ClampPiecePosition();
    }

    public void StopMove()
    {
        movementDirection = MovementDirection.None;
        ClampPiecePosition();

        rb.velocity = new Vector2(0, pieceConfig.YSpeed);
    }

    public void Rotate(MovementDirection rotationDirection)
    {
        switch (rotationDirection)
        {
            case MovementDirection.None:
            case MovementDirection.Up:
            case MovementDirection.Down:
                return;
            case MovementDirection.Left:
            case MovementDirection.Right:
                int direction = rotationDirection == MovementDirection.Right ? -1 : 1;

                transform.Rotate(new Vector3(0, 0, direction * 90));
                break;
        }

        ClampPiecePosition();
    }

    /// <summary>
    /// Clamps the piece's position inside of the screen borders
    /// </summary>
    private void ClampPiecePosition()
    {
        Vector3 position;

        switch (transform.rotation.eulerAngles.z)
        {
            case 0:
            case 180:
                if (Mathf.Abs(transform.position.x) + Dimensions.x / 2 > Constants.PIECES_POSITION_LIMITS.x)
                {
                    position = transform.position;
                    position.x = Mathf.Clamp(position.x, -Constants.PIECES_POSITION_LIMITS.x + Dimensions.x / 2, Constants.PIECES_POSITION_LIMITS.x - Dimensions.x / 2);
                    transform.position = position;
                }
                break;
            case 90:
            case 270:
                if (Mathf.Abs(transform.position.x) + Dimensions.y / 2 > Constants.PIECES_POSITION_LIMITS.x)
                {
                    position = transform.position;
                    position.x = Mathf.Clamp(position.x, -Constants.PIECES_POSITION_LIMITS.x + Dimensions.y / 2, Constants.PIECES_POSITION_LIMITS.x - Dimensions.y / 2);
                    transform.position = position;
                }
                break;
        }
    }

    public void SetPool(IObjectPool<PieceController> piecesPool)
    {
        this.piecesPool = piecesPool;
    }

    public virtual void Reset()
    {
        movementDirection = MovementDirection.None;
        rb.velocity = new Vector2(0, pieceConfig.YSpeed);
        hasBeenPlaced = false;
        hasFallenOffMap = false;
        transform.rotation = Quaternion.identity;

        SetPieceLayer(Layers.PIECES_LAYER);
        ResetPhysics();
    }

    private void ResetPhysics()
    {
        rb.mass = Constants.INITIAL_PIECE_MASS;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.gravityScale = 0;
    }
}
