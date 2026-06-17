using System.Reflection;

namespace LE_06_02.Model;

public class Note
{
    internal string Title{get;set;}
    internal string NoteText{get;set;}

    public Note(string title, string noteText)
    {
        Title = title;
        NoteText = noteText;
    }
    
    public string DisplayProperty => Title;
}