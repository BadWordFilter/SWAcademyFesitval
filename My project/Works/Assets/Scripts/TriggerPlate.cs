    using UnityEngine;
//박철웅
    public class TriggerPlate : MonoBehaviour
    {
        public float requiredSizeMin = 33f;
        public float requiredSizeMax = 58f;  // 큐브 크기 조건
        public string doorObjectName = "Door_1_White (can't touch)";

        public Material gravityCubeTargetMaterial;


        //발판 작동됐을 때
        private void OnTriggerEnter(Collider other)
        {

            
            Vector3 scale = other.transform.localScale;
            float maxSize = Mathf.Max(scale.x, scale.y, scale.z);

            Debug.Log("감지된 오브젝트: " + other.name);
            Debug.Log("측정된 최대 축 크기: " + maxSize);

            if (maxSize >= requiredSizeMin && maxSize <= requiredSizeMax)
            {
                GameObject doorParent = GameObject.Find(doorObjectName);
                if (doorParent != null)
                {
                    Transform doorChild = doorParent.transform.Find("Door");
                    if (doorChild != null)
                    {
                        doorChild.gameObject.SetActive(false);

                    }

                    // GravityCube -> 활성화상태로
                    GameObject gravityCube = GameObject.FindGameObjectWithTag("GravityCube");
                    if (gravityCube != null)
                    {
                        Renderer cubeRenderer = gravityCube.GetComponent<Renderer>();
                        if (cubeRenderer != null)
                        {
                            cubeRenderer.material = gravityCubeTargetMaterial;

                    }
                }

                }
            }
        }
    }