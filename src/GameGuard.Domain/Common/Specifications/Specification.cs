using System.Linq.Expressions;

namespace GameGuard.Domain.Common.Specifications
{
    public abstract class Specification<T>
    {
        public abstract Task<bool> IsSatisfiedByAsync(T entity);

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }
    }
}
