using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Common;

namespace HomeKeep
{
    //TODO Переделать все запросы
    static public class Controller
    {

        static private string RunSqlQuery(string query)
        {
            String connectionString = @"Data Source=" + AppDomain.CurrentDomain.BaseDirectory.ToString() + "Note.db";
            SQLiteConnection connection = new SQLiteConnection(connectionString);

            connection.Open();

            SQLiteCommand command;
            SQLiteDataReader dataReader;

            command = new SQLiteCommand(query, connection);
            dataReader = command.ExecuteReader();

            string result = "";
            int count = dataReader.FieldCount;
            foreach (DbDataRecord record in dataReader)
            {
                for (int i = 0; i < count; i++)
                {
                    result += record[i].ToString() + "<#>";
                }
                result += "<@>";
            }

            dataReader.Close();
            connection.Close();

            return result;

        }



        /// <summary>
        /// Метод, запрашивающий у БД все заметки
        /// </summary>
        /// <returns>Массив заметок типа Note[]</returns>
        static public Note[] GetAllNotes()
        {
            
            String result = "";
            String query = "SELECT * FROM Note ORDER BY id DESC";

            result = RunSqlQuery(query);

            string[] rawNotes = result.Split(new string[] { "<@>" }, StringSplitOptions.RemoveEmptyEntries);

            Note[] notesFromBD = new Note[rawNotes.Length];
            for (int i = 0; i < rawNotes.Length
                ; i++)
            {
                notesFromBD[i] = new Note(rawNotes[i]);

            }
             
            return notesFromBD;

        }

        /// <summary>
        /// Занесение в БД заметки
        /// </summary>
        /// <param name="note">Экземпляр класса Note</param>
        static public void CreateNote(Note note)
        {
            string query = "INSERT INTO Note (\"name\", \"text\", \"tag\") VALUES ('" + note.Name + "', '" + note.Text + "', " + note.Tag.ToString() + " );";
            RunSqlQuery(query);
        }

        static public void CreateTag(Tag tag)
        {
            string query = "INSERT INTO Tag (\"name\") VALUES ('" + tag.Name + "' );";
            RunSqlQuery(query);
        }


        static public void DeleteNote(int id)
        {
            string query = "DELETE FROM Note Where id = " + id.ToString() + ";";
            RunSqlQuery(query);
        }

        static public void DeleteTag(int id)
        {
            string query = "PRAGMA foreign_keys = \"1\"; DELETE FROM Tag WHERE id = " + id.ToString() + ";";
            RunSqlQuery(query);
        }

       
        /// <summary>
        /// Метод, запрашивающий у БД все заметки
        /// </summary>
        /// <returns>Массив тегов типа Tag[]</returns>
        static public Tag[] GetAllTags()
        {
            String result = "";
            String query = "SELECT * FROM Tag;";

            result = RunSqlQuery(query);

            string[] rawTags = result.Split(new string[] { "<@>" }, StringSplitOptions.RemoveEmptyEntries);
            Tag[] tagsFromBD = new Tag[rawTags.Length];
            for (int i = 0; i < rawTags.Length; i++)
            {
                tagsFromBD[i] = new Tag(rawTags[i]);

            }
            return tagsFromBD;
        }

        
        /// <summary>
        /// Возвращает тег по его номеру
        /// </summary>
        /// <param name="id">Номер тега</param>
        /// <returns>Строка с именем тега</returns>
        static public string GetTagName(int id)
        {
            string result = "";
            string query = "SELECT name FROM Tag WHERE id=" + id.ToString() + ";";

            result = RunSqlQuery(query);

            result = result.Trim(new char[] { '<', '>', '@', '#' });

                return result;
        }
        

        /// <summary>
        /// Поиск заметки по идентификатору тега
        /// </summary>
        /// <param name="tagId">Идентификатор тега</param>
        /// <returns>Найденная заметка</returns>
        public static Note [] SearchByTag (int tagId)
        {
            String result = "";
            String query = "SELECT * FROM Note WHERE tag=" + tagId.ToString() + " ORDER BY id DESC;";

            result = RunSqlQuery(query);

            string[] rawNotes = result.Split(new string[] { "<@>" }, StringSplitOptions.RemoveEmptyEntries);

            Note[] notesFromBD = new Note[rawNotes.Length];
            for (int i = 0; i < rawNotes.Length
                ; i++)
            {
                notesFromBD[i] = new Note(rawNotes[i]);

            }

            return notesFromBD;
        }

        public static void EditNote (Note note)
        {
            string query = "UPDATE Note SET name='" + note.Name + "' , text ='" + note.Text + "', tag='" + note.Tag.ToString() + "' WHERE id = " + note.Id.ToString() + ";";
            RunSqlQuery(query);
        }

        public static void EditTag(Tag tag)
        {
            string query = "UPDATE Tag SET name='" + tag.Name + "' WHERE id = " + tag.Id.ToString() + ";";
            RunSqlQuery(query);
        }

        public static Note GetNote (int id)
        {
            String result = "";
            String query = "SELECT * FROM Note WHERE id=" + id.ToString() + ";";

            result = RunSqlQuery(query);
            
            if(result.Length == 0)
            {
                return new Note();
            }
            else
            {
                result = result.Split(new string[] { "<@>" }, StringSplitOptions.RemoveEmptyEntries)[0];

                Note foundNote = new Note(result);
                return foundNote;

            }
        }


        public static Tag GetTag(int id)
        {
            String result = "";
            String query = "SELECT * FROM Tag WHERE id=" + id.ToString() + ";";

            result = RunSqlQuery(query);

            if(result.Length == 0)
            {
                return new Tag();
            }
            else
            {
                result = result.Split(new string[] { "<@>" }, StringSplitOptions.RemoveEmptyEntries)[0];

                Tag foundTag = new Tag(result);
                return foundTag;
            }
            
        }
    }
}


