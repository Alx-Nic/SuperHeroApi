namespace HeroMSVC.Api
{
    public class FooService
    {
        public int myNumber = 0;
        public FooService()
        {
            myNumber = new Random().Next(0, 10);
        }

    }
}
