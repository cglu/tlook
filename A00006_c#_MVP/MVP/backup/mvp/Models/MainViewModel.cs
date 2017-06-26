using System;
using System.Collections.Generic;

namespace WinformsMvpBasics.Models
{
    public class MainViewModel
    {
        public IList<KeyValuePair<Type, String>> MenuItems { get; set; }
    }
}
