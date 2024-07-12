namespace Mailjet.Client.TransactionalEmails.Models
{
    public class Variable
    {
        public Variable(object obj)
        {
            Value = obj;
        }

        public object Value { get; private set; }

        //public static implicit operator Dictionary<string, object>(Dictionary<string, Variable> keyValuePairs) => {}
    }

    
}
