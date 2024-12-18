using System.Collections;
using System.Collections.Generic;
using Chipmunk.GameJam;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(SpawnTableSO))]
public class SpawnTableSOEditor : Editor
{
    [SerializeField] VisualTreeAsset visualTreeAsset;
    private SpawnTableSO spawnTable;
    public override VisualElement CreateInspectorGUI()
    {
        spawnTable = (SpawnTableSO)target;

        VisualElement root = new VisualElement();
        visualTreeAsset.CloneTree(root);

        PieChartView pieChartView = new PieChartView(spawnTable.spawnables.ToArray());
        root.Add(pieChartView);
        
        InspectorView<SpawnTableSO> inspectorView = new InspectorView<SpawnTableSO>();
        inspectorView.UpdateInspactor(spawnTable);
        root.Add(inspectorView);

        return root;
    }
}
