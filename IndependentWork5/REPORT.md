# Звіт до самостійної роботи №5

## 1. Поліморфізм через Object

У `List<object>` зберігаються об'єкти різних класів: `Product`, `Customer` і `Order`. Кожен клас перевизначає `ToString()`. Під час `Console.WriteLine(item)` викликається реалізація саме фактичного типу об'єкта. Це дозволяє однаково обробляти різні типи через `object`, але отримувати різну поведінку.

## 2. Поліморфізм через IEnumerable

`MyCustomCollection` реалізує `IEnumerable<string>`, тому її можна використовувати в `foreach` так само, як стандартний `List<string>` або масив. Клієнтському коду не потрібно знати, що всередині класу використовується `List<string>`.

Ключовий фрагмент:

```csharp
public IEnumerator<string> GetEnumerator()
{
    return _items.GetEnumerator();
}
```

## 3. Поліморфізм через Stream

Метод `ProcessStream(Stream stream)` приймає базовий абстрактний тип `Stream`. Тому один метод працює і з `FileStream`, і з `MemoryStream`. Конкретна реалізація читання визначається фактичним типом переданого об'єкта.

```csharp
public static void ProcessStream(Stream stream)
{
    byte[] buffer = new byte[8];
    int bytesRead = stream.Read(buffer, 0, buffer.Length);
}
```

## Висновок

Поліморфізм дозволяє писати спільний клієнтський код для різних реалізацій. Це зменшує дублювання, підвищує гнучкість і спрощує розширення програми новими типами.
