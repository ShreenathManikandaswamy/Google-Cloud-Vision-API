using TMPro;
using UnityEngine;

public class ObjectHelper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI textPrefab;
    [SerializeField]
    private Transform parent;

    public void ShowAnnotations(LocalizedObjectAnnotation[] obj, int titleValue)
    {
        title.text = "Object " + titleValue;
        foreach(LocalizedObjectAnnotation res in obj)
        {
            TextMeshProUGUI instance = Instantiate(textPrefab, parent);
            instance.text = res.Name + " -- " + (res.Score * 100).ToString("F2") + " %";
        }
    }
}
