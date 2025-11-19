using UnityEngine;

public class Windmill : Building
{
    public float rotationSpeed = 5;

    private void FixedUpdate()
    {
        // reuse rotation anchor because we need to set it to allow rotation
        rotationAnchor.Rotate(new Vector3(0, 0, rotationSpeed * Time.fixedDeltaTime));
    }

    public override Rotation GetNextRotation(Rotation rotation, bool _clockWise)
    {
        return rotation switch
        {
            Rotation.Degree0 => Rotation.Degree180,
            Rotation.Degree180 => Rotation.Degree0,
            _ => Rotation.Degree0,
        };
    }

    public override void Rotate(BuildingData _buildingData, Rotation rotation)
    {
        if (rotationAnchor == null)
            return;

        switch (rotation)
        {
            default:
            case Rotation.Degree0:
                rotationSpeed = Mathf.Abs(rotationSpeed);
                break;
            case Rotation.Degree180:
                rotationSpeed = -Mathf.Abs(rotationSpeed);
                break;
        }
    }
}
