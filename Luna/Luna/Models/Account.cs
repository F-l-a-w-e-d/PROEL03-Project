using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna.Models
{
    public class Account
    {
        // for login
        public string AccountId { get; set; }
        public string Lname { get; set; }
        public string Mname {  get; set; }
        public string Fname {  get; set; }
        public string fullName => $"{Fname} {Mname} {Lname}";
        public string address {  get; set; }
        public DateTime BirthDate { get; set; }
        public string contactNo {  get; set; }
        public string user {  get; set; }
        public string pass {  get; set; }
        public string confirmPass {  get; set; }

        // Default BirthDate
        public Account()
        {
            BirthDate = DateTime.Now;
        }
    }
}
