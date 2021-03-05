using Codecool.BookDb.Model;
using Codecool.BookDb.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.BookDb.Manager
{
    public class AuthorManager : Manager
    {
        private readonly IAuthorDao _authorDao;

        public AuthorManager(UserInterface ui, IAuthorDao authorDao) : base(ui)
        {
            _authorDao = authorDao;
        }

        protected override void Add()
        {
            string firstName = _ui.ReadString("First name", "X");
            string lastName = _ui.ReadString("Last name", "Y");
            DateTime defaultDate = new DateTime(1900, 5, 28);
            DateTime birthDate = _ui.ReadDate("Birth date", defaultDate.Date);
            _authorDao.Add(new Author(firstName, lastName, birthDate));
        }

        protected override void Edit()
        {
            int id = _ui.ReadInt("Author ID", 0);
            Author author = _authorDao.Get(id);
            if (author == null)
            {
                _ui.PrintLn("Author not found");
                return;
            }

            _ui.PrintLn(author);

            string firstName = _ui.ReadString("First name", author.FirstName);
            string lastName = _ui.ReadString("Last name", author.LastName);
            DateTime birthDate = _ui.ReadDate("Birth date", author.BirthDate);

            author.FirstName = firstName;
            author.LastName = lastName;
            author.BirthDate = birthDate;

            _authorDao.Update(author);
        }

        protected override string GetName()
        {
            return "Author Manager";
        }

        protected override void List()
        {
            foreach(Author author in _authorDao.GetAll())
            {
                _ui.PrintLn(author);
            }
        }
    }
}
