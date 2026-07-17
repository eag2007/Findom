    SYMBOLS = "qwertyuiopasdfghjklzxcvbnm1234567890йцукенгшщзхъфывапролджэячсмитьбюё"

"""
Вытаскивает из тега его название

Args:
    tag_str (str): строка тега
    
Result:
    str: название тега
"""
def get_tag_name(tag_str: str) -> str:
    if tag_str.startswith("</"):
        tag_str = tag_str[2:]
    elif tag_str.startswith("<"):
        tag_str = tag_str[1:]
    else:
        return tag_str.lower()

    end = 0
    while end < len(tag_str) and tag_str[end] not in " >\t\n/":
        end += 1
    return tag_str[:end].lower()


"""
Проверяет есть ли тэг на вершине стека тэгов

Args:
    closing_tag_name (str): закрытый тег
    tags (list[str]):       стек тэгов

Result:
    bool: результат проверки
"""
def check_in_tags(closing_tag_name: str, tags: list) -> bool:
    if not tags:
        return False
    return get_tag_name(tags[-1]) == closing_tag_name.lower()


"""
Парсит html страницу, смотри на атрибуты некоторых тегов

Args:
    html_page (str): Текст html страницы

Result:
    dict[str, list[str]]: Словарь {"words": [], "links": []}
"""
def parse_html(html_page: str) -> dict[str, list[str]]:
    tags = []
    result = {
        "words": [],
        "links": []
    }
    word = ""
    index = 0

    while index < len(html_page):
        if html_page[index] == "<":
            if html_page[index:index + 7] == "<script":
                end = html_page.find("</script>", index + 1)
                if end == -1:
                    break
                index = end + 9

            elif html_page[index:index + 6] == "<style":
                end = html_page.find("</style>", index + 1)
                if end == -1:
                    break
                index = end + 8

            elif html_page[index:index + 4] == "<!--":
                end = html_page.find("-->", index + 4)
                if end == -1:
                    break
                index = end + 3
                continue

            elif html_page[index:index + 9].lower() == "<!doctype":
                end = html_page.find(">", index + 1)
                if end == -1:
                    break
                index = end + 1

            elif html_page[index:index + 5].lower() == "<meta":
                end = html_page.find(">", index + 1)
                if end == -1:
                    break
                index = end + 1

            elif html_page[index:index + 5].lower() == "<link":
                index += 5
                while index < len(html_page) and html_page[index] != ">":
                    if html_page[index:index + 5] == "title":
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                            result["words"] += html_page[left + 1:right].lower().split()
                        else:
                            index += 1
                    elif html_page[index:index + 4] == "href":
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                            result["links"] += html_page[left + 1:right].lower().split()
                        else:
                            index += 1
                    else:
                        index += 1
                index += 1

            elif html_page[index:index + 2].lower() == "<a" and html_page[index + 2] in " >\t\n/>":
                index += 2
                while index < len(html_page) and html_page[index] != ">":
                    if html_page[index:index + 4] == "href":
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                            result["links"] += html_page[left + 1:right].lower().split()
                        else:
                            index += 1
                    else:
                        index += 1
                tags.append("<a>")
                index += 1

            elif html_page[index:index + 4].lower() == "<img":
                index += 4
                while index < len(html_page) and html_page[index] != ">":
                    if html_page[index:index + 3] == "alt":
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                            result["words"] += html_page[left + 1:right].lower().split()
                        else:
                            index += 1
                    elif html_page[index:index + 3] == "src":
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                            result["links"] += html_page[left + 1:right].lower().split()
                        else:
                            index += 1
                    else:
                        index += 1
                index += 1

            elif html_page[index:index + 6].lower() == "<input":
                index += 6
                while index < len(html_page) and html_page[index] != ">":
                    if html_page[index:index + 11] == "placeholder":
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                            result["words"] += html_page[left + 1:right].lower().split()
                        else:
                            index += 1
                    elif html_page[index:index + 10] == "aria-label":
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                            result["words"] += html_page[left + 1:right].lower().split()
                        else:
                            index += 1
                    elif html_page[index:index + 5] == "title":
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                            result["words"] += html_page[left + 1:right].lower().split()
                        else:
                            index += 1
                    else:
                        index += 1
                index += 1

            elif html_page[index:index + 4] == "</a>":
                if check_in_tags("a", tags):
                    tags.pop()
                index += 4
            elif html_page[index:index + 4].lower() == "<div":
                index += 4
                while index < len(html_page) and html_page[index] != ">":
                    if html_page[index:index + 5] == "title" and (index == 0 or html_page[index - 1] in " \t\n"):
                        left = html_page.find('"', index + 1)
                        right = html_page.find('"', left + 1)
                        result["words"] += html_page[left + 1:right].lower().split()
                        index = right + 1
                    elif html_page[index:index + 7] == "data-mw":
                        left = html_page.find("'", index + 1)
                        right = html_page.find("'", left + 1)
                        if left != -1 and right != -1:
                            index = right + 1
                        else:
                            index += 1
                    else:
                        index += 1
                tags.append("<div>")
                index += 1

            elif html_page[index:index + 6] == "</div>":
                if check_in_tags("div", tags):
                    tags.pop()
                index += 6

            elif html_page[index:index + 2] == "</":
                end = html_page.find(">", index + 1)
                if end == -1:
                    break
                closing_tag_str = html_page[index:end + 1]
                tag_name = get_tag_name(closing_tag_str)
                if check_in_tags(tag_name, tags):
                    tags.pop()
                else:
                    pass
                index = end + 1
            else:
                end = html_page.find(">", index + 1)
                if end == -1:
                    break
                tag_str = html_page[index:end + 1]
                VOID_TAGS = (
                    "<br", "<hr", "<!", "<wbr", "<area", "<base", "<col",
                    "<embed", "<param", "<source", "<track",
                    "<frame", "<isindex", "<keygen",
                    "<img", "<input", "<meta", "<link", "<nav", "<header"
                )

                flag = True
                for v in VOID_TAGS:
                    if v in tag_str.lower():
                        flag = False
                        break

                if flag:
                    tag_name = get_tag_name(tag_str)
                    tags.append(f"<{tag_name}>")

                index = end + 1

        else:
            if html_page[index].lower() in SYMBOLS:
                word += html_page[index].lower()
            else:
                if word:
                    result["words"].append(word)
                    word = ""
            index += 1
    return result