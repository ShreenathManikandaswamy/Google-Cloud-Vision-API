using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Table", menuName = "Custom/Table")]
public class TableSorter : ScriptableObject
{
    public string tableName;
    public List<string> objects;
}
