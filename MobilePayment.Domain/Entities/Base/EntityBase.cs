namespace MobilePayment.Domain.Entities.Base
{
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        public virtual TId Id { get; }
        private int? _requestedHashCode;

        public bool IsTransient()
        {
            return Id.Equals(default(TId));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EntityBase<TId> item)) return false;

            if (ReferenceEquals(this, item)) return true;

            if (GetType() != item.GetType()) return false;

            if (item.IsTransient() || IsTransient()) return false;
            return item == this;
        }

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            _requestedHashCode ??= Id.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }

        public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
        {
            return !(left == right);
        }
    }
}