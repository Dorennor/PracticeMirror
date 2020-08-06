using UnityEngine;

namespace Assets.L_FG_Assets_Pack_Lite.Demo
{
    public class cameraMove : MonoBehaviour
    {
        private readonly float rSpeed = 3.0f;
        private readonly float mSpeed = 20.0f;
        private float X;
        private float Y;

        private void Update()
        {
            X += Input.GetAxis("Mouse X") * rSpeed;
            Y += Input.GetAxis("Mouse Y") * rSpeed;
            transform.localRotation = Quaternion.AngleAxis(X, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(Y, Vector3.left);
            transform.position += transform.forward * mSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * mSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
    }
}