using System;
using System.Collections;
using System.Collections.Generic;

namespace Mailjet.Client.TransactionalEmails.Models
{
    public class VariableDictionary :
         IDictionary<string, Variable>,
         ICollection<KeyValuePair<string, Variable>>,
         IReadOnlyDictionary<string, Variable>,
         IReadOnlyCollection<KeyValuePair<string, Variable>>,
         IEnumerable<KeyValuePair<string, Variable>>,
         IDictionary,
         ICollection,
         IEnumerable
    {
        #region Fields
        private IDictionary<string, Variable> _dictionary;
        #endregion

        #region Ctr
        public VariableDictionary()
        {
            _dictionary = new Dictionary<string, Variable>();
        }

        public VariableDictionary(VariableDictionary dictionary)
        {
            _dictionary = new Dictionary<string, Variable>(dictionary);
        }

        public VariableDictionary(IDictionary<string, Variable> dictionary)
        {
            _dictionary = dictionary;
        }

        public VariableDictionary(IEqualityComparer<string> comparer)
        {
            _dictionary = new Dictionary<string, Variable>(comparer);
        }

        public VariableDictionary(int capacity)
        {
            _dictionary = new Dictionary<string, Variable>(capacity);
        }

        public VariableDictionary(IDictionary<string, Variable> dictionary, IEqualityComparer<string> comparer)
        {
            _dictionary = new Dictionary<string, Variable>(dictionary, comparer);
        }

        public VariableDictionary(int capacity, IEqualityComparer<string> comparer)
        {
            _dictionary = new Dictionary<string, Variable>(capacity, comparer);
        }
        #endregion

        #region Properties
        public Variable this[string key] { get => _dictionary[key]; set => _dictionary[key] = value; }
        public object this[object key] { get => ((IDictionary)_dictionary)[key]; set => ((IDictionary)_dictionary)[key] = value; }

        public ICollection<string> Keys => _dictionary.Keys;

        public ICollection<Variable> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => _dictionary.IsReadOnly;

        public bool IsFixedSize => ((IDictionary)_dictionary).IsFixedSize;

        public bool IsSynchronized => ((IDictionary)_dictionary).IsSynchronized;

        public object SyncRoot => ((IDictionary)_dictionary).SyncRoot;

        IEnumerable<string> IReadOnlyDictionary<string, Variable>.Keys => _dictionary.Keys;

        ICollection IDictionary.Keys => ((IDictionary)_dictionary).Keys;

        IEnumerable<Variable> IReadOnlyDictionary<string, Variable>.Values => _dictionary.Values;

        ICollection IDictionary.Values => ((IDictionary)_dictionary).Values;
        #endregion
        #region Methods
        public void Add(string key, Variable value)
        {
            _dictionary.Add(key, value);
        }

        public void Add(KeyValuePair<string, Variable> item)
        {
            _dictionary.Add(item);
        }

        public void Add(object key, object value)
        {
            ((IDictionary)_dictionary).Add(key, value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, Variable> item)
        {
            return _dictionary.Contains(item);
        }

        public bool Contains(object key)
        {
            return ((IDictionary)_dictionary).Contains(key);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Variable>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            ((IDictionary)_dictionary).CopyTo(array, index);
        }

        public IEnumerator<KeyValuePair<string, Variable>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return _dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<string, Variable> item)
        {
            return _dictionary.Remove(item);
        }

        public void Remove(object key)
        {
            ((IDictionary)_dictionary).Remove(key);
        }

        public bool TryGetValue(string key, out Variable value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)_dictionary).GetEnumerator();
        }
        #endregion

        #region Operators
        public static implicit operator VariableDictionary(Dictionary<string, Variable> dictionary)
        {
            var newDictionary = new VariableDictionary();
            foreach (var key in dictionary.Keys)
            {
                var value = dictionary[key];
                newDictionary.Add(key, value);
            }
            return newDictionary;
        }

        public static implicit operator VariableDictionary(Dictionary<string, object> dictionary)
        {
            var newDictionary = new VariableDictionary();
            foreach (var key in dictionary.Keys)
            {
                var value = dictionary[key];
                newDictionary.Add(key, value);
            }
            return newDictionary;
        }

        public static implicit operator Dictionary<string, Variable>(VariableDictionary dictionary)
        {
            var newDictionary = new Dictionary<string, Variable>();
            foreach (var key in dictionary.Keys)
            {
                var value = dictionary[key];
                newDictionary.Add(key, value);
            }
            return newDictionary;
        }

        public static implicit operator Dictionary<string, object>(VariableDictionary dictionary)
        {
            var newDictionary = new Dictionary<string, object>();
            foreach (var key in dictionary.Keys)
            {
                var value = dictionary[key];
                newDictionary.Add(key, value.Value);
            }
            return newDictionary;
        }
        #endregion
    }
}
