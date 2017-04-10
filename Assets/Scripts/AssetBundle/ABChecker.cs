using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class ABChecker : MonoBehaviour {
    private ABHashCollection mServerHashCollection;
    private ABHashCollection mClientHashCollection;

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
            //UpdateClientJsonFile(www.text);
        }
    }

    private void CalModifyABs() {
        List<string> modifyABs = mClientHashCollection.CalModifyAB(mServerHashCollection);
    }

    private void GenServerJsonCollection(byte[] jsonBytes) {
        //网页下载下来的字符串前3个字节是Coding，例子如下
        //http://answers.unity3d.com/questions/844423/wwwtext-not-reading-utf-8-text.html
        string jsonStr = System.Text.Encoding.UTF8.GetString(jsonBytes, 3, jsonBytes.Length - 3);
        mServerHashCollection = JsonUtility.FromJson<ABHashCollection>(jsonStr);
    }

    private void GenClientJsonCollection() {
        mClientHashCollection = ABUtil.ReadABCompareFile();
        
    }


    /// <summary>
    /// 将服务器的Json更新到客户端
    /// </summary>
    /// <param name="text"></param>
    private void UpdateClientJsonFile( string text) {
        try {
            string filePath = Path.Combine(Application.dataPath, ABGlobal.abCompareFile);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.Write(text);
            streamWriter.Close();
            fs.Close();
        }
        catch (System.Exception e) {
            throw new System.Exception(e.Message);
        }
    }
}
