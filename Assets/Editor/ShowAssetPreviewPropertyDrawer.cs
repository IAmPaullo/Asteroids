using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AssetPreviewAttribute))]
public class ShowAssetPreviewPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label);

        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue != null)
        {
            AssetPreviewAttribute previewAttribute = (AssetPreviewAttribute)attribute;
            float previewHeight = previewAttribute.Height;

            Rect previewRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, previewHeight);
            Texture2D previewTexture = AssetPreview.GetAssetPreview(property.objectReferenceValue);

            if (previewTexture != null)
            {
                GUI.DrawTexture(previewRect, previewTexture, ScaleMode.ScaleToFit);
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        AssetPreviewAttribute previewAttribute = (AssetPreviewAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue != null)
        {
            return EditorGUIUtility.singleLineHeight + previewAttribute.Height + 4;
        }

        return EditorGUIUtility.singleLineHeight;
    }
}
