using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace App1
{
    public class DatabaseService
    {
        SQLiteConnection db;

        public void CreateDatabaseWithTable()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myNotes.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Note>();
        }

        public void AddNote(string title, string content)
        {
            Note newNote = new Note();
            newNote.Title = title;
            newNote.Date = DateTime.Now.ToUniversalTime();
            newNote.Content = content;
            db.Insert(newNote);
        }

        public void DeleteNote(int id)
        {
            Note noteToDelete = new Note();
            noteToDelete.Id = id;
            db.Delete(noteToDelete);
        }

        public TableQuery<Note> GetAllNotes()
        {
            return db.Table<Note>();
        }
    }
}