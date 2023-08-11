using System.Collections;
using UnityEngine;

public class AIManager : Singleton<AIManager>
{
    [SerializeField] private LayerMask landedPiecesLayerMask;

    private AIPieceController aiPiece;
    private Coroutine coroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetActivePiece(AIPieceController aiPiece)
    {
        this.aiPiece = aiPiece;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }


        coroutine = StartCoroutine(SetRandomAction());
    }

    /// <summary>
    /// Sets the piece's action based on a pseudo random algorithm. If the piece is above a landed piece
    /// or the tower base it will try to stay in that area by stopping itself and then moving to the side
    /// that is further from the floor
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetRandomAction()
    {
        if (aiPiece.HasBeenPlaced)
        {
            yield return null;
        }

        while (true)
        {
            float randomTime = Random.Range(0.2f, 1f);
            int action = Random.Range(0, 5);

            RaycastHit2D ray = Physics2D.Raycast(aiPiece.transform.position, Vector2.down, 200, landedPiecesLayerMask);

            if (ray.rigidbody == null)
            {
                aiPiece.StopMove();

                MovementDirection direction = aiPiece.transform.position.x > 0 ? MovementDirection.Left : MovementDirection.Right;
                aiPiece.Move(direction);
                yield return new WaitForSeconds(randomTime);
            }

            switch (action)
            {
                case 0:
                    aiPiece.Move(MovementDirection.Right);
                    break;
                case 1:
                    aiPiece.Move(MovementDirection.Left);
                    break;
                case 2:
                    aiPiece.StopMove();
                    break;
                case 3:
                    aiPiece.Rotate(MovementDirection.Right);
                    break;
                case 4:
                    aiPiece.Rotate(MovementDirection.Left);
                    break;
            }
            yield return new WaitForSeconds(randomTime);
        }
    }
}
