using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        public void ApplyRotation()
        {
            transform.rotation =
                Quaternion.Euler(transform.rotation.eulerAngles +
                    GetHorizontalRotationDir(mouseSensitivity, Time.deltaTime));

            // TODO: Vertical rotation
        }

        /// <summary>
        /// Handles horizontal rotation for player movement.
        /// </summary>
        Vector3 GetHorizontalRotationDir(float mouseSensitivity, float deltaTime)
        {
            return new Vector3(
                0,
                Input.GetAxis("Mouse X") * mouseSensitivity,
                0
            ) * Time.fixedDeltaTime * 100;
        }

        /// <summary>
        /// Handles vertical rotation for player movement.
        /// </summary>
        Vector3 GetVerticalRotationDir(float mouseSensitivity, float deltaTime)
        {
            return new Vector3(
                Input.GetAxis("Mouse Y") * mouseSensitivity,
                0,
                0
            ) * Time.fixedDeltaTime * 100;
        }
    }
}
