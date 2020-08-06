using UnityEngine;

namespace Assets.Knight.scripts
{
    public class rotator : MonoBehaviour
    {
        public float speed;
        public Vector3 direction = Vector3.zero;// Use this for initialization

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(direction * Time.deltaTime * speed, Space.World);
        }
    }
}