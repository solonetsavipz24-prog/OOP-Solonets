# Самостійна робота №8 — звіт

## 1. Приклади з open-source (.NET Runtime)

### Dictionary
Файл: `System.Private.Xml/src/System/Xml/Schema/SchemaInfo.cs`  
https://github.com/dotnet/runtime/blob/ad029a35a82f87a2b5ef4472abf9e5cefcae9f11/src/libraries/System.Private.Xml/src/System/Xml/Schema/SchemaInfo.cs

У `SchemaInfo` використовуються словники на кшталт `Dictionary<XmlQualifiedName, SchemaElementDecl>`. Ключ дає прямий доступ до декларації елемента. Для такого сценарію `Dictionary` доречніший за `List`, бо середній пошук за ключем має складність O(1), а в списку довелося б виконувати O(n) пошук.

### HashSet
Файл: `System.Private.CoreLib/src/System/SearchValues/Strings/StringSearchValuesBase.cs`  
https://github.com/dotnet/runtime/blob/ad029a35a82f87a2b5ef4472abf9e5cefcae9f11/src/libraries/System.Private.CoreLib/src/System/SearchValues/Strings/StringSearchValuesBase.cs

Клас містить `HashSet<string>? _uniqueValues`. Множина зручна, коли важлива унікальність значень і швидка перевірка наявності. Середня складність `Contains` — O(1).

### Dictionary + HashSet разом
Файл: `tools/illink/src/linker/Linker/WarningSuppressionWriter.cs`  
https://github.com/dotnet/runtime/blob/ad029a35a82f87a2b5ef4472abf9e5cefcae9f11/src/tools/illink/src/linker/Linker/WarningSuppressionWriter.cs

Там використовується `Dictionary<AssemblyNameDefinition, HashSet<(int Code, IMemberDefinition Member)>>`. Словник групує дані за збіркою, а `HashSet` не допускає дублікатів попереджень усередині групи. Це хороший приклад структури "ключ -> множина унікальних значень".

## 2. Порівняльна таблиця

| Операція | List<T> | Dictionary<TKey,TValue> | HashSet<T> | Коли використовувати |
|---|---:|---:|---:|---|
| Додавання | O(1)* | O(1)* | O(1)* | Залежить від структури даних |
| Пошук за значенням | O(n) | O(n) | O(1)* | HashSet для швидкої перевірки наявності |
| Пошук за ключем | N/A | O(1)* | N/A | Dictionary для доступу за ключем |
| Видалення | O(n) | O(1)* | O(1)* | Dictionary/HashSet для швидкого видалення |
| Перевірка наявності | O(n) | O(1)* за ключем | O(1)* | Dictionary або HashSet |

`*` — середня складність за нормальної роботи хеш-таблиці.

## 3. Висновки

`Dictionary` кращий за `List`, коли об'єкт потрібно швидко знаходити за унікальним ключем. `HashSet` оптимальний, коли важлива унікальність і швидкі операції множин: `UnionWith`, `IntersectWith`, `ExceptWith`. Основні підводні камені — правильні `Equals/GetHashCode`, вибір comparer та відсутність гарантованого порядку елементів.

У практичній частині реалізовано `FileMetadataCache`, `TagManager` та `SearchIndex`.
