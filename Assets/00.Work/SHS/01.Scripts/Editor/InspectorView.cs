using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Chipmunk.GameJam
{
    public class InspectorView<T> : VisualElement where T : UnityEngine.Object
    {
        public static InspectorView<T> Instace;
        public InspectorView()
        {
            Instace = this;
        }
        public new class UxmlFactory : UxmlFactory<InspectorView<T>, VisualElement.UxmlTraits> { }
        Editor editor;
        public Action onDataChange;
        public void UpdateInspactor(T drawTarget)
        {
            this.Clear();
            UnityEngine.Object.DestroyImmediate(editor);

            if (drawTarget == null) return;

            editor = Editor.CreateEditor(drawTarget);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (editor.target != null)
                    editor.OnInspectorGUI();
            });
            BindingExtensions.TrackSerializedObjectValue(container, editor.serializedObject, serialzedObject => onDataChange?.Invoke());
            Add(container);
        }
    }

}