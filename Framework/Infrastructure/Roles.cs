namespace Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string ContentUploader = "2";
        public const string User = "3";
        public const string Colleague = "1002";

        public static string GetRoleBy(long id)
        {
            return id switch
            {
                1 => "مدیر سیستم",
                2 => "محتوا گذار",
                3 => "کاربر سیستم",
                1002 => "همکار",
                _ => ""
            };
        }
    }
}
