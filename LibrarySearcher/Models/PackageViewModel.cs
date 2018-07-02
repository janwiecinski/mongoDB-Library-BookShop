using System;
using System.Collections.Generic;

namespace LibrarySearcher.Models
{
    public class PackageViewModel
    {
        public IList<BookListItemViewModel> Books { get; set; }

        public DateTime CreatedOn
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

       public string CreatedBy { get; set; }
    }
}
