//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////������
//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    public DoorController[] doors;
//    public int[] StateCount;

//    private int currentStage = 1;
//    private int activeSensorCount = 0;

//    // ���� ó���� ������ ����� ����
//    private HashSet<ColorSensor> activatedSensors = new HashSet<ColorSensor>();

//    private void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    public void NotifySensorActivated(ColorSensor sensor)
//    {
//        if (currentStage < 1 || currentStage > StateCount.Length)
//            return;

//        // �̹� ó���� ������� ����
//        if (activatedSensors.Contains(sensor)) return;

//        activatedSensors.Add(sensor); // �� ������ ó��
//        activeSensorCount++;

//        //Debug.Log($"{sensor.name} ���� ���� -> �� ���� ��: {activeSensorCount}");

//        if (activeSensorCount >= StateCount[currentStage - 1])
//        {
//            OpenCurrentStageDoor();

//            //Debug.Log($"Stage {currentStage} Ŭ����!");

//            currentStage++;
//            activeSensorCount = 0;
//            activatedSensors.Clear(); // ���� �������� �غ�
//        }
//    }

//    private void OpenCurrentStageDoor()
//    {
//        int index = currentStage - 1;
//        if (index >= 0 && index < doors.Length && doors[index] != null)
//        {
//            doors[index].OpenDoor();
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ������
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentStage = 1;  // ���� ���� ���� �������� (1���� ����)
    public DoorController[] doors; // �� �������� ����
    public int[] StateCount;       // �� ���������� �䱸�ϴ� ���� ���� ��

    private int activeSensorCount = 0;

    private HashSet<ColorSensor> activatedSensors = new HashSet<ColorSensor>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // ������ ������ �� ȣ���
    public void NotifySensorActivated(ColorSensor sensor)
    {
        if (currentStage < 1 || currentStage > StateCount.Length)
            return;

        // ���� ���������� ������ ���� ���������� �ٸ��� ����
        if (sensor.stageNumber != currentStage)
            return;

        // �̹� ó���� ������� ����
        if (activatedSensors.Contains(sensor)) return;

        activatedSensors.Add(sensor);
        activeSensorCount++;

        if (activeSensorCount >= StateCount[currentStage - 1])
        {
            OpenCurrentStageDoor();

            currentStage++;
            activeSensorCount = 0;
            activatedSensors.Clear();
        }
    }

    // ���� �������� �� ����
    private void OpenCurrentStageDoor()
    {
        int index = currentStage - 1;
        if (index >= 0 && index < doors.Length && doors[index] != null)
        {
            doors[index].OpenDoor();
        }
    }

    // ���� �������� ��ȣ�� �ܺο��� �� �� �ֵ��� Getter �߰�
    public int GetCurrentStage()
    {
        return currentStage;
    }
}
