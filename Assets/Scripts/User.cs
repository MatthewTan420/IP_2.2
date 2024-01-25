public class User
{
    public string username;
    public int distance;
    public int rubbish;
    public string gender;
    public string occupation;
    public int age;
    public bool companion;
    public int img;
    public User()
    {
    }
    public User(string username, int distance, int rubbish, string gender, string occupation, int age, bool companion, int img)
    {
        this.username = username;
        this.distance = distance;
        this.rubbish = rubbish;
        this.gender = gender;
        this.occupation = occupation;
        this.age = age;
        this.companion = companion;
        this.img = img;
    }
}