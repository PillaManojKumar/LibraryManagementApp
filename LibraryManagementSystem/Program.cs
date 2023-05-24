using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using Spectre.Console;

namespace LibraryManagementSystem 
{
    public class LibraryManagementSystem : ILibraryManagementSystem
    {
        public string connectionString;
        public bool IsLoggedIn { get; set; }
        public LibraryManagementSystem(string connectionString)
        {
            this.connectionString = connectionString;
        }                     

        public void Login()
        {
            AnsiConsole.Write(new FigletText("Library Management System").Centered().Color(Spectre.Console.Color.Red));
            AnsiConsole.MarkupLine("[bold Blue]Enter login details[/]");
            AnsiConsole.MarkupLine("[bold yellow]Enter username:[/]");
            string username = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold yellow]Enter password:[/]");
            string password = Console.ReadLine();

            IsLoggedIn = ValidateUser(username, password);
        }
        public bool ValidateUser(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username and Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while validating the user: " + ex.Message);
                return false;
            }
        }

        public bool AddBook()
        {
            AnsiConsole.MarkupLine("[bold green]Enter book details:[/]");
            AnsiConsole.MarkupLine("[bold olive]Title:[/]");
            string title = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold olive]Author:[/]");
            string author = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold olive]Publication:[/]");
            string publication = Console.ReadLine();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Books (Title, Author, Publication, IsAvailable) VALUES (@Title, @Author, @Publication, 1)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Author", author);
                        command.Parameters.AddWithValue("@Publication", publication);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the book: " + ex.Message);
                return false;
            }
        }
        public bool EditBook()
        {
            AnsiConsole.MarkupLine("[bold orange4]Enter the Book ID to edit:[/]");
            int bookId;
            if (!int.TryParse(Console.ReadLine(), out bookId))
            {
                AnsiConsole.MarkupLine("[bold purple3]Invalid Book ID.[/]");
                return false;
            }

            AnsiConsole.MarkupLine("[bold purple3]Enter new book details:[/]");
            AnsiConsole.MarkupLine("[bold darkred_1]Title:[/]");
            string title = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold darkred_1]Author:[/]");
            string author = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold darkred_1]Publication:[/]");
            string publication = Console.ReadLine();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Books SET Title = @Title, Author = @Author, Publication = @Publication WHERE BookId = @BookId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Author", author);
                        command.Parameters.AddWithValue("@Publication", publication);
                        command.Parameters.AddWithValue("@BookId", bookId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            AnsiConsole.MarkupLine("[bold darkred_1]Book details updated successfully.[/]");
                            return true;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold darkred_1]Book not found.[/]");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while editing the book: " + ex.Message);
                return false;
            }
        }

        public bool DeleteBook()
        {
            AnsiConsole.MarkupLine("[bold gold3]Enter the Book ID to delete:[/]");
            int bookId;
            if (!int.TryParse(Console.ReadLine(), out bookId))
            {
                AnsiConsole.MarkupLine("[bold gold3]Invalid Book ID.[/]");
                return false;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Books WHERE BookId = @BookId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookId", bookId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            AnsiConsole.MarkupLine("[bold gold3]Book deleted successfully.[/]");
                            return true;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold gold3]Book not found.[/]");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting the book: " + ex.Message);
                return false;
            }
        }

        public bool AddStudent()
        {
            AnsiConsole.MarkupLine("[bold yellow3]Enter student details:[/]");
            AnsiConsole.MarkupLine("[bold yellow3]Name:[/]");
            string name = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold yellow3]Email:[/]");
            string email = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold yellow3]Phone number:[/]");
            long phnum = Convert.ToInt64(Console.ReadLine());

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT Into Students (Name, Email, Phonenumber) VALUES (@Name, @Email, @phnum)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@phnum", phnum);
                       

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            AnsiConsole.MarkupLine("[bold yellow3]Student added successfully.[/]");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the student: " + ex.Message);
            }
            return false;
        }

        public bool EditStudent()
        {
            AnsiConsole.MarkupLine("[bold mediumpurple1]Enter the Student ID to edit:[/]");
            int studentId;
            if (!int.TryParse(Console.ReadLine(), out studentId))
            {
                AnsiConsole.MarkupLine("[bold mediumpurple1]Invalid Student ID.[/]");
                return false;
            }

            AnsiConsole.MarkupLine("[bold mediumpurple1]Enter new student details:[/]");
            AnsiConsole.MarkupLine("[bold mediumpurple1]Name:[/]");
            string name = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold mediumpurple1]Email:[/]");
            string email = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold mediumpurple1]Phone number:[/]");
            long phnum = Convert.ToInt64(Console.ReadLine());

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Students SET Name = @Name, Email = @email, Phonenumber = @phnum WHERE StudentId = @StudentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@phnum", phnum);                        
                        command.Parameters.AddWithValue("@StudentId", studentId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            AnsiConsole.MarkupLine("[bold mediumpurple1]Student details updated successfully.[/]");
                            return true;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold mediumpurple1]Student not found.[/]");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while editing the student: " + ex.Message);
                return false;
            }
        }

        public bool DeleteStudent()
        {
            AnsiConsole.MarkupLine("[bold orangered1]Enter the Student ID to delete:[/]");
            int studentId;
            if (!int.TryParse(Console.ReadLine(), out studentId))
            {
                AnsiConsole.MarkupLine("[bold orangered1]Invalid Student ID.[/]");
                return false;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Students WHERE StudentId = @StudentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", studentId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            AnsiConsole.MarkupLine("[bold orangered1]Student deleted successfully.[/]");
                            return true;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold orangered1]Student not found.[/]");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting the student: " + ex.Message);
                return false;
            }
        }

        public void IssueBook()
        {
            AnsiConsole.MarkupLine("[bold orchid1]Enter the Student ID:[/]");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                AnsiConsole.MarkupLine("[bold orchid1]Invalid Student ID.[/]");
                return;
            }

            // Check if the student exists in the database
            bool isStudentValid = CheckStudentExistence(studentId);
            if (!isStudentValid)
            {
                AnsiConsole.MarkupLine("[bold orchid1]Invalid Student ID. The student does not exist.[/]");
                return;
            }

            AnsiConsole.MarkupLine("[bold orchid1]Enter the Book ID to issue:[/]");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                AnsiConsole.MarkupLine("[bold orchid1]Invalid Book ID.[/]");
                return;
            }

            // Check if the book exists and is available
            bool isBookAvailable = CheckBookAvailability(bookId);
            if (!isBookAvailable)
            {
                AnsiConsole.MarkupLine("[bold orchid1]The book is not available.[/]");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string issueQuery = "INSERT INTO IssuedBooks (StudentId, BookId, IssueDate) VALUES (@StudentId, @BookId, @IssueDate)";
                    using (SqlCommand issueCommand = new SqlCommand(issueQuery, connection))
                    {
                        issueCommand.Parameters.AddWithValue("@StudentId", studentId);
                        issueCommand.Parameters.AddWithValue("@BookId", bookId);
                        issueCommand.Parameters.AddWithValue("@IssueDate", DateTime.Today);

                        issueCommand.ExecuteNonQuery();
                    }

                    string updateQuery = "UPDATE Books SET IsAvailable = 0 WHERE BookId = @BookId";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@BookId", bookId);

                        updateCommand.ExecuteNonQuery();
                    }

                    AnsiConsole.MarkupLine("[bold orchid1]Book issued successfully.[/]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while issuing the book: " + ex.Message);
            }
        }

        private bool CheckStudentExistence(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Students WHERE StudentId = @StudentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private bool CheckBookAvailability(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT IsAvailable FROM Books WHERE BookId = @BookId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookId", bookId);
                    bool isAvailable = (bool)(command.ExecuteScalar() ?? false);

                    return isAvailable;
                }
            }
        }
        public void ReturnBook()
        {
            AnsiConsole.MarkupLine("[bold orange1]Enter the Book ID to return:[/]");
            int bookId;
            if (!int.TryParse(Console.ReadLine(), out bookId))
            {
                AnsiConsole.MarkupLine("[bold orange1]Invalid Book ID.[/]");
                return;
            }

            AnsiConsole.MarkupLine("[bold orange1]Enter the Student ID to return:[/]");
            int studentId;
            if (!int.TryParse(Console.ReadLine(), out studentId))
            {
                AnsiConsole.MarkupLine("[bold orange1]Invalid Student ID.[/]");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string issuedQuery = "SELECT COUNT(*) FROM IssuedBooks WHERE BookId = @BookId AND StudentId = @StudentId";
                    using (SqlCommand issuedCommand = new SqlCommand(issuedQuery, connection))
                    {
                        issuedCommand.Parameters.AddWithValue("@BookId", bookId);
                        issuedCommand.Parameters.AddWithValue("@StudentId", studentId);
                        int count = (int)issuedCommand.ExecuteScalar();

                        if (count == 0)
                        {
                            AnsiConsole.MarkupLine("[bold orange1]The book is not issued.[/]");
                            return;
                        }
                    }

                    string returnQuery = "UPDATE IssuedBooks SET ReturnDate = @ReturnDate WHERE BookId = @BookId AND StudentId = @StudentId AND ReturnDate IS NULL";
                    using (SqlCommand returnCommand = new SqlCommand(returnQuery, connection))
                    {
                        returnCommand.Parameters.AddWithValue("@ReturnDate", DateTime.Today);
                        returnCommand.Parameters.AddWithValue("@BookId", bookId);
                        returnCommand.Parameters.AddWithValue("@StudentId", studentId);

                        int rowsAffected = returnCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string updateQuery = "UPDATE Books SET IsAvailable = 1 WHERE BookId = @BookId";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@BookId", bookId);
                                updateCommand.ExecuteNonQuery();
                            }
                            AnsiConsole.MarkupLine("[bold orange1]Book returned successfully.[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold orange1]Book not found or already returned.[/]");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while returning the book: " + ex.Message);
            }
        }


        public void SearchBooksByAuthorOrPublication()
        {
            AnsiConsole.MarkupLine("[bold lightpink1]Enter the author or publication name:[/] ");
            string searchKey = Console.ReadLine();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Books WHERE Author LIKE '%' + @SearchKey + '%' OR Publication LIKE '%' + @SearchKey + '%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchKey", searchKey);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            AnsiConsole.MarkupLine("[bold lightpink1]Search results:[/]");
                            Console.WriteLine("BookId\tTitle\tAuthor\t\tPublication\tIsAvailable");
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}",
                                    reader["BookId"], reader["Title"], reader["Author"],
                                    reader["Publication"], reader["IsAvailable"]);
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold lightpink1]No books found with the given author or publication name. [/]");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while searching for books: " + ex.Message);
            }
        }
        public void SearchStudentById()
        {
            AnsiConsole.MarkupLine("[bold hotpink3_1]Enter the student Id: [/]");
            string StudentId = Console.ReadLine();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Students WHERE StudentId = @StudentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", StudentId);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            AnsiConsole.MarkupLine("[bold hotpink3_1]Search results:[/] ");
                            Console.WriteLine("StudentId\tName\tEmail\t\tPhonenumber");
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t\t{1}\t{2}\t{3}",
                                    reader["StudentId"], reader["Name"], reader["Email"], reader["Phonenumber"]);
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold hotpink3_1]No student found with the given StudentId.[/] ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while searching for students: " + ex.Message);
            }
        }
        public void GetStudentsWithIssuedBooks()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Students.StudentId, Students.Name, COUNT(*) AS BookCount " +
                                    "FROM Students " + "INNER JOIN IssuedBooks ON Students.StudentId = IssuedBooks.StudentId " +
                                    "GROUP BY Students.StudentId, Students.Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            AnsiConsole.MarkupLine("[bold red3_1]Students with issued books:[/] ");
                            Console.WriteLine("StudentId\tStudentName\tBookCount");
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t\t{1}\t\t{2}",
                                    reader["StudentId"], reader["Name"], reader["BookCount"]);
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold red3_1]No students have issued books.[/] ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving students with issued books: " + ex.Message);
            }
        }
        public static void Main(string[] args)
        {
            string connectionString = "Data Source=IN-DQ3K9S3; Initial Catalog=LibraryManagementDB; Integrated Security=True;";
            LibraryManagementSystem library = new LibraryManagementSystem(connectionString);

            bool loggedIn = false;

            while (!loggedIn)
            {
                library.Login();

                if (library.IsLoggedIn)
                {
                    loggedIn = true;
                    AnsiConsole.MarkupLine("[bold yellow3]Login successful![/] ");
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold yellow3]Invalid username or password. Please try again.[/] ");
                }
            }

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Edit Book");
                Console.WriteLine("3. Delete Book");
                Console.WriteLine("4. Add Student");
                Console.WriteLine("5. Edit Student");
                Console.WriteLine("6. Delete Student");
                Console.WriteLine("7. Issue Book");
                Console.WriteLine("8. Return Book");
                Console.WriteLine("9. Search Books by Author or Publication");
                Console.WriteLine("10. Search Student Id");
                Console.WriteLine("11. Get Students with Issued Books");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:                        
                        bool success = library.AddBook();
                        if (success)
                        {
                            AnsiConsole.MarkupLine("[bold green]Book added successfully.[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold red]Failed to add the book.[/]");
                        }
                        break;
                    case 2:
                        library.EditBook();
                        break;
                    case 3:
                        library.DeleteBook();
                        break;
                    case 4:
                        library.AddStudent();
                        break;
                    case 5:
                        library.EditStudent();
                        break;
                    case 6:
                        library.DeleteStudent();
                        break;
                    case 7:
                        library.IssueBook();
                        break;
                    case 8:
                        library.ReturnBook();
                        break;
                    case 9:
                        library.SearchBooksByAuthorOrPublication();
                        break;
                    case 10:
                        library.SearchStudentById();
                        break;
                    case 11:
                        library.GetStudentsWithIssuedBooks();
                        break;
                    case 0:
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid number.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}