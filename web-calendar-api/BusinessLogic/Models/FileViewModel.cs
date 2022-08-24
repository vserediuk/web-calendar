using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime DateUploaded { get; set; }
        public int Size { get; set; }
    }
}
