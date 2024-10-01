using TMPro;
using UnityEngine;

public class NotificationHelper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI message;

    public void SetupNotification(string Message)
    {
        message.text = Message;
    }
}
