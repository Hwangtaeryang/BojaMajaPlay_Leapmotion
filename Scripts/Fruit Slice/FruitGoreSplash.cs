using UnityEngine;

    public class FruitGoreSplash : MonoBehaviour
    {
        [SerializeField] private float splashForce = 100f;
        [SerializeField] private float splashForceFluctuation = 100f;
        [SerializeField] private float rotationForce = 50f;
        [SerializeField] private float rotationForceFluctuation = 50f;
        [SerializeField] private float maxPercentAngleDivergenceFromUp = 20f;
        private Collider col;

        void Start()
        {
            maxPercentAngleDivergenceFromUp *= 0.01f;
        }

        public void SplashGore(Transform fruit)
        {
            col = fruit.GetComponent<Collider>();

            //find random x and z coordinate on the collider
            Vector3 randomBoundsPoint = new Vector3(Random.Range(col.bounds.center.x - col.bounds.extents.x, col.bounds.center.x + col.bounds.extents.x), transform.position.y, transform.position.z);

            //get up direction from randomBoundsPoint
            Vector3 dirUp = new Vector3(randomBoundsPoint.x, randomBoundsPoint.y + 2f, randomBoundsPoint.z) - randomBoundsPoint;
            Debug.DrawRay(randomBoundsPoint, dirUp, Color.white, 2f);

            //get a random point in a sphere around the randomBoundsPoint
            Vector3 randomCoord = randomBoundsPoint + (Vector3)Random.insideUnitCircle;
            Debug.DrawRay(randomBoundsPoint, randomCoord - randomBoundsPoint, Color.cyan, 2f);

            float angle = Vector3.Angle(dirUp, randomCoord - randomBoundsPoint);
            //get the final direction from up direction to random direction
            Vector3 randomDir = Vector3.RotateTowards(dirUp, randomCoord - randomBoundsPoint, angle * maxPercentAngleDivergenceFromUp * Mathf.Deg2Rad, 0f);
            Debug.DrawRay(randomBoundsPoint, randomDir, Color.yellow, 2f);

            fruit.rotation = Random.rotation;

            fruit.GetComponent<Rigidbody>().AddTorque(randomDir.normalized * Random.Range(rotationForce - rotationForceFluctuation, rotationForce + rotationForceFluctuation));

            fruit.GetComponent<Rigidbody>().AddForce(randomDir.normalized * Random.Range(splashForce - (angle * 0.5f) - splashForceFluctuation, splashForce - (angle * 0.5f) + splashForceFluctuation));

        }
        public void SplashGore(Transform fruit, Vector3 direction)
        {
            Debug.DrawRay(transform.position, direction * 10f, Color.cyan, 10f);
            col = fruit.GetComponent<Collider>();

            Vector3 newForce = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f) - (direction * 10f);

            fruit.rotation = Random.rotation;

            fruit.GetComponent<Rigidbody>().AddTorque(newForce * splashForce);

            fruit.GetComponent<Rigidbody>().AddForce(newForce * splashForce);

        }
    }

