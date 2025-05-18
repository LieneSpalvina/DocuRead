using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DocumentReaderApp.Models;

namespace DocumentReaderApp.Services
{
    public class DocumentService
    {
        private readonly string _connectionString = "Data Source=document_reader.db;Version=3;";

        public void AddDocument(Document document)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = "INSERT INTO Documents (FileName, FilePath, ExtractedText, Language, ExtractionDate) " +
                           "VALUES (@FileName, @FilePath, @ExtractedText, @Language, @ExtractionDate)";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@FileName", document.FileName);
            command.Parameters.AddWithValue("@FilePath", document.FilePath);
            command.Parameters.AddWithValue("@ExtractedText", document.ExtractedText);
            command.Parameters.AddWithValue("@Language", document.Language);
            command.Parameters.AddWithValue("@ExtractionDate", document.ExtractionDate);

            command.ExecuteNonQuery();
        }

        public List<Document> GetAllDocuments()
        {
            var documents = new List<Document>();

            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Documents";

            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var document = new Document
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    FileName = reader.GetString(reader.GetOrdinal("FileName")),
                    FilePath = reader.GetString(reader.GetOrdinal("FilePath")),
                    ExtractedText = reader.GetString(reader.GetOrdinal("ExtractedText")),
                    Language = reader.GetString(reader.GetOrdinal("Language")),
                    ExtractionDate = reader.GetDateTime(reader.GetOrdinal("ExtractionDate"))
                };
                documents.Add(document);
            }

            return documents;
        }

        public void DeleteDocument(int documentId)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM Documents WHERE Id = @Id";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Id", documentId);
            command.ExecuteNonQuery();
        }



    }
}
