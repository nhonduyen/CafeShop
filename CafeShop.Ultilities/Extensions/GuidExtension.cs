namespace CafeShop.Ultilities.Extensions {
    public static class GuidExtension {

        public static Guid NewIfEmpty(this Guid id) {
            return id == Guid.Empty ? Guid.NewGuid() : id;
        }
    }
}