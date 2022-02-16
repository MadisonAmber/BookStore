using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class EFBookProjectRepository : IBookProjectRepository
    {
        private BookstoreContext context { get; set; }
        public EFBookProjectRepository(BookstoreContext ctx) => context = ctx;

        public IQueryable<Book> Books => context.Books;
    }
}
