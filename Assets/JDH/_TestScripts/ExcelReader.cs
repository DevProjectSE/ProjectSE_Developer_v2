using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelReader : MonoBehaviour
{
    private TextAsset csvData; // CSV 데이터를 저장할 변수
    private string[] rows;     // 행 데이터를 저장할 배열

    // Start is called before the first frame update
    void Start()
    {
        // 'example'은 파일 이름 (확장자는 제외)
        csvData = Resources.Load<TextAsset>("시나리오 대사_25_01_06");
        if (csvData != null)
        {
            rows = csvData.text.Split('\n'); // 데이터를 행 단위로 분리
            foreach (var row in rows)
            {
                Debug.Log(row); // 각 행의 데이터를 출력
            }
        }
        else
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다. Resources 폴더에 'example.csv' 파일이 있는지 확인하세요.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 필요 시 Update 로직 추가
    }
}
