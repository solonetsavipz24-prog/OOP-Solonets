namespace Lab12v2;

public class InMemoryRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> items = new();

    public void Add(T item)
    {
        if (items.Any(x => x.Id == item.Id))
            throw new InvalidOperationException($"Елемент з ID {item.Id} вже існує.");

        items.Add(item);
    }

    public T GetById(int id)
    {
        foreach (T item in items)
        {
            if (item.Id == id)
                return item;
        }

        throw new KeyNotFoundException($"Елемент з ID {id} не знайдено.");
    }

    public IEnumerable<T> GetAll()
    {
        return items.ToList();
    }

    public void Update(T item)
    {
        int index = items.FindIndex(x => x.Id == item.Id);

        if (index == -1)
            throw new KeyNotFoundException($"Елемент з ID {item.Id} не знайдено.");

        items[index] = item;
    }

    public void Delete(int id)
    {
        int index = items.FindIndex(x => x.Id == id);

        if (index == -1)
            throw new KeyNotFoundException($"Елемент з ID {id} не знайдено.");

        items.RemoveAt(index);
    }
}
