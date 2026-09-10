using System.IO;
using UnityEditor;
using UnityEngine;

public static class MissingScriptsRemover
{
    [MenuItem("Tools/Remove Missing Scripts From Folder")]
    private static void RemoveFromFolder()
    {
        string folderPath = GetSelectedFolderPath();

        if (string.IsNullOrEmpty(folderPath))
        {
            Debug.LogWarning("Please select a folder in the Project window.");
            return;
        }

        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });

        int prefabCount = 0;
        int removedCount = 0;

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = PrefabUtility.LoadPrefabContents(path);

            try
            {
                foreach (Transform transform in prefab.GetComponentsInChildren<Transform>(true))
                {
                    removedCount += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(
                        transform.gameObject);
                }

                PrefabUtility.SaveAsPrefabAsset(prefab, path);
                prefabCount++;
            }
            finally
            {
                PrefabUtility.UnloadPrefabContents(prefab);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log(
            $"Processed {prefabCount} prefabs. " +
            $"Removed {removedCount} missing scripts.");
    }

    private static string GetSelectedFolderPath()
    {
        Object selected = Selection.activeObject;
        if (selected == null)
            return null;

        string path = AssetDatabase.GetAssetPath(selected);
        if (AssetDatabase.IsValidFolder(path))
            return path;

        if (File.Exists(path))
            return Path.GetDirectoryName(path)?.Replace("\\", "/");

        return null;
    }
}
