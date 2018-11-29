using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.IO;
using SQLite;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
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

            noteListView.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e)
            {
                //Android.Support.V7.App.AlertDialog.Builder builder;
                //builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                //builder.SetTitle("Confirm delete");
                //builder.SetMessage("Are you sure you want to delete this note?");
                //builder.SetCancelable(false);
                //builder.SetPositiveButton("OK", delegate
                //{
                //    databaseService.DeleteNote(notes.ToList()[e.Position].Id);
                //    notes = databaseService.GetAllNotes();
                //    noteListView.Adapter = new CustomAdapter(this, notes.ToList());
                //});
                //builder.SetNegativeButton("Cancel", delegate { });
                //Dialog dialog = builder.Create();
                //dialog.Show();


                var dlgAlert = (new Android.App.AlertDialog.Builder(this)).Create();
                dlgAlert.SetMessage("Choose action");
                dlgAlert.SetTitle("Action");
                dlgAlert.SetButton("Edit", delegate
                {
                    //databaseService.DeleteNote(notes.ToList()[e.Position].Id);
                    //notes = databaseService.GetAllNotes();
                    //noteListView.Adapter = new CustomAdapter(this, notes.ToList());
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