using TMPro;
using UnityEngine;

public class GoogleVisionManager : MonoBehaviour
{
    [SerializeField]
    private ObjectHelper objectHelperPrefab;
    [SerializeField]
    private Transform parent;

    public void ShowObject(MultiAnnotationsResponseData obj)
    {
        for(int i = 0; i<obj.Responses.Length; i++)
        {
            ObjectHelper instance = Instantiate(objectHelperPrefab, parent);
            instance.ShowAnnotations(obj.Responses[i].LocalizedObjectAnnotations, i + 1);
        }
    }
}
