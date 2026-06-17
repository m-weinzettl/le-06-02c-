using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LE_06_02.Controller;
using LE_06_02.Model;
using System.Linq;

namespace LE_06_01;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ObservableCollection<Note> _notes;
    public MainWindow()
    {
        InitializeComponent();
        _notes = JsonHandler.LoadNotes();
        ListNotes.ItemsSource = _notes;
    }

    private void BtnCreateNote_Click(object sender, RoutedEventArgs e)
    {
        string title = TxtNoteTitle.Text.Trim();
        string text = TxtNote.Text.Trim();

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(text))
        {
            TxtBlockOutput.Text = "Bitte geben Sie sowohl einen Titel als auch einen Notiztext ein.";
            return;
        }
        if (_notes.Any(n => n.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
        {
            TxtBlockOutput.Text = "Eine Notiz mit diesem Titel existiert bereits.";
            return;
        }
        Note newNote = new Note(title, text);
        _notes.Add(newNote);
        JsonHandler.SaveNotes(_notes);
        
        TxtNoteTitle.Clear();
        TxtNote.Clear();
        TxtBlockOutput.Text = "Notiz erfolgreich hinzugefügt.";
    }

    private void BtnDeleteNote_Click(object sender, RoutedEventArgs e)
    {
        Note selectedNote = ListNotes.SelectedItem as Note;

        if (selectedNote != null)
        {
            _notes.Remove(selectedNote);
            JsonHandler.SaveNotes(_notes);
            TxtBlockOutput.Text = "Notiz erfolgreich gelöscht.";
        }
        else
        {
            TxtBlockOutput.Text = "Bitte wählen Sie zuerst eine Notiz aus der Liste aus.";
            return;
        }
    }
}
