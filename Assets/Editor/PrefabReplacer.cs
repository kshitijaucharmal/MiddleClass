using UnityEditor;
using UnityEngine;

public class PrefabReplacer : EditorWindow
{
    private GameObject selectedGameObject;
    private GameObject replacementPrefab;

    [MenuItem("Tools/GameObject Replacer")]
    public static void ShowWindow()
    {
        GetWindow<PrefabReplacer>("GameObject Replacer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Replace GameObject with Prefab", EditorStyles.boldLabel);

        selectedGameObject = (GameObject)EditorGUILayout.ObjectField("GameObject to Replace", selectedGameObject, typeof(GameObject), true);
        replacementPrefab = (GameObject)EditorGUILayout.ObjectField("Replacement Prefab", replacementPrefab, typeof(GameObject), false);

        if (GUILayout.Button("Replace"))
        {
            if (selectedGameObject == null || replacementPrefab == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign both the GameObject and the prefab before proceeding.", "OK");
            }
            else
            {
                ReplaceGameObject();
            }
        }
    }

    private void ReplaceGameObject()
    {
        if (selectedGameObject == null || replacementPrefab == null)
        {
            Debug.LogWarning("GameObject to replace or replacement prefab is not assigned.");
            return;
        }

        // Store the transform properties of the original GameObject
        Transform originalTransform = selectedGameObject.transform;
        Vector3 originalPosition = originalTransform.position;
        Quaternion originalRotation = originalTransform.rotation;
        Vector3 originalScale = originalTransform.localScale;

        // Instantiate the new prefab in the scene
        GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(replacementPrefab, originalTransform.parent);

        // Apply the original transform properties to the new object
        newObject.transform.SetPositionAndRotation(originalPosition, originalRotation);
        newObject.transform.localScale = originalScale;

        // Record the undo operation
        Undo.RegisterCompleteObjectUndo(selectedGameObject, "Replace GameObject");
        DestroyImmediate(selectedGameObject);
        Undo.RegisterCreatedObjectUndo(newObject, "Replace GameObject");

        Debug.Log("GameObject replacement completed!");
    }
}
