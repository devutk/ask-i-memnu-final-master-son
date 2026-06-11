using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildPlayerScript
{
    public static void BuildWindowsBatch()
    {
        string projectPath = Directory.GetParent(Application.dataPath).FullName;
        string buildDir = Path.Combine(projectPath, "Builds", "Windows");
        string buildPath = Path.Combine(buildDir, "AskIMemnu.exe");

        Directory.CreateDirectory(buildDir);

        string[] scenes = GetEnabledScenes();
        BuildReport report = BuildPipeline.BuildPlayer(
            scenes,
            buildPath,
            BuildTarget.StandaloneWindows64,
            BuildOptions.None
        );

        if (report.summary.result != BuildResult.Succeeded)
        {
            Debug.LogError("Build failed: " + report.summary.result);
            EditorApplication.Exit(1);
        }

        Debug.Log("Build succeeded: " + buildPath);
        EditorApplication.Exit(0);
    }

    static string[] GetEnabledScenes()
    {
        EditorBuildSettingsScene[] settingsScenes = EditorBuildSettings.scenes;
        int count = 0;
        for (int i = 0; i < settingsScenes.Length; i++)
        {
            if (settingsScenes[i].enabled)
                count++;
        }

        string[] scenes = new string[count];
        int index = 0;
        for (int i = 0; i < settingsScenes.Length; i++)
        {
            if (!settingsScenes[i].enabled)
                continue;

            scenes[index++] = settingsScenes[i].path;
        }

        return scenes;
    }
}
