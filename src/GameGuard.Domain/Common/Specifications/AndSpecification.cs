namespace GameGuard.Domain.Common.Specifications
{
    /// <summary>
    /// Combines two specifications using a logical AND operation.
    /// This specification is satisfied only if both the left and right specifications are satisfied.
    /// </summary>
    /// <typeparam name="T">The type of entity being specified.</typeparam>
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
