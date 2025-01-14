using System.Collections.Generic;
using UnityEngine;

public class ExcelReader : MonoBehaviour
{
    // 데이터를 저장할 클래스
    [System.Serializable]
    public class ScenarioData
    {
        public string CharacterName; // 캐릭터 이름
        public string Dialogue;      // 대사
        public string Emotion;       // 감정 (예: 웃음, 화남 등)
    }

    private List<ScenarioData> scenarioList = new List<ScenarioData>(); // 구조화된 데이터를 저장할 리스트

    // Start is called before the first frame update
    void Start()     
    {
        TextAsset csvData = Resources.Load<TextAsset>("시나리오 대사_25_01_06");

        if (csvData != null)
        {
            string[] rows = csvData.text.Split('\n');
            foreach (var row in rows)
            {
                string[] columns = row.Split(',');

                // 데이터 유효성 검증 (열 개수 확인)
                if (columns.Length >= 3)
                {
                    ScenarioData data = new ScenarioData
                    {
                        CharacterName = columns[1].Trim(),
                        Dialogue = columns[2].Trim(),
                        Emotion = columns[3].Trim()
                    };
                    scenarioList.Add(data);
                }
            }

            // 구조화된 데이터 출력
            foreach (var scenario in scenarioList)
            {
                Debug.Log($"캐릭터: {scenario.CharacterName}, 대사: {scenario.Dialogue}, 감정: {scenario.Emotion}");
            }
        }
        else
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다. Resources 폴더에 파일이 있는지 확인하세요.");
        }
    }
}
