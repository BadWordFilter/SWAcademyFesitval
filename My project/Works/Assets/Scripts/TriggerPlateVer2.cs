using UnityEngine;
using System.Collections; // 코루틴 사용
//박철웅
public class TriggerPlateVer2 : MonoBehaviour
{
    public float requiredSizeMin = 13f;
    public float requiredSizeMax = 28f;

    public GameObject gravityCube; 

    private void Start()
    {
        if (gravityCube != null)
        {
            // start() 끝난 후 1프레임 기다리고 꺼줌 → Start() 내부 로직 보장
            StartCoroutine(DisableAfterStart(gravityCube));
        }
        
    }
    private IEnumerator DisableAfterStart(GameObject cube)
    {
        yield return null; // 1프레임 대기 → gravityCube의 Start() 먼저 실행되도록 보장
        cube.SetActive(false);
        Debug.Log("🔻 중력큐브 비활성화됨");
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 scale = other.transform.localScale;
        float maxSize = Mathf.Max(scale.x, scale.y, scale.z);

        Debug.Log("감지된 오브젝트: " + other.name);
        Debug.Log("측정된 최대 축 크기: " + maxSize);

        if (maxSize >= requiredSizeMin && maxSize <= requiredSizeMax && gravityCube != null)
        {
            gravityCube.SetActive(true); 
            
        }
    }
}
