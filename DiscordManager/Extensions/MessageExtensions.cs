using Discord;
using System.Threading.Tasks;

namespace DCM.Extensions
{
    public static class MessageExtensions
    {
        /// <summary>
        /// Deletes a Discord Message after a period of time.
        /// </summary>
        /// <param name="delay">The delay in Milliseconds</param>
        /// <param name="ignoreError">When true all exceptions from the deletion will be caught and ignored.</param>
        public static async Task DeleteAsync(this IDeletable deleteable, int delay, bool ignoreError = false)
        {
            await Task.Delay(delay);

            try
            {
                await deleteable.DeleteAsync();
            }
            catch
            {
                if (!ignoreError)
                    throw;
            }
        }

        public static void DeleteAfter(this IDeletable deleteable, int delay)
        {
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(delay);

                try
                {
                    await deleteable.DeleteAsync();
                }
                catch
                {

                }
            });
        }
    }
}
