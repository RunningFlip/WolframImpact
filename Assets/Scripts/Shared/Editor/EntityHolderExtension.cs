using UnityEngine;
using UnityEditor;


[CanEditMultipleObjects]
[CustomEditor(typeof(GameObjectEntityHolder))]
public class EntityHolderExtension : Editor
{
    //Flag
    private bool init;

    //Properties
    private SerializedProperty entityID;
    private SerializedProperty archeType;
    private SerializedProperty entityComponentsProperty;

    //GUI Styles
    private GUIStyle headerStyle;


    private void OnEnable()
    {
        //Properties
        entityID = serializedObject.FindProperty("entityId");
        archeType = serializedObject.FindProperty("archeType");
        entityComponentsProperty = serializedObject.FindProperty("entityComponents");

        //Styles
        SetupGUIStyles();

        //Flag
        init = true;
    }


    public override void OnInspectorGUI()
    {
        EditorUtility.SetDirty(target);

        if (!init) return;

        //It is required to connect SerializedProperty fields with actual object properties using serializedObject. 
        serializedObject.UpdateIfRequiredOrScript();

        //Target
        GameObjectEntityHolder entityHolder = (GameObjectEntityHolder)target; //Original tile
        if (entityHolder == null) return;

        //SECTION-------------HEADER-----------------------------------------------
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Entity", headerStyle);

        //SECTION-------------Entity-----------------------------------------------
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Endity ID");
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.IntField(entityID.intValue);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        //SECTION-------------ArcheType--------------------------------------------
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Component ArcheType", headerStyle);
        EditorGUI.BeginDisabledGroup(!Application.isEditor && Application.isPlaying);
        EditorGUILayout.PropertyField(archeType);
        EditorGUI.EndDisabledGroup();

        //SECTION-------------Components-------------------------------------------
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Entity Components", headerStyle);
        ShowList(entityComponentsProperty);

        //Apply modified properties
        serializedObject.ApplyModifiedProperties();
    }


    /// <summary>
    /// Shows a given list in the inspector.
    /// </summary>
    /// <param name="_serializedList"></param>
    private void ShowList(SerializedProperty _serializedList)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Component Count");
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.IntField(_serializedList.arraySize);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(_serializedList);
        for (int i = 0; i < _serializedList.arraySize; i++)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_serializedList.GetArrayElementAtIndex(i));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        EditorGUI.indentLevel--;
    }


    /// <summary>
    /// Makes a setup for all guistyles.
    /// </summary>
    private void SetupGUIStyles()
    {
        //Header
        headerStyle = new GUIStyle();
        headerStyle.fontSize = 11;
        headerStyle.fontStyle = FontStyle.Bold;
    }
}
