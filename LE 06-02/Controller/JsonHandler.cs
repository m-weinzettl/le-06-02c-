using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using LE_06_02.Model;

namespace LE_06_02.Controller;

public class JsonHandler
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NoteLib.json");

    public static ObservableCollection<Note> LoadNotes()
    {
        if (!File.Exists(_filePath))
        {
            return new ObservableCollection<Note>();
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            var list = JsonSerializer.Deserialize<ObservableCollection<Note>>(json);
            return list ?? new ObservableCollection<Note>();
        }
        catch
        {
            return new ObservableCollection<Note>();
        }
    }

    public static void SaveNotes(ObservableCollection<Note> notes)
    {
        try
        {
            string json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Fehler beim Speichern: {ex.Message}");
        }
    }
}