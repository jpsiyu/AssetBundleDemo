﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class ABWindow : EditorWindow {
    private bool groupEnabled;
    private float mBtnHeight = 30;
    
    //setting version
    private int mVersion = 1;
    private ABBuildPlatform mBuildPlatform = ABBuildPlatform.Win64;

    public ABBuildPlatform BuildPlatform
    {
        get { return mBuildPlatform; }
    }

    [MenuItem("Window/ABWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ABWindow));
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label("AssetBundle Build Settings");

        Line();
        Setting();

        Line();
        ToolSetBtn();

        Line();
        BuildAB();
        EditorGUILayout.EndVertical();
    }

    private void BuildAB() {
        GUILayout.Label("Build Asset Bundle");
        GUILayout.Label("ABPath: " + ABSettings.AssetBundleBuildPath());
        GUILayout.Label("ABVersion: " + mVersion);
        GUILayout.Label("ABPlatform: " + mBuildPlatform);
        SetBtn("Builde", ABToolSet.BuildAllAssetBundles);
    }

    private void Setting() {
        GUILayout.Label("Settings");

        EditorGUILayout.BeginHorizontal();
        int newVersion = EditorGUILayout.IntField("Version:", mVersion, GUILayout.ExpandWidth(false));
        mVersion = newVersion < 100 ? newVersion : mVersion;
        EditorGUILayout.EndHorizontal();

        mBuildPlatform = (ABBuildPlatform)EditorGUILayout.EnumPopup("ABBuildPlatform", mBuildPlatform, GUILayout.ExpandWidth(false));
    }


    private void ToolSetBtn() {
        GUILayout.Label("Toolsets");

        groupEnabled = EditorGUILayout.BeginToggleGroup("Enable ToolSets", groupEnabled);

        if (groupEnabled) {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            SetBtn("GetABNames", ABToolSet.GetNames);
            SetBtn("GenCompareFile", ABToolSet.GenABCompareFile);
            SetBtn("ReadCompareFile", ABToolSet.ReadABCompareFile);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            SetBtn("DeleteCompareFile", ABToolSet.RemoveLocalCompareFile);
            SetBtn("DeleteAssetBundle", ABToolSet.RemoveAssetBundle);
            SetBtn("CleanABCache", ABToolSet.ClearABCache);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndToggleGroup();

    }

    private void SetBtn(string btnName, System.Action callback)
    {
        if (GUILayout.Button(btnName, GUILayout.Width(btnName.Length * 10), GUILayout.Height(mBtnHeight)))
            if(callback != null) callback();
    }

    private void Line() {
        GUILayout.Space(20f);
        GUILayout.Label("^.^----------------------------------------------------------------------------------------------------------------");
    }
}
