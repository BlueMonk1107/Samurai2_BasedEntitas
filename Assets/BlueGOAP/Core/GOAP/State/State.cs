
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BlueGOAP
{
    public class State : IState
    {
        private Dictionary<string, bool> _dataTable;
        private Action _onChange;

        public State()
        {
            _dataTable = new Dictionary<string, bool>();
        }

        public void Set(string key, bool value)
        {
            if (_dataTable.ContainsKey(key) && _dataTable[key] != value)
            {
                ChangeValue(key, value);
            }
            else if(!_dataTable.ContainsKey(key))
            {
                ChangeValue(key, value);
            }
        }

        private void ChangeValue(string key, bool value)
        {
            _dataTable[key] = value;
            if (_onChange != null)
                _onChange();
        }

        public void AddStateChangeListener(Action onChange)
        {
            _onChange = onChange;
        }

        public IState GetSameData(IState otherState)
        {
            IState data = new State();
            foreach (var entry in _dataTable)
            {
                if (otherState.ContainKey(entry.Key))
                {
                    data.Set(entry.Key, entry.Value);
                }
            }

            return data;
        }

        public IState InversionValue() 
        {
            IState state = new State();
            foreach (KeyValuePair<string, bool> pair in _dataTable)
            {
                state.Set(pair.Key, !pair.Value);
            }

            return state;
        }

        public bool Get(string key)
        {
            if (!_dataTable.ContainsKey(key))
            {
                DebugMsg.LogError("state not contain the key:" + key);
                return false;
            }
            return _dataTable[key];
        }

        public bool ContainState(IState otherState)
        {
            foreach (string key in otherState.GetKeys())
            {
                DebugMsg.Log("otherState key:"+ key+"   "+ otherState.Get(key));
            }
            foreach (var key in otherState.GetKeys())
            {
                if (ContainKey(key))
                {
                    DebugMsg.Log("key  " + key + "   当前状态的值  " + _dataTable[key] + "  另一状态的值 " + otherState.Get(key));
                }
            }

            foreach (var key in otherState.GetKeys())
            {
                if (!ContainKey(key) || _dataTable[key] != otherState.Get(key))
                {
                    return false;
                }
            }

            return true;
        }

        public ICollection<string> GetValueDifferences(IState otherState)
        {
            List<string> keys = new List<string>();
            foreach (var key in otherState.GetKeys())
            {
                if (!_dataTable.ContainsKey(key) || otherState.Get(key) != _dataTable[key])
                {
                    keys.Add(key);
                }
            }

            return keys;
        }

        public ICollection<string> GetNotExistKeys(IState otherState)
        {
            List<string> keys = new List<string>();
            foreach (var key in otherState.GetKeys())
            {
                if (!_dataTable.ContainsKey(key))
                {
                    keys.Add(key);
                }
            }
            return keys;
        }

        public void Set(IState otherState)
        {
            foreach (var key in otherState.GetKeys())
            {
                Set(key, otherState.Get(key));
            }
        }

        public bool ContainKey(string key)
        {
            return _dataTable.ContainsKey(key);
        }

        public ICollection<string> GetKeys()
        {
            return _dataTable.Keys;
        }

        public void Copy(IState otherState)
        {
            Clear();
            Set(otherState);
        }

        public void Clear()
        {
            _dataTable.Clear();
        }

        public override string ToString()
        {
            StringBuilder temp = new StringBuilder();
            foreach (KeyValuePair<string, bool> pair in _dataTable)
            {
                temp.Append("key:");
                temp.Append(pair.Key);
                temp.Append("        value:");
                temp.Append(pair.Value);
                temp.Append("\r\n");
            }

            return temp.ToString();
        }
    }

    public class State<TKey> : State
    {
        public State() : base()
        {

        }

        public void Set(TKey key, bool value)
        {
            base.Set(key.ToString(), value);
        }

        public bool Get(TKey key)
        {
            return base.Get(key.ToString());
        }

        public bool ContainKey(TKey key)
        {
            return base.ContainKey(key.ToString());
        }
    }
}
