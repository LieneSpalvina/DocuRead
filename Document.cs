namespace DocumentReaderApp.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ExtractedText { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public DateTime ExtractionDate { get; set; }
        public List<OcrResult> OcrResults { get; set; } = new();

    }
}
