using System.Linq.Expressions;

namespace GameGuard.Domain.Common.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}
