namespace Factory06
{
    public class MyBot : Bot
    {
        public override void Start(Game game)
        {
            game.Build(MachineType.Hat);
            game.Build(MachineType.Coat);
            game.Build(MachineType.Flask);
        }

        // TODO
        public override void Update(Game game)
        {
            
            
        }

        public override void End(Game game)
        {
        }

    }
}
