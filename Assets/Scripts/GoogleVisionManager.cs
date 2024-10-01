using System.Collections.Generic;
using UnityEngine;

public class GoogleVisionManager : MonoBehaviour
{
    [SerializeField]
    private ObjectHelper objectHelperPrefab;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private List<TableSorter> tables;

    private ObjectHelper instance;
    private TableSorter currentTable;

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

    public void SetCurrentTable(string tableId)
    {
        foreach(TableSorter table in tables)
        {
            if (table.tableName == tableId)
                currentTable = table;
        }

        Debug.Log(currentTable.tableName);
        Notification.Instance.ShowNotification("Current Table = " + currentTable.tableName, 5);
    }
}
