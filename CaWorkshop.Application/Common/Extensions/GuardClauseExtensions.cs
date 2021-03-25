using Ardalis.GuardClauses;
using CaWorkshop.Application.Common.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class GuardClauseExtensions
    {
        public static void NotFound<T>(this IGuardClause guardClause, T entity, object key)
        {
            if (entity is null)
                throw new NotFoundException(typeof(T).Name, key);
        }
    }
}
