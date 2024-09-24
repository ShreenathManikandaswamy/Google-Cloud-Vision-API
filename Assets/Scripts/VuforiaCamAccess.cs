using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class VuforiaCamAccess : MonoBehaviour
{
    private PixelFormat mPixelFormat = PixelFormat.RGB888;
    private bool mFormatRegistered = false;
    private Texture2D texture;

    public GoogleCloudVision cloudVision;
    private bool isSendDataToCloud = false;

    void Start()
    {
        texture = new Texture2D(256, 256, TextureFormat.RGB24, false);

        VuforiaApplication.Instance.OnVuforiaStarted += RegisterFormat;
        VuforiaBehaviour.Instance.World.OnStateUpdated += OnVuforiaUpdated;
    }

    void OnVuforiaUpdated()
    {
        if (mFormatRegistered)
        {
            Vuforia.Image image = VuforiaBehaviour.Instance.CameraDevice.GetCameraImage(mPixelFormat);
            image.CopyBufferToTexture(texture);
            texture.Apply();
            if(isSendDataToCloud)
                cloudVision.SendVuforiaCameraData(texture);
        }
    }

    public void DontSendDataToCloud()
    {
        isSendDataToCloud = false;
    }

    public void SendDataToCloud()
    {
        isSendDataToCloud = true;
    }

    void RegisterFormat()
    {

        bool success = VuforiaBehaviour.Instance.CameraDevice.SetFrameFormat(mPixelFormat, true);
        if (success)

        {
            mFormatRegistered = true;

        }
        else
        {
            mFormatRegistered = false;
        }
    }
}