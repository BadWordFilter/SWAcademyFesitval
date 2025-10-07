using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//서우진
public class SaveResetter : MonoBehaviour
{
    // 인스펙터 우클릭 메뉴에서도 실행 가능
    [ContextMenu("저장 초기화")]
    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("저장 데이터 초기화 완료!");
    }

    // 버튼에서 직접 연결할 수 있게 public 메서드 추가
    public void OnResetButtonClick()
    {
        ResetAllProgress();
    }
}