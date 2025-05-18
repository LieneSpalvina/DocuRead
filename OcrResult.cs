using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentReaderApp
{
    public class OcrResult
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string TextBlock { get; set; }
        public double Confidence { get; set; }
        public int PageNumber { get; set; }
    }

}
