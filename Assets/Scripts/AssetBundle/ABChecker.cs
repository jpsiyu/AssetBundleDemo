using UnityEngine;
using System.Collections;
using System.IO;

public class ABChecker : MonoBehaviour {

    public IEnumerator StartCheck()
    {
        string url = Global.abURL + Global.abCompareFileName;
        using (WWW www = new WWW(url)) {
            yield return www;
            if (www.error != null)
                throw new System.Exception(string.Format("WWW downad error:{0}, url:{1}", www.error, url));

            ReadFile(www.text);
        }
    }

    private void ReadFile( string text) {
        try {
            string filePath = Path.Combine(Application.dataPath, Global.abCompareFile);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.Write(text);
            streamWriter.Close();
            fs.Close();
            Debug.Log("Read Success");
        }
        catch (System.Exception e) {
            throw new System.Exception(e.Message);
        }
    }
}
