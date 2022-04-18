namespace WebApiMediatR
{
    internal class User:BaseEntity
    {
        public int Id { get; init; }
        public DateTime CreateTime { get; init; }
        public string  UserName { get;private set; }
        public int Credit { get; private set; }
        private string? passwordHash;
        public string? remark;
        public string? Remark
        {
            get { return remark; }
        }
        public string? Tag { get; set; }
        private User()//给EFCore从数据库读出数据用的
        {
        }
        public User(string yhm)//我们自己用
        {
            UserName = yhm;
            CreateTime = DateTime.Now;
            Credit = 10;
            AddDomainEvent(new NewUserNotification(yhm,CreateTime));
        }
        public void ChangeUserName(string un)
        {
            if (un.Length > 5||un.Length<1)
            {
                Console.WriteLine("用户长度不能大于5");
                return;
            }
            AddDomainEvent(new UserNameChangeNotification(UserName,un));
            UserName = un;
        }
        public void ChangePassword(string pwd)
        {
            if (pwd.Length != 0)
            {
                passwordHash= "HASH"+pwd;
            }
        }

        
    }
}
