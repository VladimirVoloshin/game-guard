namespace GameGuard.Domain.Common.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        private readonly CompositeSpecification<T> _left;
        private readonly CompositeSpecification<T> _right;

        public OrSpecification(CompositeSpecification<T> left, CompositeSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override async Task<bool> IsSatisfiedByAsync(T entity)
        {
            return await _left.IsSatisfiedByAsync(entity)
                || await _right.IsSatisfiedByAsync(entity);
        }
    }
}
