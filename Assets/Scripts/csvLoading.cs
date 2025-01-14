using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class csvLoading : MonoBehaviour
{
    private List<List<string>> csvData = new List<List<string>>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("example");
        if(csvFile != null )
        {
            Debug.Log("파일있음");
            // '\n' --> 줄바꿈
            string[] rows = csvFile.text.Split('\n'); // Split : \n 을 기준으로 나눠(분리해)준다
            
            foreach( string row in rows )
            {
                string[] fields = row.Split(',');
                List<string> rowData = new List<string>(fields);
                csvData.Add(rowData);
            }

            int row_num = 0;
            foreach(List<string> row in csvData)
            {
                Debug.Log("[" + (row_num+1) + "]행");
                int field_num = 0;
                foreach(string field in row)
                {
                    switch(field_num)
                    {
                        case 0: Debug.Log("1열 " + int.Parse(field)); break;
                        case 1: Debug.Log("2열 " + int.Parse(field)); break;
                        case 2: Debug.Log("3열 " + float.Parse(field)); break;
                        case 3: Debug.Log("4열 " + float.Parse(field)); break;
                    }
                    field_num++;
                }
                row_num++;
            }
        }
        else
        {
            Debug.Log("파일 없다요");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
