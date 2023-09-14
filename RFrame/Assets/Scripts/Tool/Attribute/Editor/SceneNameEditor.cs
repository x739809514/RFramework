using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] nameList = (this.attribute as SceneNameAttribute)?.NameList;
        if (property.propertyType == SerializedPropertyType.String)
        {
            int selectIndex = Mathf.Max(0, Array.IndexOf<string>(nameList, property.stringValue));
            int index = EditorGUI.Popup(position, property.displayName, selectIndex, nameList);
            property.stringValue = nameList[index];
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, nameList);
        }
        else
        {
            base.OnGUI(position, property, label);
        }
    }
}