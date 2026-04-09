using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private LevelRowUI[] levelRows;
    [SerializeField] private LevelDetailPanel detailPanel;

    private int selectedIndex = 0;

    public void OnLevelRowClicked(int index)
    {
        // Deselect previous
        levelRows[selectedIndex].SetSelected(false);
        selectedIndex = index;
        levelRows[selectedIndex].SetSelected(true);

        // Update right panel
        detailPanel.Display(LevelData.levels[index]);
    }
}
