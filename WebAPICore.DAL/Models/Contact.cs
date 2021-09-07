using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPICore.DAL.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool Marked { get; set; }
    }
}
