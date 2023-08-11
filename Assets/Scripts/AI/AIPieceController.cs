public class AIPieceController : PieceController
{
    private void Start()
    {
        AIManager.Instance.SetActivePiece(this);
    }
}
