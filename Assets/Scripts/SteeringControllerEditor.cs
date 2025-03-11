using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SteeringController))]
public class SteeringControllerEditor : Editor
{
    private static Type[] behaviorTypes;

    private void OnEnable()
    {
        behaviorTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(SteeringBehavior)) && !type.IsAbstract)
            .ToArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Obten la property de behaviorsList y cambiala segun lo que se definira en el codigo.
        SteeringController controller = (SteeringController)target;
        SerializedProperty behaviorsList = serializedObject.FindProperty("behaviors");

        // 1. Agrega una etiqueta Steering Behaviors con estilo "negrita"
        EditorGUILayout.LabelField("Steering Behaviors", EditorStyles.boldLabel);

        // 2. Itera para todos los elementos de Behavior List y aplica:
        for (int i = 0; i < behaviorsList.arraySize; i++)
        {
            // 2.1 Obten el nombre del Behavior agregado, Seek, Flee, etc
            SerializedProperty element = behaviorsList.GetArrayElementAtIndex(i);
            string behaviorName = controller.behaviors[i].GetType().Name;

            // 2.2 Inicia un elemento vertical para agrupar visualmente diferentes elementos.
            EditorGUILayout.BeginVertical(GUI.skin.box);
            
            // 2.3 Inicia un elemento horizontal para agrupar visualmente diferentes elementos.
            EditorGUILayout.BeginHorizontal();
            
                // 2.3.1 Agrega una etiqueta con el nombre del behavior y un numero segun su cantidad del mismo. (en Horizontal)
            EditorGUILayout.LabelField($"{behaviorName} {i + 1}", EditorStyles.boldLabel); 
            
                // 2.3.2 Asigna al siguiente elemento como tipo Flexible (recorrelo hasta la derecha)
            GUILayout.FlexibleSpace();
            
                // 2.3.3 Agrega un boton "Remove", si es presionado, elimina el elemento seleccionado de la lista.
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                controller.behaviors.RemoveAt(i);
                break;
            }
            
                // 2.3.4 Termina el elemento horizontal, teniendo en este, la etiqueta y el boton.
            EditorGUILayout.EndHorizontal();

                // 2.2.1 Obten un iterador de todos los elementos Serializados.
            SerializedProperty iterator = element.Copy();

                // 2.2.2 Obten el final de cada uno de las propiedades (fields)
            SerializedProperty endProperty = iterator.GetEndProperty();
            
                // 2.2.3 Aplica a las propiedades lo que este en el codigo.
            while (iterator.NextVisible(true) && !SerializedProperty.EqualContents(iterator, endProperty))
            {
                // 2.2.4 Inicia un elemento horizontal
                EditorGUILayout.BeginHorizontal();
                // 2.2.5 Agrega una etiqueta de tamano 80
                EditorGUILayout.LabelField(iterator.displayName, GUILayout.Width(80));
                // 2.2.6 Agrega un input del valor de la etiqueta y expandelo. 
                EditorGUILayout.PropertyField(iterator, GUIContent.none, GUILayout.ExpandWidth(true)); // Expandir solo el campo de entrada
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        // 3. Agrega un salto de linea 
        EditorGUILayout.Space();

        // 4 Agrega una etiqueta
        EditorGUILayout.LabelField("Add New Behavior", EditorStyles.boldLabel);


        // 5. Agrega un boton por cada archivo script Steering Behaviors que exita en tu proyecto.
        foreach (var type in behaviorTypes)
        {
            if (GUILayout.Button($"Add {type.Name}"))
            {
                SteeringBehavior newBehavior = (SteeringBehavior)Activator.CreateInstance(type);
                controller.behaviors.Add(newBehavior);
            }
        }

        // 6. Renderiza lo definido en el codigo, aplicado hacia la lista.
        serializedObject.ApplyModifiedProperties();
    }
}
