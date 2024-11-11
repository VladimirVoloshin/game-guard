namespace GameGuard.Domain.Common.Specifications
{
    public class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override async Task<bool> IsSatisfiedByAsync(T entity)
        {
            return await _left.IsSatisfiedByAsync(entity)
                && await _right.IsSatisfiedByAsync(entity);
        }
    }
}
