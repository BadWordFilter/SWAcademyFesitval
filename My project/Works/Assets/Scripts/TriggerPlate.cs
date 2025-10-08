    using UnityEngine;
//��ö��
    public class TriggerPlate : MonoBehaviour
    {
        public float requiredSizeMin = 33f;
        public float requiredSizeMax = 58f;  // ť�� ũ�� ����
        public string doorObjectName = "Door_1_White (can't touch)";

        public Material gravityCubeTargetMaterial;


        //���� �۵����� ��
        private void OnTriggerEnter(Collider other)
        {

            
            Vector3 scale = other.transform.localScale;
            float maxSize = Mathf.Max(scale.x, scale.y, scale.z);

            Debug.Log("������ ������Ʈ: " + other.name);
            Debug.Log("������ �ִ� �� ũ��: " + maxSize);

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

                    // GravityCube -> Ȱ��ȭ���·�
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