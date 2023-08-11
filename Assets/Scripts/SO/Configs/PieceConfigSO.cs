using UnityEngine;

[CreateAssetMenu(fileName = "New Piece Config", menuName = "ScriptableObjects/PieceConfigSO")]
public class PieceConfigSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite pieceSprite;
    public Vector2Int dimensions;
    public float Mass;

    public float XSpeed { get; private set; }
    public float YSpeed { get; private set; }
    public float Drag { get; private set; }
    public float AngularDrag { get; private set; }
    public float GravityScale { get; private set; }

    public void SetupValues(float XSpeed, float YSpeed, float Drag, float AngularDrag, float GravityScale)
    {
        this.XSpeed = XSpeed;
        this.YSpeed = YSpeed;
        this.Drag = Drag;
        this.AngularDrag = AngularDrag;
        this.GravityScale = GravityScale;
    }
}