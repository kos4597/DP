using System.Collections.Generic;
using UnityEngine;

public abstract class TableDataBase
{
    public string tableKey = string.Empty;
    public virtual void Load()
    {
        Debug.Log($"DataBase Load");
    }
}
