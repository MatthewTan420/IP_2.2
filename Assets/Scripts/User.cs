public class User
{
    public string username;
    public int time;
    public int points;
    public string country;
    public int age;
    public bool admin;
    public int num;
    public User()
    {
    }
    public User(string username, int time, int points, string country, int age, bool admin, int num)
    {
        this.username = username;
        this.time = time;
        this.points = points;
        this.country = country;
        this.age = age;
        this.admin = admin;
        this.num = num;
    }
}