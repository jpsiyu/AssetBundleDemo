using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class ABChecker {
    private ABHashCollection mServerHashCollection;
    private ABHashCollection mClientHashCollection;
    private List<string> mModifyABs;

    public List<string> ModifyABs
    {
        get { return mModifyABs; }
    }

    public IEnumerator StartCheck()
    {
        string url = ABGlobal.abURL + ABGlobal.abCompareFileName;
        using (WWW www = new WWW(url)) {
            yield return www;
            if (www.error != null)
                throw new System.Exception(string.Format("WWW downad error:{0}, url:{1}", www.error, url));

            GenServerJsonCollection(www.bytes);
            GenClientJsonCollection();
            CalModifyABs();
        }
    }

    private void CalModifyABs() {
        mModifyABs = mClientHashCollection.CalModifyAB(mServerHashCollection);
    }

    private void GenServerJsonCollection(byte[] jsonBytes) {
        //网页下载下来的字符串前3个字节是Coding，例子如下
        //http://answers.unity3d.com/questions/844423/wwwtext-not-reading-utf-8-text.html
        string jsonStr = System.Text.Encoding.UTF8.GetString(jsonBytes, 3, jsonBytes.Length - 3);
        mServerHashCollection = JsonUtility.FromJson<ABHashCollection>(jsonStr);
    }

    private void GenClientJsonCollection() {
        mClientHashCollection =  ReadABCompareFile();
        
    }

    /// <summary>
    /// 读取Json对比文件
    /// </summary>
    public ABHashCollection ReadABCompareFile()
    {
        ABHashCollection abHashCollection = new ABHashCollection();
        string filePath = ClientCompareFilePath();

        if (!File.Exists(filePath))
            return abHashCollection;

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
            ABUtil.Log(e.Message);
        }
        return abHashCollection;
    }

    private string ClientCompareFilePath()
    {
        return Path.Combine(Application.persistentDataPath, ABGlobal.abCompareFileName);
    }


    /// <summary>
    /// 将服务器的Json更新到客户端
    /// </summary>
    /// <param name="text"></param>
    public void UpdateClientJsonFile() {
        string jsonStr = JsonUtility.ToJson(mServerHashCollection);
        try {
            string filePath = ClientCompareFilePath();
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.Write(jsonStr);
            streamWriter.Close();
            fs.Close();
            ABUtil.Log("Update Client Json Compare File Success");
        }
        catch (System.Exception e) {
            throw new System.Exception(e.Message);
        }
    }
}
