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


        static private void RunSqlQuery(string query)
        {
            String connectionString = @"Data Source = DESKTOP-M37KRRP\SQL2017;Initial Catalog=Notes;User ID=sa;Password=11223344";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            
            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            connection.Close();


        }
        
        /// <summary>
        /// Метод, запрашивающий у БД все заметки
        /// </summary>
        /// <returns>Массив заметок типа Note[]</returns>
        static public Note[] GetAllNotes()
        {
            String connectionString = @"Data Source = DESKTOP-M37KRRP\SQL2017;Initial Catalog=Notes;User ID=sa;Password=11223344";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            String result = "";
            String query = "SELECT * FROM Note";

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                result += dataReader.GetValue(0) + "<#>" + dataReader.GetValue(1) + "<#>" + dataReader.GetValue(2) + "<#>" + dataReader.GetValue(3) + "<@>";
            }

            string[] rawNotes = result.Split(new string[] { "<@>" }, StringSplitOptions.RemoveEmptyEntries);

            Note[] notesFromBD = new Note[rawNotes.Length];
            for (int i = 0; i < rawNotes.Length
                ; i++)
            {
                notesFromBD[i] = new Note(rawNotes[i]);

            }


            connection.Close();
             
            return notesFromBD;

        }

        /// <summary>
        /// Занесение в БД заметки
        /// </summary>
        /// <param name="note">Экземпляр класса Note</param>
        static public void CreateNote(Note note)
        {
            String connectionString = @"Data Source = DESKTOP-M37KRRP\SQL2017;Initial Catalog=Notes;User ID=sa;Password=11223344";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            string query = "INSERT INTO Note VALUES ('" + note.Name + "', '" + note.Text + "', " + note.Tag.ToString() + " )";

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();
  
            connection.Close();
 
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
            String connectionString = @"Data Source = DESKTOP-M37KRRP\SQL2017;Initial Catalog=Notes;User ID=sa;Password=11223344";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            
            SqlCommand command;
            SqlDataReader dataReader;
            String result = "";
            String query = "SELECT * FROM Tag";

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                result += dataReader.GetValue(0) + "<#>" + dataReader.GetValue(1) + "<@>";
            }

            string[] rawTags = result.Split(new string[] { "<@>" }, StringSplitOptions.RemoveEmptyEntries);

            Tag[] tagsFromBD = new Tag[rawTags.Length];
            for (int i = 0; i < rawTags.Length; i++)
            {
                tagsFromBD[i] = new Tag(rawTags[i]);

            }
            
            connection.Close();
            
            return tagsFromBD;
        }

        /// <summary>
        /// Возвращает тег по его номеру
        /// </summary>
        /// <param name="id">Номер тега</param>
        /// <returns>Строка с именем тега</returns>
        static public string GetTagName(int id)
        {
            string connectionString = @"Data Source = DESKTOP-M37KRRP\SQL2017;Initial Catalog=Notes;User ID=sa;Password=11223344";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            string result = "";
            string query = "SELECT name FROM Tag WHERE id=" + id.ToString();

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                result += dataReader.GetValue(0).ToString();
            }

            return result;
        }
        
    }
}


