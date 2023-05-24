
public abstract class KillablePresenter : Presenter
{
    public new Creature Model => base.Model as Creature;
}
