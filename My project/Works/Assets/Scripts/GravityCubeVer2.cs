using UnityEngine;
//박철웅
public class GravityCubeVer2 : MonoBehaviour
{
    public float maxInteractDistance = 80f;

    public GameObject invisiblePlaneParent;       
    public GameObject invisibleWallParent;


    public Material clickableMaterial;    // 클릭 가능 상태
    public Material transparentMat;  //중력 아래일때 (투명 )   
    public Material outlineMat;     // 중력 위일때 ( 트리거 메테리얼 )
    void Start()
    {
        if (invisiblePlaneParent != null)
        {
            invisiblePlaneParent.SetActive(false); // 게임 시작 시 Invisible Plane 끄끼

            //gameObject.SetActive(false); 

        }


    }

    
    // 중력큐브 클릭시
    private void OnMouseDown()
    {


        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) return;

        //  메테리얼 기준으로 비교
        if (renderer.sharedMaterial == clickableMaterial)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > maxInteractDistance)
                {
                    Debug.Log("⛔ 너무 멈: " + distance + "m");
                    return;
                }

                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    bool isCurrentlyFlipped = Vector3.Angle(Physics.gravity.normalized, Vector3.up) < 5f;
                    float gravityStrength = 92.5f;
                    Physics.gravity = isCurrentlyFlipped ? Vector3.down * gravityStrength : Vector3.up * gravityStrength;

                    player.transform.rotation = Quaternion.Euler(0f, player.transform.eulerAngles.y, isCurrentlyFlipped ? 0f : 180f);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.AddForce((isCurrentlyFlipped ? Vector3.down : Vector3.up) * 1f, ForceMode.Impulse);

                    FirstPersonController controller = player.GetComponent<FirstPersonController>();
                    if (controller != null)
                    {
                        controller.ForceUnground();
                    }

                    //  Invisible Plane on/off
                    if (invisiblePlaneParent != null)
                    {
                        bool activatePlanes = !isCurrentlyFlipped;
                        invisiblePlaneParent.SetActive(activatePlanes);
                        Debug.Log("invisiblePlaneParent SetActive(" + activatePlanes + ")");
                    }

                    //  벽 머테리얼 변경
                    if (invisibleWallParent != null)
                    {
                        Material targetMat = isCurrentlyFlipped ? transparentMat : outlineMat;
                        foreach (Transform wall in invisibleWallParent.transform)
                        {
                            Renderer rend = wall.GetComponent<Renderer>();
                            if (rend != null)
                            {
                                rend.sharedMaterial = targetMat;
                            }
                        }

                        
                    }
                }
            }
        }
        else
        {
            
        }
    }
}
