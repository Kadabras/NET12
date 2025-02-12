﻿using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace WebMaze.Models.GenerationDocument
{
    public class PDFGenerationTaskInfoViewModel
    {        
        public int Id { get; set; }

        public int Progress { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        public bool ReadyToDownload { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public string ReadyPDF { get; set; }

    }
}
