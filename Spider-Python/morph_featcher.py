from collections import Counter
import pymorphy3
import string

"""
Делит список слов по количеству и морфемам

Args:
    words (list[str]): Список слов
    
Returns:
    dict[str, int]: Словарь где ключ начальная форма слова, а значение количество раз сколько,
        оно встречается в списке
"""
def morph_words(words: list[str]) -> dict:
    morph = pymorphy3.MorphAnalyzer()
    normal_forms = []

    punctuation = string.punctuation + '«»—–'

    for word in words:
        clean_word = word.strip(punctuation).lower()

        if not clean_word or clean_word.isdigit():
            continue

        parsed = morph.parse(clean_word)
        if parsed:
            normal_forms.append(parsed[0].normal_form)
        else:
            normal_forms.append(clean_word)

    return dict(Counter(normal_forms))