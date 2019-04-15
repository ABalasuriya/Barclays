namespace ThirdPartyTools
{
    public class UserDetail
    {
        public virtual string UserName { get; set; }
        public virtual UserAddress UserAddress { get; set; }

        public virtual string UserRole { get; set; }
    }
}