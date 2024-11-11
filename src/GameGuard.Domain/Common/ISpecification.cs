using System.Linq.Expressions;

namespace GameGuard.Domain.Common
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}
