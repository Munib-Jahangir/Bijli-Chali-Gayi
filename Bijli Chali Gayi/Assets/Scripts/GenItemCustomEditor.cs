using UnityEditor;

[CustomEditor(typeof(GeneratorSystem.GenItem))]
public class GenItemCustomEditor : Editor
{
    SerializedProperty itemType;

    SerializedProperty canBurnFuel;
    SerializedProperty burnRate;
    SerializedProperty canRumble;
    SerializedProperty rumbleSpeed;
    SerializedProperty rumbleIntensity;

    SerializedProperty itemFuelAmount;
    SerializedProperty itemMaxFuelAmount;

    SerializedProperty showUI;
    SerializedProperty popoutUI;

    SerializedProperty sound1;
    SerializedProperty sound2;

    SerializedProperty activateGenerator;
    SerializedProperty deactivateGenerator;

    bool generatorSelection, fuelParameters, soundGroup, generatorEvents;

    void OnEnable()
    {
        itemType = serializedObject.FindProperty("itemType");

        canBurnFuel = serializedObject.FindProperty("canBurnFuel");
        burnRate = serializedObject.FindProperty("burnRate");

        canRumble = serializedObject.FindProperty("canRumble");
        rumbleSpeed = serializedObject.FindProperty("rumbleSpeed");
        rumbleIntensity = serializedObject.FindProperty("rumbleIntensity");

        itemFuelAmount = serializedObject.FindProperty("itemFuelAmount");
        itemMaxFuelAmount = serializedObject.FindProperty("itemMaxFuelAmount");

        showUI = serializedObject.FindProperty("showUI");
        popoutUI = serializedObject.FindProperty("_popoutUI");

        sound1 = serializedObject.FindProperty("fuelSwishSound");
        sound2 = serializedObject.FindProperty("waterPourSound");

        activateGenerator = serializedObject.FindProperty("activateGenerator");
        deactivateGenerator = serializedObject.FindProperty("deactivateGenerator");

    }

    public override void OnInspectorGUI()
    {
        GeneratorSystem.GenItem _generatorItem = (GeneratorSystem.GenItem)target;

        EditorGUILayout.PropertyField(itemType);

        EditorGUILayout.PropertyField(itemFuelAmount);
        EditorGUILayout.PropertyField(itemMaxFuelAmount);

        if (_generatorItem.itemType == GeneratorSystem.GenItem.GeneratorItemType.Generator)
        {
            EditorGUILayout.Space(5);
            generatorSelection = EditorGUILayout.BeginFoldoutHeaderGroup(generatorSelection, "Generator Parameters");
            if (generatorSelection)
            {
                EditorGUILayout.PropertyField(canBurnFuel);
                if(_generatorItem.canBurnFuel)
                {
                    EditorGUILayout.PropertyField(burnRate);
                }

                EditorGUILayout.Space(5);

                EditorGUILayout.PropertyField(canRumble);
                if (_generatorItem.canRumble)
                {
                    EditorGUILayout.PropertyField(rumbleSpeed);
                    EditorGUILayout.PropertyField(rumbleIntensity);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        EditorGUILayout.PropertyField(showUI);

        if (_generatorItem.showUI)
        {
            EditorGUILayout.PropertyField(popoutUI);
        }

        EditorGUILayout.Space(5);

        soundGroup = EditorGUILayout.Foldout(soundGroup, "Sound Effects");
        if (soundGroup)
        {
            EditorGUILayout.PropertyField(sound1);
            EditorGUILayout.PropertyField(sound2);
        }

        if (_generatorItem.itemType == GeneratorSystem.GenItem.GeneratorItemType.Generator)
        {
            EditorGUILayout.Space(5);
            generatorEvents = EditorGUILayout.BeginFoldoutHeaderGroup(generatorEvents, "Generator Events");
            if (generatorEvents)
            {
                EditorGUILayout.PropertyField(activateGenerator);
                EditorGUILayout.PropertyField(deactivateGenerator);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
