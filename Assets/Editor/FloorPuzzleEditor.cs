
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/*
public class FloorPuzzleEditor : EditorWindow
{
    [SerializeField] public AnswerGraphic answerGraphic;
    [SerializeField] public MatrixWrapper answerKey = new();

    [MenuItem("Window/Floor Puzzle Editor")]
    public static void ShowWindow()
    {
        FloorPuzzleEditor window = GetWindow<FloorPuzzleEditor>();
        window.titleContent = new GUIContent("AnswerEditor");

    }
    public void CreateGUI()
    {
        //Retrieve UXML file
        VisualTreeAsset uxmlFile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/FloorPuzzleEditor.uxml");
        VisualElement main = uxmlFile.Instantiate();
        rootVisualElement.Add(main);

        if (answerGraphic == null)
        {
            answerGraphic = FindAnyObjectByType<AnswerGraphic>();
        }
        answerGraphic.RequestAnswerKey.AddListener(() => { SendAnswerKey(); });
        
        VisualElement[] box = new VisualElement[5];
        for (int i = 0; i < 5; i++)
        {
            box[i] = main.Q<VisualElement>("Row" + i);
            for (int j = 0; j < 5; j++)
            {
                MatrixToggle newTog = new()
                {
                    value = false,
                    row = i,
                    col = j
                };
                box[i].Add(newTog);
   
                // Mirror value of uxml field into the C# field.
                newTog.RegisterCallback<ChangeEvent<bool>>((evt) =>
                {
                    newTog.value = evt.newValue;
                    UpdateAnswerKey(newTog);
                });

            }
        }
        return;
    }
    public void SendAnswerKey()
    {
        bool[][] newKey = new bool[5][];
        for (int i = 0;i < 5;i++)
        {
            newKey[i] = new bool[5];
            for (int j = 0; i < 5; i++)
            {
                newKey[i][j] = answerKey.row[i].col[j];
            }
        }
        answerGraphic.SetAnswerKey(newKey);
    }
    private void UpdateAnswerKey(MatrixToggle change)
    {
        if (answerGraphic == null)
        {
            Debug.Log("Null answerGraphic, finding now");
            answerGraphic = FindAnyObjectByType<AnswerGraphic>();
        }
        answerKey.row[change.row].col[change.col] = change.value;
        answerGraphic.UpdateAnswer(change.row, change.col, change.value);
        answerKey.Serialize(true);
    }
}
[System.Serializable]
public class MatrixToggle : Toggle
{
    public int row;
    public int col;
}
[System.Serializable]
public class ArrayWrapper
{
    public bool[] col;
}
[System.Serializable]
public class MatrixWrapper
{
    public ArrayWrapper[] row = new ArrayWrapper[5];
}

*/
