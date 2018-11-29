using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.IO;
using SQLite;
using Android.Views;
using Newtonsoft.Json;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public partial class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText addNoteEditText = FindViewById<EditText>(Resource.Id.editText2);
            Button addNoteButton = FindViewById<Button>(Resource.Id.button1);
            ListView noteListView = FindViewById<ListView>(Resource.Id.listView1);

            DatabaseService databaseService = new DatabaseService();
            databaseService.CreateDatabaseWithTable();
            TableQuery<Note> notes = databaseService.GetAllNotes();

            noteListView.Adapter = new CustomAdapter(this, notes.ToList());

            addNoteButton.Click += delegate
            {
                EditText noteTitle = FindViewById<EditText>(Resource.Id.editText1);
                string content = addNoteEditText.Text;
                databaseService.AddNote(noteTitle.Text, content);

                addNoteEditText.Text = noteTitle.Text = "";

                notes = databaseService.GetAllNotes();
                noteListView.Adapter = new CustomAdapter(this, notes.ToList());
            };

            noteListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                Android.App.AlertDialog dlgAlert = (new Android.App.AlertDialog.Builder(this)).Create();
                dlgAlert.SetMessage("Choose action");
                dlgAlert.SetTitle("Action");

                dlgAlert.SetButton("Edit", delegate
                {
                    LayoutInflater layoutInflater = LayoutInflater.From(this);
                    View view = layoutInflater.Inflate(Resource.Layout.UserInputDialog, null);
                    Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                    alertbuilder.SetView(view);
                    Note note = notes.ToList()[e.Position];
                    EditText newTitleEditText = view.FindViewById<EditText>(Resource.Id.title);
                    EditText newContentEditText = view.FindViewById<EditText>(Resource.Id.content);
                    newTitleEditText.Text = note.Title;
                    newContentEditText.Text = note.Content;
                    alertbuilder.SetCancelable(false)
                    .SetPositiveButton("Submit", delegate
                    {
                        databaseService.EditNote(note.Id, note.Date, newTitleEditText.Text, newContentEditText.Text);
                        notes = databaseService.GetAllNotes();
                        noteListView.Adapter = new CustomAdapter(this, notes.ToList());
                    })
                    .SetNegativeButton("Cancel", delegate { });
                    Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
                    dialog.Show();
                });

                dlgAlert.SetButton2("Delete", delegate
                {
                    databaseService.DeleteNote(notes.ToList()[e.Position].Id);
                    notes = databaseService.GetAllNotes();
                    noteListView.Adapter = new CustomAdapter(this, notes.ToList());
                });

                dlgAlert.SetButton3("Cancel", delegate { });
                dlgAlert.Show();
            };
        }
    }
}