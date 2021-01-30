using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerRotationModule : BasePlayerModule
    {
        public override void Update(PlayerController parent, float deltaTime)
        {
            parent.transform.rotation = 
                Quaternion.Euler(parent.transform.rotation.eulerAngles + 
                    GetRotationDir(parent.mouseSensitivity, deltaTime));
        }

        /// <summary>
        /// Handles horizontal rotation for player movement.
        /// </summary>
        Vector3 GetRotationDir(float mouseSensitivity, float deltaTime)
        {
            return new Vector3(
                0,
                Input.GetAxis("Mouse X") * mouseSensitivity,
                0
            ) * Time.fixedDeltaTime * 100;
        }
    }
}
