using Assets.Scripts.Player;
using UnityEngine;

public class VerticalMouseLook : MonoBehaviour
{
    public PlayerController playerController;

    private void Update()
    {
        var newRotation = transform.rotation.eulerAngles;
        newRotation += GetRotationDir();

        // Prevent player from looking too high / low
        newRotation.x = (newRotation.x > 180) ? newRotation.x - 360 : newRotation.x;
        newRotation.x = Mathf.Clamp(newRotation.x, -playerController.maxAngle, playerController.maxAngle);

        transform.rotation = Quaternion.Euler(newRotation);
    }
    
    /// <summary>
    /// Handles vertical rotation for camera movement.
    /// </summary>
    Vector3 GetRotationDir()
    {
        return new Vector3(
            -Input.GetAxis("Mouse Y") * playerController.mouseSensitivity,
            0,
            0
        );
    }
}
