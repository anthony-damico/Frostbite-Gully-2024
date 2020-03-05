using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


namespace FbGDialogSystem
{

    public class FableDictionary : ScriptableObject
    {
        Dictionary<string, string> _fableDictionary;

        

        #region Helper Functions

        public bool CreateEntry(string key, string value)
        {
            if (_fableDictionary.ContainsKey(key))
            {
                return false;
            }
            else
            {
                _fableDictionary.Add(key, value);
                return true;
            }
        }


        public bool UpdateEntry(string key, string value)
        {
            if(_fableDictionary.ContainsKey(key))
            {
                return false;
            }
            else
            {
                _fableDictionary.Remove(key);
                _fableDictionary.Add(key, value);
                return true;
            }
        }
        #endregion
    }
}
