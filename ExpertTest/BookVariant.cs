using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertTest
{
    class BookVariant
    {
        public int Id;
        public float Result;
        public BookVariant(int id, int result)
        {
            Id = id;
            Result = result;
        }

        public BookVariant() { }
    }
}
