using SuperHeroApi.Models.PaginationFilters;
using SuperHeroApi.Models.SuperHero;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;

namespace SuperHeroApi.Helpers
{
    public static class ExpressionHelper
    {
        internal static Expression<Func<T, bool>> GetFilterExpression<T>(string filter, string attributeToFilterOn)
        {




            string[] filterArr = filter.Split(":");

            string filterOp = filterArr[0];
            string filterValue = filterArr[1];

            PropertyInfo property = typeof(T).GetProperty(attributeToFilterOn);
            if (property == null)
            {
                throw new ArgumentException($"Attribute '{attributeToFilterOn}' does not exist on type '{typeof(T).Name}'.");
            }

            ParameterExpression parameter = Expression.Parameter(typeof(T), "entity");
            MemberExpression member = Expression.Property(parameter, property);
            ConstantExpression constant = Expression.Constant(Convert.ChangeType(filterValue, property.PropertyType));

            Expression<Func<T, bool>> predicate;

            switch (filterOp)
            {
                case FilterOperators.eq:
                    predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
                    break;
                case FilterOperators.ne:
                    predicate = Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
                    break;
                default:
                    throw new ArgumentException($"Unsupported filter operator: {filterOp}");
            }

            return predicate;
        }

    }

}
