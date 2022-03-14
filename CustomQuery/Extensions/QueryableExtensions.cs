using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CustomQuery.Data;
using CustomQuery.Helpers;
using CustomQuery.Services;

namespace CustomQuery.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> FilterByBst<T>(
        this IQueryable<T> query, 
        Expression<Func<T, int>> keySelector,
        ISessionService sessionService, 
        Context context) 
    {
        var userId = sessionService.GetUserId();
        var allowedBsts = context.UserBsts.Where(x => x.UserId == userId).Select(x => x.BstId);
        
        Expression<Func<int, bool>> filterByCompanyId = id => allowedBsts.Contains(id);

        Expression<Func<T, bool>> composed = filterByCompanyId.Compose(keySelector);
        return query.Where(composed);
    }
}