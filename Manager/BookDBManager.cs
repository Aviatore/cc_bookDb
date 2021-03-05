using Codecool.BookDb.Model;
using Codecool.BookDb.View;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Codecool.BookDb.Manager
{
    public class BookDBManager
    {
        private readonly UserInterface _ui;
        private IAuthorDao _authorDao;
        private IAuthorDao _bookDao;

        public BookDBManager(UserInterface ui)
        {
            _ui = ui;
        }

        public void Run()
        {
            try
            {
                Connect();
            }
            catch (SqlException)
            {
                Console.WriteLine("Could not connect to the database.");
            }

            bool running = true;

            while(running)
            {
                _ui.PrintTitle("Main Menu");
                _ui.PrintOption('a', "Authors");
                _ui.PrintOption('b', "Books");
                _ui.PrintOption('q', "Quit");

                switch(_ui.Choice("abq"))
                    {
                    case 'a':
                        new AuthorManager(_ui, _authorDao).Run();
                        break;
                    case 'q':
                        running = false;
                        break;
                }
            }
        }

        public void Connect()
        {
            string connectionString = ConnectionString;
            _authorDao = new AuthorDAO(connectionString);
        }
        private string ConnectionString => ConfigurationManager.AppSettings["connectionString"];
    }
}
