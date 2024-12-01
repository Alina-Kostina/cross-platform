import tkinter as tk


class CustomButton:
    def __init__(self, master, text, width=100, height=50, bg="lightgrey", fg="black", command=None):
        """
        Ініціалізація користувацької кнопки.
        """
        self.master = master
        self.text = text
        self.width = width
        self.height = height
        self.bg = bg
        self.fg = fg
        self.command = command
        self.is_hovered = False

        # Canvas як основа для кнопки
        self.canvas = tk.Canvas(master, width=width, height=height, bg=bg, highlightthickness=0)
        self.canvas.bind("<Button-1>", self._on_click)
        self.canvas.bind("<Enter>", self._on_hover)
        self.canvas.bind("<Leave>", self._on_leave)

        # Відмальовка початкового стану
        self._draw_button()

    def _draw_button(self):
        """
        Відмальовка кнопки.
        """
        self.canvas.delete("all")  # Очистка перед оновленням
        self.canvas.create_rectangle(0, 0, self.width, self.height, fill=self.bg, outline="darkgrey")
        self.canvas.create_text(self.width // 2, self.height // 2, text=self.text, fill=self.fg, font=("Arial", 12))

    def _on_click(self, event):
        """
        Обробник натискання на кнопку.
        """
        if self.command:
            self.command()

    def _on_hover(self, event):
        """
        Обробник наведення курсору.
        """
        self.is_hovered = True
        self.bg = "lightblue"  # Зміна кольору при наведенні
        self._draw_button()

    def _on_leave(self, event):
        """
        Обробник покидання курсору.
        """
        self.is_hovered = False
        self.bg = "lightgrey"  # Повернення до початкового кольору
        self._draw_button()

    def place(self, x, y):
        """
        Розташування кнопки на батьківському вікні.
        """
        self.canvas.place(x=x, y=y)


# Приклад використання
if __name__ == "__main__":
    def on_button_click():
        print("Button clicked!")

    root = tk.Tk()
    root.title("Custom Button Example")
    root.geometry("300x200")

    button1 = CustomButton(root, text="Click Me", command=on_button_click)
    button1.place(x=100, y=75)

    root.mainloop()
