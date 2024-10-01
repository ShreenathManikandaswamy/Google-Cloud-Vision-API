using UnityEngine;
using ZXing;
using Vuforia;

public class VuforiaCamAccess : MonoBehaviour
{
    private PixelFormat mPixelFormat = PixelFormat.RGB888;
    private bool mFormatRegistered = false;
    private Texture2D texture;
    private IBarcodeReader barcodeReader = new BarcodeReader();
    private bool isScanningBarcore = false;

    public GoogleVisionManager manager;
    public GoogleCloudVision cloudVision;
    private bool isSendDataToCloud = false;

    public void ScanBarCode()
    {
        isScanningBarcore = true;
        Notification.Instance.ShowNotification("Scan QR Code on Table", 5);
    }

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
            if(isScanningBarcore)
            {
                if (texture.width > 0)
                {
                    Result result = barcodeReader.Decode(texture.GetPixels32(), texture.width, texture.height);

                    if (result != null)
                    {
                        isScanningBarcore = false;
                        manager.SetCurrentTable(result.Text);
                        Notification.Instance.CloseNotification();
                    }
                }
            }
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