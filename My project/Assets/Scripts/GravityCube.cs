using UnityEngine;
//박철웅
public class GravityCube : MonoBehaviour
{
    public float maxInteractDistance = 80f;

    public Material clickableMaterial; // 작동 가능한 메테리얼
    public Material unclickableMaterial; // 클릭 후 바꿀 메테리얼

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
                    Debug.Log(" 너무 멈: " + distance + "m");
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

                    // 클릭 후 비활성화 메테리얼로 교체
                    renderer.sharedMaterial = unclickableMaterial;
                }
            }
        }
    }
}
