using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using MySqlConnector;
using UnityEngine;

public class SqlCoordsConverter : MonoBehaviour
{
    private const string ConnectionString = "Server=localhost;Database=escooterdata;Uid=root;Pwd=;";

    [SerializeField] private AbstractMap map;

    void Start()
    {
        StartCoroutine(UpdateDatabase());
    }

    IEnumerator UpdateDatabase()
    {
        yield return new WaitForSeconds(2f);
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT EntryID, X, Y FROM vehicles WHERE xPos IS NULL OR yPos IS NULL", connection);
    
        var updates = new List<(string entryId, double x, double y)>();
    
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var entryId = reader["EntryID"].ToString();
                var x = Convert.ToDouble(reader["X"]);
                var y = Convert.ToDouble(reader["Y"]);

                var result = GetxAndYPos(y,x );
                updates.Add((entryId, result.x, result.y));
            }
        }

        foreach (var update in updates)
        {
            UpdateEntry(connection, update.entryId, update.x, update.y);
            yield return null;

        }

        Debug.Log("Finished");
        connection.Close();
    }


    private void UpdateEntry(MySqlConnection connection, string entryId, double xPos, double yPos)
    {
        using var command =
            new MySqlCommand("UPDATE vehicles SET xPos = @xPos, yPos = @yPos WHERE EntryID = @entryId",
                connection);
        command.Parameters.AddWithValue("@xPos", xPos);
        command.Parameters.AddWithValue("@yPos", yPos);
        command.Parameters.AddWithValue("@entryId", entryId);

        try
        {
            command.ExecuteNonQuery();
            Debug.Log($"Entry {entryId} updated");

        }
        catch (Exception ex)
        {
            Debug.LogError($"Update error: {ex.Message}");
        }
    }

    private Vector2 GetxAndYPos(double x, double y)
    {
        Vector2d loc = new Vector2d(x, y);
        
        Vector3 pos = map.GeoToWorldPosition(loc);
        
        return new Vector2(pos.x, pos.z);
    }
}