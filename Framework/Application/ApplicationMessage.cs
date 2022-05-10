namespace Framework.Application
{
    public class ApplicationMessage
    {
        public const string DuplicatedRecord = "امکان ثبت رکورد تکراری وجود ندارد.";
        public const string RecordNotFound = "رکورد موردنظر یافت نشد";
        public const string PasswordNotMatch = "پسورد وارد شده و تکرار آن با هم مطابقت ندارند.";
        public const string WrongUsernamePassword = "نام کاربری یا رمزعبور اشتباه می‌باشد.";
        public const string PaymentSucceeded = "پرداخت با موفقیت انجام شد.";
        public const string PaymentFailed = "پرداخت با موفقیت انجام نشد. درصورت کسر از حساب، پس از ۷۲ ساعت مبلغ به حساب شما باز خواهد گشت.";
    }
}