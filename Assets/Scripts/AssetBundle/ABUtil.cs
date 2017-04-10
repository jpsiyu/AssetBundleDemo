using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ABUtil  {
    private static string yellowCode = "#FFFF00";

    public static void Log(params string[] logs) {
        for (int i = 0; i < logs.Length; i++) {
            Debug.Log(string.Format("<color={0}>ABFlow: {1}</color>", yellowCode, logs[i]));
        }
    }

    /// <summary>
    /// 读取Json对比文件
    /// </summary>
    public static ABHashCollection ReadABCompareFile()
    {
        ABHashCollection abHashCollection = null;
        string filePath = Path.Combine(Application.dataPath, ABGlobal.abCompareFile);

        try
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            StreamReader streamReader = new StreamReader(fs);

            string jsonStr = streamReader.ReadToEnd();
            abHashCollection = JsonUtility.FromJson<ABHashCollection>(jsonStr);

            streamReader.Close();
            fs.Close();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        return abHashCollection;
    }
}
