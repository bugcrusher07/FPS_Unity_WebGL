using UnityEngine;

public class TagChildren : MonoBehaviour
{
    public string tagToApply = "Environment";

    [ContextMenu ("Tag All Children")]
    void TagAllChildren()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            child.gameObject.tag = tagToApply;
        }
    }
}
