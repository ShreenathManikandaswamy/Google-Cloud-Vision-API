using UnityEngine;

public class JsonParseTester : MonoBehaviour
{
    [SerializeField]
    private string jsonString;
    [SerializeField]
    private FeatureType featureType = FeatureType.TEXT_DETECTION;
    [SerializeField]
    private LineRenderer boundsPref;

    public enum FeatureType
    {
        TYPE_UNSPECIFIED,
        FACE_DETECTION,
        LANDMARK_DETECTION,
        LOGO_DETECTION,
        LABEL_DETECTION,
        TEXT_DETECTION,
        SAFE_SEARCH_DETECTION,
        IMAGE_PROPERTIES,
        OBJECT_LOCALIZATION
    }

    // Start is called before the first frame update
    void Start()
    {
        ProcessResponse();
    }

    void ProcessResponse()
    {
        switch (featureType)
        {
            case FeatureType.LOGO_DETECTION:
                LogoAnnotationResponse annotations = LogoAnnotationResponse.FromJson(jsonString);
                ProcessLogo(annotations);
                break;
        }
    }

    void ProcessLogo(LogoAnnotationResponse response)
    {
        for (int i = 0; i < response.Responses[0].LogoAnnotations.Length; i++)
        {
            LineRenderer boundsInstance = Instantiate(boundsPref);
            for (int j = 0; j < response.Responses[0].LogoAnnotations[i].BoundingPoly.Vertices.Length; j++)
            {
                Vertex[] bounds = response.Responses[0].LogoAnnotations[i].BoundingPoly.Vertices;
                Debug.Log(i + " : X = " + bounds[j].X + " ------ Y = " + bounds[j].Y);
                boundsInstance.SetPosition(i, new Vector3(bounds[j].X, bounds[j].Y, 0)); 
            }
        }
    }
}
