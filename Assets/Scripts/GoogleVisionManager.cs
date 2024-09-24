using TMPro;
using UnityEngine;

public class GoogleVisionManager : MonoBehaviour
{
    [SerializeField]
    private ObjectHelper objectHelperPrefab;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private VuforiaCamAccess vuforiaCam;
    [SerializeField]
    private GameObject modelTarget;

    private ObjectHelper instance;

    public void ShowObject(MultiAnnotationsResponseData obj)
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }

        if (obj.Responses.Length > 0)
        {
            for (int i = 0; i < obj.Responses.Length; i++)
            {
                instance = Instantiate(objectHelperPrefab, parent);
                instance.ShowAnnotations(obj.Responses[i].LocalizedObjectAnnotations, i + 1);
            }
        }
    }

    public void ScanCustomObject()
    {
        vuforiaCam.DontSendDataToCloud();
        modelTarget.SetActive(true);
    }

    public void ScanMLModel()
    {
        modelTarget.SetActive(false);
        vuforiaCam.SendDataToCloud();
    }
}
