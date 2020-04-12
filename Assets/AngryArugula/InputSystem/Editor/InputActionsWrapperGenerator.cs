/*
MIT License

Copyright(c) 2019 Mitchel Thompson
www.angryarugula.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using System.IO;
using System.Text;

namespace Arugula.Peripherals
{


    public class InputActionsWrapperGenerator
    {
        [MenuItem("Assets/Create/Input Actions Wrapper")]
        static void CreateWrapperScript()
        {
            var inputActionAsset = (InputActionAsset)Selection.activeObject;

            if (inputActionAsset.actionMaps.Count != 1)
            {
                Debug.LogError("InputActionAsset must have only one ActionMap for now!");
                return;
            }

            StringBuilder s = new StringBuilder();
            s.AppendLine("using Arugula.Peripherals;");
            s.AppendFormat("public class {0} : InputActionsWrapper\r\n", inputActionAsset.name);
            s.AppendLine("{");

            var map = inputActionAsset.actionMaps[0];

            foreach (var a in map.actions)
            {
                if (a.type == InputActionType.Button)
                {
                    s.AppendFormat("    public Button {0} = new Button(\"{1}\");\r\n", a.name, a.id.ToString());
                }
                else if (a.type == InputActionType.Value)
                {
                    switch (a.expectedControlType)
                    {
                        case "Button":
                            s.AppendFormat("    public Button {0} = new Button(\"{1}\");\r\n", a.name, a.id.ToString());
                            break;
                        case "Analog":
                        case "Axis":
                            s.AppendFormat("    public Axis {0} = new Axis(\"{1}\");\r\n", a.name, a.id.ToString());
                            break;
                        case "Dpad":
                        case "Stick":
                        case "Vector2":
                            s.AppendFormat("    public Stick {0} = new Stick(\"{1}\");\r\n", a.name, a.id.ToString());
                            break;
                        default:
                            Debug.LogWarning("Cannot handle control type of: " + a.expectedControlType + " yet.");
                            break;
                    }
                }
            }

            s.Append("}");

            string assetPath = AssetDatabase.GetAssetPath(inputActionAsset);
            string assetDir = Path.GetDirectoryName(assetPath);
            string wrapperPath = Path.Combine(assetDir, inputActionAsset.name + ".cs");

            if (File.Exists(wrapperPath))
            {
                bool overwrite = EditorUtility.DisplayDialog("File Exists", string.Format("{0} already exists.  Overwrite?", Path.GetFileName(wrapperPath)), "Overwrite", "Cancel");
                if (!overwrite)
                    return;
            }

            File.WriteAllText(wrapperPath, s.ToString());

            AssetDatabase.ImportAsset(wrapperPath);

            MonoImporter importer = (MonoImporter)AssetImporter.GetAtPath(wrapperPath);
            importer.SetDefaultReferences(new string[] { "inputActionAsset" }, new Object[] { inputActionAsset });
            importer.SaveAndReimport();
        }

        [MenuItem("Assets/Create/Input Actions Wrapper", true)]
        static bool CreateWrapperScriptValidator()
        {
            return Selection.activeObject != null && Selection.activeObject.GetType() == typeof(InputActionAsset);
        }
    }
}