using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertTest
{
    class UserAnswer
    {
        public int PropertyId;
        public int QuestionId;
        public UserAnswer(int pID, int qID)
        {
            this.PropertyId = pID;
            this.QuestionId = qID;
        }
    }
}
