using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;

namespace XA_SQLite_Lectures
{

    class SQLiteDB
    {
        //database path
        private readonly string dbPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Mid2Lab5.db3");

        // Constructor
        public SQLiteDB()
        {
            //Creating database, if it doesn't already exist 
            if (!File.Exists(dbPath))
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Lectures>();
                db.CreateTable<Teachers>();
            }
        }

        //  Insert the object to Lectures table
        //  ادخال مستخدم
        public void InsertLecture(Lectures newLecture)
        {
            var db = new SQLiteConnection(dbPath);
            db.Insert(newLecture);
        }

        public void InsertTeacher(Teachers newTeacher)
        {
            var db = new SQLiteConnection(dbPath);
            db.Insert(newTeacher);
        }

        //================================================
        // Object ارجاع بيانات سجل محاضرة واحد على شكل   
        public Lectures GetLecture(string code)
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Lectures>().Where(i => i.CourseCode == code).FirstOrDefault();
        }

        public Teachers GetTeacher(string name)
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Teachers>().Where(i => i.Name == name).FirstOrDefault();
        }

        public Teachers GetTeacher(int id)
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Teachers>().Where(i => i.Id == id).FirstOrDefault();
        }
        //================================================
        // List of Lectures ارجاع بيانات مقرر جميع سجلات الشعب للمقرر الواحد على شكل   
        public List<Lectures> GetAllCodes(string courseName)
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Lectures>().Where(i => i.CourseName == courseName).ToList();
        }

        //================================================
        // List of Lectures ارجاع بيانات جميع المقرارات مع جميع سجلات الشعب على شكل   

        public List<Lectures> GetAllCourses()
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Lectures>().ToList();
        }
        //=================================
        // تحديث مستخدم
        public void UpdateLecture(Lectures lecture)
        {
            var db = new SQLiteConnection(dbPath);
            db.Update(lecture);
        }

        //=================================
        // تحديث مستخدم
        public void DeleteLecture(Lectures lecture)
        {
            var db = new SQLiteConnection(dbPath);
            db.Delete(lecture);
        }


        // Lectures Teble
        [Table("Lectures")]
        public class Lectures
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [MaxLength(8)]
            public string CourseCode { get; set; }
            [MaxLength(8)]
            public string CourseName { get; set; }
            public int TId { get; set; }
        }

        // Teachers Teble
        [Table("Teachers")]
        public class Teachers
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [MaxLength(8)]
            public string Name { get; set; }           
        }
    }
}