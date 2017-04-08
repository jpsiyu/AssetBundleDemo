using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

[Serializable]
public struct ABHashInfo
{
    public string ab;
    public string hash;
}

[Serializable]
public class ABHashCollection
{
    public List<ABHashInfo> abHashList = new List<ABHashInfo>();

}

public enum ABBuildPlatform { 
    Win64,
    Android,
    IOS,
}
