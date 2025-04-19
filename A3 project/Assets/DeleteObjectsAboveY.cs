using UnityEngine;

public class DeleteObjectsAboveY : MonoBehaviour
{
    public float thresholdY = 0.1407275f;

    [ContextMenu("Delete Objects Above Y")]
    void DeleteObjects()
    {
        // 使用新方法（不排序）
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        int deletedCount = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.transform.position.y > thresholdY)
            {
                DestroyImmediate(obj);
                deletedCount++;
            }
        }

        Debug.Log($"Deleted {deletedCount} objects above Y={thresholdY}");
    }
}