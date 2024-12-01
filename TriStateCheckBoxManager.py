import tkinter as tk


class TriStateCheckBox:
    def __init__(self, master, text, children=None):
        self.master = master
        self.text = text
        self.state = 0  # 0 - unchecked, 1 - checked, 2 - partially checked
        self.children = children if children else []

        # Головний CheckBox
        self.var = tk.IntVar(value=self.state)
        self.checkbox = tk.Checkbutton(
            master,
            text=text,
            variable=self.var,
            command=self.toggle_state,
            tristatevalue=-1
        )
        self.checkbox.pack()

    def toggle_state(self):
        """
        Змінює стан головного CheckBox і синхронізує дочірні CheckBox.
        """
        if self.var.get() == 1:  # Checked
            self.set_children_state(1)
        elif self.var.get() == 0:  # Unchecked
            self.set_children_state(0)

    def set_children_state(self, state):
        """
        Встановлює стан усіх дочірніх CheckBox.
        """
        for child in self.children:
            child.set_state(state)

    def update_state_from_children(self):
        """
        Оновлює стан головного CheckBox на основі станів дочірніх.
        """
        states = [child.state for child in self.children]
        if all(s == 1 for s in states):
            self.state = 1
            self.var.set(1)
        elif all(s == 0 for s in states):
            self.state = 0
            self.var.set(0)
        else:
            self.state = 2
            self.var.set(-1)

    def set_state(self, state):
        """
        Встановлює стан CheckBox.
        """
        self.state = state
        self.var.set(1 if state == 1 else 0)
        self.update_state_from_children()


class ChildCheckBox:
    def __init__(self, master, text, parent=None):
        self.master = master
        self.text = text
        self.state = 0  # 0 - unchecked, 1 - checked
        self.parent = parent

        # Дочірній CheckBox
        self.var = tk.IntVar(value=self.state)
        self.checkbox = tk.Checkbutton(
            master,
            text=text,
            variable=self.var,
            command=self.toggle_state
        )
        self.checkbox.pack()

    def toggle_state(self):
        """
        Змінює стан дочірнього CheckBox і оновлює батьківський CheckBox.
        """
        self.state = self.var.get()
        if self.parent:
            self.parent.update_state_from_children()

    def set_state(self, state):
        """
        Встановлює стан CheckBox.
        """
        self.state = state
        self.var.set(state)


if __name__ == "__main__":
    root = tk.Tk()
    root.title("TriState CheckBox Example")

    # Дочірні CheckBox
    child1 = ChildCheckBox(root, "Child 1")
    child2 = ChildCheckBox(root, "Child 2")
    child3 = ChildCheckBox(root, "Child 3")

    # Головний CheckBox
    parent = TriStateCheckBox(root, "Parent", children=[child1, child2, child3])

    # Прив'язка дочірніх CheckBox до батьківського
    for child in [child1, child2, child3]:
        child.parent = parent

    root.mainloop()

