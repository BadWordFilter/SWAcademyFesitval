//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////정다현
//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    public DoorController[] doors;
//    public int[] StateCount;

//    private int currentStage = 1;
//    private int activeSensorCount = 0;

//    // 정답 처리된 센서를 기록할 집합
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

//        // 이미 처리된 센서라면 무시
//        if (activatedSensors.Contains(sensor)) return;

//        activatedSensors.Add(sensor); // 새 센서만 처리
//        activeSensorCount++;

//        //Debug.Log($"{sensor.name} 센서 정답 -> 총 맞춘 수: {activeSensorCount}");

//        if (activeSensorCount >= StateCount[currentStage - 1])
//        {
//            OpenCurrentStageDoor();

//            //Debug.Log($"Stage {currentStage} 클리어!");

//            currentStage++;
//            activeSensorCount = 0;
//            activatedSensors.Clear(); // 다음 스테이지 준비
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
// 정다현
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentStage = 1;  // 현재 진행 중인 스테이지 (1부터 시작)
    public DoorController[] doors; // 각 스테이지 문들
    public int[] StateCount;       // 각 스테이지가 요구하는 정답 센서 수

    private int activeSensorCount = 0;

    private HashSet<ColorSensor> activatedSensors = new HashSet<ColorSensor>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 센서가 정답일 때 호출됨
    public void NotifySensorActivated(ColorSensor sensor)
    {
        if (currentStage < 1 || currentStage > StateCount.Length)
            return;

        // 현재 스테이지와 센서가 속한 스테이지가 다르면 무시
        if (sensor.stageNumber != currentStage)
            return;

        // 이미 처리된 센서라면 무시
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

    // 현재 스테이지 문 열기
    private void OpenCurrentStageDoor()
    {
        int index = currentStage - 1;
        if (index >= 0 && index < doors.Length && doors[index] != null)
        {
            doors[index].OpenDoor();
        }
    }

    // 현재 스테이지 번호를 외부에서 알 수 있도록 Getter 추가
    public int GetCurrentStage()
    {
        return currentStage;
    }
}
