using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Collider col;
    private MeshRenderer meshRenderer;
    private Vector3 currPos;
    private Vector3 deltaVector;
    private Vector3 prevPos;

    public string[] missSfx;

    void Start()
    {
        SetInitialReferences();
        //prevPos = transform.position;
    }

    //void Update()
    //{
    //    currPos = transform.position;

    //    deltaVector = currPos - prevPos;

    //    prevPos = transform.position;
    //}

    void SetInitialReferences()
    {
        rigidBody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision _col)
    {
        GameObject go;

        if (LayerMask.LayerToName(_col.gameObject.layer) == "Animals")
        //if (_col.gameObject.CompareTag("Animal"))
        {
            // 돈
            go = Instantiate(CatchPang_DataManager.Instance.hitParticles);

            // 파티클 객체 위치
            go.transform.SetParent(this.transform);
            // 파티클 게임상 위치
            go.transform.position = this.transform.position;

            this.col.enabled = false;
            rigidBody.isKinematic = true;
            meshRenderer.enabled = false;

            Destroy(gameObject, 2f);    // 파티클 생성하고, 2초 뒤에 사라짐
        }
        else if (LayerMask.LayerToName(_col.gameObject.layer) == "Geometry")
        {
            go = Instantiate(CatchPang_DataManager.Instance.missParticles);

            // 공이 미스나면 소리
            CatchPang_SoundManager.Instance.PlaySE(missSfx[Random.Range(0, 2)]);

            // 파티클 객체 위치
            go.transform.SetParent(this.transform);
            // 파티클 게임상 위치
            go.transform.position = this.transform.position;

            this.col.enabled = false;
            rigidBody.isKinematic = true;
            meshRenderer.enabled = false;

            Destroy(gameObject, 2f);
        }
        //else if (_col.collider.gameObject.CompareTag("L_Hand") || _col.collider.gameObject.CompareTag("R_Hand") || _col.collider.gameObject.CompareTag("Ball") || _col.collider.gameObject.CompareTag("Table"))
        //{
        //    // 돈이 아닌 다른 충돌

        //    return;
        //}
        //else
        //{
        //    // 연기
        //    go = Instantiate(CatchPang_DataManager.Instance.missParticles);

        //    // 공이 미스나면 소리
        //    CatchPang_SoundManager.Instance.PlaySE(missSfx[Random.Range(0, 2)]);

        //    // 파티클 객체 위치
        //    go.transform.SetParent(this.transform);
        //    // 파티클 게임상 위치
        //    go.transform.position = this.transform.position;

        //    Destroy(gameObject, 2f);
        //}

        //this.col.enabled = false;
        //rigidBody.isKinematic = true;
        //meshRenderer.enabled = false;


        //Destroy(gameObject, 2f);
    }


    private void EnableGravity()
    {
        rigidBody.isKinematic = false;
    }

    public void Throw(Vector3 forceVector, float strength)
    {
        EnableGravity();


        Debug.DrawRay(Camera.main.transform.position, forceVector, Color.magenta, 10f);
        Debug.DrawRay(prevPos, deltaVector, Color.white, 10f);

        Debug.DrawRay(Camera.main.transform.position, forceVector + (deltaVector * 3f), Color.cyan, 10f);


        rigidBody.AddForce((forceVector + (deltaVector * 3f)).normalized * strength, ForceMode.Impulse);
    }
}