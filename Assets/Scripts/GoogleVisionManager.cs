using System.Collections;
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
    [SerializeField]
    private GameObject scanOutput;
    [SerializeField]
    private int waitTime = 5;
    [SerializeField]
    private GoogleCloudVision cloudVision;

    private ObjectHelper instance;
    private bool vuforiaDetected = false;

    public void StartScanning()
    {
        modelTarget.SetActive(true);
        vuforiaCam.DontSendDataToCloud();
        cloudVision.StopCoroutine();
        StartCoroutine(Wait());
    }

    public void ShowObject(MultiAnnotationsResponseData obj)
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }

        for (int i = 0; i < obj.Responses.Length; i++)
        {
            instance = Instantiate(objectHelperPrefab, parent);

            if (obj.Responses[i].LocalizedObjectAnnotations != null)
                instance.ShowAnnotations(obj.Responses[i].LocalizedObjectAnnotations, "");
            else
                instance.ShowAnnotations(null, "Not Recogonised");
        }
    }

    private void ScanMLModel()
    {
        modelTarget.SetActive(false);
        vuforiaCam.SendDataToCloud();
        cloudVision.StartCloudVision();
    }

    public void GokuDetected()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = Instantiate(objectHelperPrefab, parent);
        instance.ShowAnnotations(null, "Goku Detected");
        vuforiaDetected = true;
    }

    public void GokuLost()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (vuforiaDetected == false)
            ScanMLModel();
    }
}
