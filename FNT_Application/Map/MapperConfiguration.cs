namespace FNT_Application.Map
{
    public static class MapperConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<Menu, MenuDTO>.NewConfig();
        }
    }
}
