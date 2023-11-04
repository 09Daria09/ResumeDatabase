using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeDatabase.Model
{
    public class Resume
    {
        public string FullName { get; set; }
        public int? Age { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<string> Skills { get; set; }
    }
}
