namespace Game.Ecs.Gameplay.Tutorial.Actions.AnalyticsAction.Data
{
    public interface ITutorialAnalytics
    {
        void Send(TutorialMessage message);
    }
}