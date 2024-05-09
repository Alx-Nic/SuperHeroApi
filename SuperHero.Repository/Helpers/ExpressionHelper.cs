
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using HeroMSVC.Models.PaginationFilters;

namespace HeroMSVC.Repo.Helpers
{
    public static class ExpressionHelper
    {
        internal static Expression<Func<T, bool>> GetFilterExpression<T>(string filter, string keyToFilterOn)
        {

            //SuperHero Type
            //filter= name=eq:Batman
            //attributeToFilterOn=Name
            //hero => hero.Name == "Batman"

            string[] filterArr = filter.Split(":");

            string filterOp = filterArr[0];
            string filterValue = filterArr[1];

            PropertyInfo property = typeof(T).GetProperty(keyToFilterOn);
            if (property == null)
            {
                throw new ArgumentException($"Attribute '{keyToFilterOn}' does not exist on type '{typeof(T).Name}'.");
            }

            Expression<Func<T, bool>> predicate;

            //SuperHeroType
            ParameterExpression parameter = Expression.Parameter(typeof(T), "entity");
            //The property in the type ex Name
            MemberExpression member = Expression.Property(parameter, property);
            //The constant is the actual value of the filter converted to the type of the property, for ex if prop is int it will be int.Parse(filterValue)
            ConstantExpression constant = Expression.Constant(Convert.ChangeType(filterValue, property.PropertyType));

            switch (filterOp)
            {
                case FilterOperators.equal:
                    predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
                    break;
                case FilterOperators.notEqual:
                    predicate = Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
                    break;
                case FilterOperators.greaterThan:
                    predicate = Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
                    break;
                case FilterOperators.lessThan:
                    predicate = Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
                    break;
                default:
                    throw new ArgumentException($"Unsupported filter operator: {filterOp}");
            }

            return predicate;
        }

    }

}
