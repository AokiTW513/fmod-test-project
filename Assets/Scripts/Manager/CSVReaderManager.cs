using System.IO;
using UnityEngine;

public class CSVReaderManager : MonoBehaviour
{
    public static CSVReaderManager instance { get; private set; }
    
    private string folderPath = Path.Combine(Application.dataPath, "CSV");

    private string[][] csvData;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found another CSVReaderManager in this scene.");
            return;
        }

        instance = this;

        //如果沒資料夾，就幫他建一個
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    private string[][] ReadCSV(string fileName)
    {
        string filePath = Path.Combine(folderPath, fileName);

        if(File.Exists(filePath))
        {
            string[] dataLines = File.ReadAllLines(filePath);
            string[][] rowData = new string[dataLines.Length][];

            for (int i = 0; i < dataLines.Length; i++)
            {
                rowData[i] = dataLines[i].Split(',');
            }

            return rowData;
        }
        else
        {
            Debug.LogError("CSV file not found at: " + filePath);
            return null;
        }
    }
}
