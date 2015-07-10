namespace NinjaHive.Core
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
