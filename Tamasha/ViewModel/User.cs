using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamasha.ViewModel
{
    internal class User
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RegistryDate { get; set; }

        public override string ToString()
        {
            return "Email: "+Email + "\tEmail: " + Username + "\tPassword: " + Password + "\tRegistryDate: " + RegistryDate;
        }
    }
}
