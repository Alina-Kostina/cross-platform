
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public class TriStateCheckBoxManager
{
    private CheckBox mainCheckBox;
    private List<CheckBox> dependentCheckBoxes;

    public TriStateCheckBoxManager(CheckBox mainCheckBox, List<CheckBox> dependentCheckBoxes)
    {
        this.mainCheckBox = mainCheckBox;
        this.dependentCheckBoxes = dependentCheckBoxes;

        // Підписка на події
        mainCheckBox.CheckedChanged += MainCheckBox_CheckedChanged;
        foreach (var checkBox in dependentCheckBoxes)
        {
            checkBox.CheckedChanged += DependentCheckBox_CheckedChanged;
        }

        UpdateMainCheckBoxState(); // Ініціалізація стану головного CheckBox
    }

    private void MainCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (mainCheckBox.CheckState == CheckState.Indeterminate)
            return;

        bool isChecked = mainCheckBox.Checked;

        foreach (var checkBox in dependentCheckBoxes)
        {
            checkBox.CheckedChanged -= DependentCheckBox_CheckedChanged;
            checkBox.Checked = isChecked;
            checkBox.CheckedChanged += DependentCheckBox_CheckedChanged;
        }
    }

    private void DependentCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateMainCheckBoxState();
    }

    private void UpdateMainCheckBoxState()
    {
        bool allChecked = dependentCheckBoxes.All(cb => cb.Checked);
        bool noneChecked = dependentCheckBoxes.All(cb => !cb.Checked);

        mainCheckBox.CheckedChanged -= MainCheckBox_CheckedChanged;

        if (allChecked)
        {
            mainCheckBox.CheckState = CheckState.Checked;
        }
        else if (noneChecked)
        {
            mainCheckBox.CheckState = CheckState.Unchecked;
        }
        else
        {
            mainCheckBox.CheckState = CheckState.Indeterminate;
        }

        mainCheckBox.CheckedChanged += MainCheckBox_CheckedChanged;
    }
}

// Приклад використання
public class MainForm : Form
{
    public MainForm()
    {
        CheckBox mainCheckBox = new CheckBox { Text = "Головний CheckBox", AutoCheck = true, Top = 10, Left = 10 };
        CheckBox checkBox1 = new CheckBox { Text = "Чекбокс 1", Top = 40, Left = 10 };
        CheckBox checkBox2 = new CheckBox { Text = "Чекбокс 2", Top = 70, Left = 10 };
        CheckBox checkBox3 = new CheckBox { Text = "Чекбокс 3", Top = 100, Left = 10 };

        this.Controls.Add(mainCheckBox);
        this.Controls.Add(checkBox1);
        this.Controls.Add(checkBox2);
        this.Controls.Add(checkBox3);

        List<CheckBox> dependentCheckBoxes = new List<CheckBox> { checkBox1, checkBox2, checkBox3 };

        // Ініціалізація менеджера
        new TriStateCheckBoxManager(mainCheckBox, dependentCheckBoxes);
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
