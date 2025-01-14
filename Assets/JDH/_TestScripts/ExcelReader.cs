using System.Collections.Generic;
using UnityEngine;

public class ExcelReader : MonoBehaviour
{
    // �����͸� ������ Ŭ����
    [System.Serializable]
    public class ScenarioData
    {
        public string CharacterName; // ĳ���� �̸�
        public string Dialogue;      // ���
        public string Emotion;       // ���� (��: ����, ȭ�� ��)
    }

    private List<ScenarioData> scenarioList = new List<ScenarioData>(); // ����ȭ�� �����͸� ������ ����Ʈ

    // Start is called before the first frame update
    void Start()     
    {
        TextAsset csvData = Resources.Load<TextAsset>("�ó����� ���_25_01_06");

        if (csvData != null)
        {
            string[] rows = csvData.text.Split('\n');
            foreach (var row in rows)
            {
                string[] columns = row.Split(',');

                // ������ ��ȿ�� ���� (�� ���� Ȯ��)
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

            // ����ȭ�� ������ ���
            foreach (var scenario in scenarioList)
            {
                Debug.Log($"ĳ����: {scenario.CharacterName}, ���: {scenario.Dialogue}, ����: {scenario.Emotion}");
            }
        }
        else
        {
            Debug.LogError("CSV ������ ã�� �� �����ϴ�. Resources ������ ������ �ִ��� Ȯ���ϼ���.");
        }
    }
}
