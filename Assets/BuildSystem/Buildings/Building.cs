using UnityEngine;

public class Building : MonoBehaviour
{
    public enum Rotation
    {
        Degree0,
        Degree90,
        Degree180,
        Degree270
    }

    public SpriteRenderer spriteRenderer;

    // leave this null if the building is not rotatable
    [SerializeField] protected Transform rotationAnchor;

    public void SetTint(Color tint)
    {
        spriteRenderer.color = tint;
    }

    public virtual void Init()
    { }

    public virtual void SetRenderingOrder(int sortingLayerId, int sortingOrder)
    {
        spriteRenderer.sortingLayerID = sortingLayerId;
        spriteRenderer.sortingOrder = sortingOrder;
    }

    public virtual Rotation GetNextRotation(Rotation rotation, bool clockWise)
    {
        if (rotationAnchor == null)
            return Rotation.Degree0;

        return rotation switch
        {
            Rotation.Degree0 => clockWise ? Rotation.Degree90 : Rotation.Degree270,
            Rotation.Degree90 => clockWise ? Rotation.Degree180 : Rotation.Degree0,
            Rotation.Degree180 => clockWise ? Rotation.Degree270 : Rotation.Degree90,
            Rotation.Degree270 => clockWise ? Rotation.Degree0 : Rotation.Degree180,
            _ => Rotation.Degree0,
        };
    }

    public virtual void Rotate(BuildingData buildingData, Rotation rotation)
    {
        if (rotationAnchor == null)
            return;

        switch (rotation)
        {
            default:
            case Rotation.Degree0:
                rotationAnchor.eulerAngles = Vector3.zero;
                rotationAnchor.position = Vector3.zero;
                break;
            case Rotation.Degree90:
                rotationAnchor.eulerAngles = new Vector3(0, 0, -90);
                rotationAnchor.position = new Vector3(0, buildingData.cellCount.x, 0);
                break;
            case Rotation.Degree180:
                rotationAnchor.eulerAngles = new Vector3(0, 0, -180);
                rotationAnchor.position = new Vector3(buildingData.cellCount.x, buildingData.cellCount.y, 0);
                break;
            case Rotation.Degree270:
                rotationAnchor.eulerAngles = new Vector3(0, 0, -270);
                rotationAnchor.position = new Vector3(buildingData.cellCount.y, 0, 0);
                break;
        }
    }
}
