namespace DCM.Core.Utils;

public static class TaskHelper
{
    public static async Task LoadTo<T>(Func<Task<T[]>> task, ICollection<T> collection)
    {
        var results = await task();
        foreach (var result in results)
            collection.Add(result);
    }
}