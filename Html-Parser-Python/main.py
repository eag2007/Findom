from pprint import pprint
from open_html import open_html
from parse_html import parse_html
from morph_featcher import morph_words

"""Файл для запуска/теста парсера"""
pprint(morph_words(parse_html(open_html("../html3.html"))["words"]))
