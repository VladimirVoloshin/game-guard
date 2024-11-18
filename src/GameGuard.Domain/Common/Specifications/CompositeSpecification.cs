namespace GameGuard.Domain.Common.Specifications
{
    public abstract class CompositeSpecification<T>
    {
        public abstract Task<bool> IsSatisfiedByAsync(T entity);

        public static CompositeSpecification<T> operator &(
            CompositeSpecification<T> left,
            CompositeSpecification<T> right
        )
        {
            return new AndSpecification<T>(left, right);
        }

        public static CompositeSpecification<T> operator |(
            CompositeSpecification<T> left,
            CompositeSpecification<T> right
        )
        {
            return new OrSpecification<T>(left, right);
        }
    }
}
