using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtyNamesTest.LoggingManagement.Behaviours
{
    internal class BehaviourAttribute : Attribute
    {
        #region Constructor

        public BehaviourAttribute(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        public string Name { get; }

        #endregion
    }
}
