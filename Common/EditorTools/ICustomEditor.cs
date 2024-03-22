using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

public interface ICustomEditor
{
    SerializedProperty FindProperty(string propertyName);
}
