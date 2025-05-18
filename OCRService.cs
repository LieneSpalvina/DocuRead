using System;
using System.Collections.Generic;
using System.IO;
using Tesseract;
using DocumentReaderApp.Models;
using System.Text.Json;

namespace DocumentReaderApp.Services
{
    public class OCRService
    {
        // Ideally move to a config file or use dependency injection
        private readonly string _tessDataPath = @"C:\Program Files\Tesseract-OCR\tessdata";

        /// <summary>
        /// Extracts plain text from the image file using Tesseract.
        /// </summary>
        public string ExtractText(string filePath, string language)
        {
            try
            {
                using var engine = new TesseractEngine(_tessDataPath, language, EngineMode.Default);
                using var img = Pix.LoadFromFile(filePath);
                using var page = engine.Process(img);

                return page.GetText();
            }
            catch (Exception ex)
            {
                return $"Error processing file: {ex.Message}";
            }
        }

        /// <summary>
        /// Extracts detailed OCR results as a list of text blocks with confidence values.
        /// </summary>
        public List<OcrResult> ExtractDetailedResults(string filePath, string language, int documentId)
        {
            var results = new List<OcrResult>();

            try
            {
                using var engine = new TesseractEngine(_tessDataPath, language, EngineMode.Default);
                using var img = Pix.LoadFromFile(filePath);
                using var page = engine.Process(img);

                int pageNumber = 1; // Could be improved for multi-page PDFs

                var lines = page.GetText().Split('\n');
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        results.Add(new OcrResult
                        {
                            DocumentId = documentId,
                            TextBlock = line.Trim(),
                            Confidence = page.GetMeanConfidence(),
                            PageNumber = pageNumber
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                results.Add(new OcrResult
                {
                    DocumentId = documentId,
                    TextBlock = $"Error: {ex.Message}",
                    Confidence = 0,
                    PageNumber = 0
                });
            }

            return results;
        }

        /// <summary>
        /// Saves OCR results to a JSON file for later use or debugging.
        /// </summary>
        public void SaveResultsToJson(List<OcrResult> results, int documentId)
        {
            try
            {
                string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OcrData");
                Directory.CreateDirectory(directory);

                string filePath = Path.Combine(directory, $"ocr_{documentId}.json");
                string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                // You may want to log this or inform the user
                Console.WriteLine($"Error saving OCR results: {ex.Message}");
            }
        }
    }
}
