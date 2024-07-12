using System;
using System.Collections.Generic;
using System.Text;

namespace Mailjet.Client.TransactionalEmails.Models
{
    public class Variable
    {
        public Variable(object obj)
        {
            Value = obj;
        }

        public object Value { get; private set; }
    }
}
