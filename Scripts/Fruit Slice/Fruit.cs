using UnityEngine;


namespace FruitSlice
{
    public class Fruit : MonoBehaviour
    {
        private Rigidbody rigidBody;
        private Collider col;
        private MeshRenderer meshRenderer;
        private Vector3 currPos;
        private Vector3 deltaVector;
        private Vector3 prevPos;
        private FruitGoreSplash splasher;

        public GameObject slicedVersion;
        public float points = 100;


        void Start()
        {
            SetInitialReferences();
            prevPos = transform.position;
        }

        void Update()
        {
            currPos = transform.position;
            deltaVector = currPos - prevPos;
            // Debug.DrawRay(prevPos, deltaVector, Color.white, 10f);

            prevPos = transform.position;
        }

        void SetInitialReferences()
        {
            rigidBody = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            meshRenderer = GetComponent<MeshRenderer>();
            splasher = GetComponent<FruitGoreSplash>();
        }

        public void Slice()
        {
            if(AppManager_FruitSlice.Instance.gamePlay)
            {
                GameObject go = Instantiate(slicedVersion, this.transform.position, Quaternion.identity);

                for (int i = 0; i < 2; i++)
                {
                    splasher.SplashGore(go.transform.GetChild(i), deltaVector);
                }

                FruitDataManager.Instance.AddScore(points);
                FruitSoundManager.Instance.FruitTouchSound();

                Destroy(this.gameObject);
            }
            
        }
    }
}