using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<TableGroup> tableGroups = new List<TableGroup>();
    public List<TableSession> Tables = new List<TableSession>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Her TableGroup için bir TableSession oluþtur
        foreach (var group in tableGroups)
        {
            Tables.Add(new TableSession(group));
        }
    }

    public TableSession GetAvailableTable(TableMode mode)
    {
        foreach (var table in Tables)
        {
            if (table.TableGroup.mode == mode && table.IsAvailable())
                return table;
        }
        return null;
    }

    public bool AreAllTablesFull()
    {
        foreach (var table in Tables)
        {
            if (!table.IsFull())
                return false;
        }
        return true;
    }

    public int CountTablesByMode(TableMode mode)
    {
        int count = 0;
        foreach (var table in Tables)
        {
            if (table.TableGroup.mode == mode)
                count++;
        }
        return count;
    }
}
