namespace Crub
{
    public class Dog
    {
        public string Name { get; set; } = "Bilbo";
        public int Age { get; set; }

        public Dog(int age)
        {
            Age = age;
        }
    }
}