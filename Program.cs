using Codecool.BookDb.Manager;
using Codecool.BookDb.Model;
using Codecool.BookDb.View;
using Microsoft.Data.SqlClient;
using System;
using System.Configuration;

namespace Codecool.BookDb
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            new BookDBManager(ui).Run();
        }
    }
}
