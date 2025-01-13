using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager Instance { get; private set; }

    public List<TableDataBase> tableList = new List<TableDataBase>();
    public TestTable TestTable = new TestTable();


    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        MakeDummyData();
    }

    private void MakeDummyData()
    {
        for(int i = 0; i < 10; i++)
        {
            TestTable temp = new TestTable();
            temp.tableKey = $"TestTable_{i+1}";
            tableList.Add(temp);
        }
    }
}
