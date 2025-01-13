using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelReader : MonoBehaviour
{
    private TextAsset csvData; // CSV �����͸� ������ ����
    private string[] rows;     // �� �����͸� ������ �迭

    // Start is called before the first frame update
    void Start()
    {
        // 'example'�� ���� �̸� (Ȯ���ڴ� ����)
        csvData = Resources.Load<TextAsset>("�ó����� ���_25_01_06");
        if (csvData != null)
        {
            rows = csvData.text.Split('\n'); // �����͸� �� ������ �и�
            foreach (var row in rows)
            {
                Debug.Log(row); // �� ���� �����͸� ���
            }
        }
        else
        {
            Debug.LogError("CSV ������ ã�� �� �����ϴ�. Resources ������ 'example.csv' ������ �ִ��� Ȯ���ϼ���.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �ʿ� �� Update ���� �߰�
    }
}
