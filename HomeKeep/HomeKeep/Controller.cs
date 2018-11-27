using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HomeKeep
{
    static public class Controller
    {


        static private string RunSqlQuery(string query)
        {
            String connectionString = @"Data Source = DESKTOP-M37KRRP\SQL2017;Initial Catalog=Notes;User ID=sa;Password=11223344";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            
            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            string result = "";
            int count = dataReader.FieldCount;
            while (dataReader.Read())
            {
                for (int i = 0; i < count; i++)
                {
                    result += dataReader.GetValue(i).ToString()+ "<#>";
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
            String query = "SELECT * FROM Note";

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
            string query = "INSERT INTO Note VALUES ('" + note.Name + "', '" + note.Text + "', " + note.Tag.ToString() + " )";
            RunSqlQuery(query);
        }

        static public void DeleteNote(int id)
        {
            string query = "DELETE FROM Note Where id = " + id.ToString();
            RunSqlQuery(query);
        }

        static public void DeleteNote(Note note)
        {
            string query = "DELETE FROM Note Where id = " + note.Id;
            RunSqlQuery(query);
        }
        
        /// <summary>
        /// Метод, запрашивающий у БД все заметки
        /// </summary>
        /// <returns>Массив тегов типа Tag[]</returns>
        static public Tag[] GetAllTags()
        {
            String result = "";
            String query = "SELECT * FROM Tag";

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
            string query = "SELECT name FROM Tag WHERE id=" + id.ToString();

            result = RunSqlQuery(query);

            result = result.Trim(new char[] { '<', '>', '@', '#' });

                return result;
        }
        
        public static Note [] SearchByTag (int tagId)
        {
            String result = "";
            String query = "SELECT * FROM Note WHERE Note.tag=" + tagId.ToString();

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
    }
}


