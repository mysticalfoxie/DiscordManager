using Discord;

namespace DCM.Core.Extensions;

public static class DeleteableExtensions
{
    public static async Task<int> BulkDelete(this IEnumerable<IDeletable> deletables)
    {
        var tasks = deletables
            .AsParallel()
            .Select(x => x.DeleteSafe())
            .ToArray();
        await Task.WhenAll(tasks);
        return tasks.Length;
    }

    public static async Task<int> BulkDelete(this IAsyncEnumerable<IDeletable> deleteables)
    {
        var tasks = new List<Task>();
        await foreach (var element in deleteables.ConfigureAwait(false))
            tasks.Add(element.DeleteSafe());
        await Task.WhenAll(tasks);
        return tasks.Count;
    }

    public static void DeleteAfter(this IDeletable deleteable, int delay)
    {
        Task.Factory.StartNew(async () =>
        {
            await Task.Delay(delay);
            await deleteable.DeleteAsync();
        });
    }

    public static async Task<bool> DeleteSafe(this IDeletable deleteable)
    {
        try
        {
            await deleteable.DeleteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}