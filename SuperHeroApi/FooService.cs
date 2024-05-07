namespace SuperHeroApi
{
    public class FooService
    {
        public int myNumber = 0;
        public FooService()
        {
            this.myNumber = new Random().Next(0, 10);
        }

    }
}
