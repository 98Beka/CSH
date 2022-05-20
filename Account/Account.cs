namespace ServerCsharpPool.Account {
    public abstract class Account {
        public int Id { get; set; }
        public string? Nickname { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }
}
