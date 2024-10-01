using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField]
    private NotificationHelper notificationPrefab;
    [SerializeField]
    private Transform notificationParent;

    public static Notification Instance;
    private NotificationHelper notificationInstance;
    private Coroutine currentCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowNotification(string message, float time)
    {
        CloseNotification();

        notificationInstance = Instantiate(notificationPrefab, notificationParent);
        notificationInstance.SetupNotification(message);
        currentCoroutine = StartCoroutine(CloseNotification(time));
    }

    private IEnumerator CloseNotification(float time)
    {
        yield return new WaitForSeconds(time);
        CloseNotification();
    }

    public void CloseNotification()
    {
        if (notificationInstance != null)
        {
            Destroy(notificationInstance.gameObject);
            if(currentCoroutine != null)
                StopCoroutine(currentCoroutine);
        }
    }
}
