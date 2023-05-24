using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public interface ILibraryManagementSystem
    {
        bool ValidateUser(string username, string password);
        bool AddBook();
        bool EditBook();
        bool DeleteBook();
        bool AddStudent();
        bool EditStudent();
        bool DeleteStudent();

    }
}
