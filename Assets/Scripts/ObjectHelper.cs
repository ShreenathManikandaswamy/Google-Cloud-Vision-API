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

    public void ShowAnnotations(LocalizedObjectAnnotation[] obj, string titleValue)
    {
        if (!string.IsNullOrEmpty(titleValue))
            title.text = "Object " + titleValue;
        else
            Destroy(title.gameObject);

        if (obj != null)
        {
            foreach (LocalizedObjectAnnotation res in obj)
            {
                TextMeshProUGUI instance = Instantiate(textPrefab, parent);
                instance.text = res.Name + " -- " + (res.Score * 100).ToString("F2") + " %";
            }
        }
    }
}
