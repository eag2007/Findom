"""
Открывает html файл

Args:
    name (str): путь до файла

Returns:
    str: строка текста
"""
def open_html(name: str) -> str:
    with open(name) as f:
        return f.read()
