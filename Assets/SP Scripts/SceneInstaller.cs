using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    [SerializeField] GameManager m_gameManager;
    [SerializeField] GridManager m_gridManager;
    [SerializeField] BoundsManager m_boundsManager;
    [SerializeField] TurnManager m_turnManager;
    [SerializeField] CommandManager m_commandManager;
    [SerializeField] ScoreManager m_scoreManager;


    public override void InstallBindings()
    {
        SmartBindTo<IGameManager>(m_gameManager);
        SmartBindTo<IGameController>(m_gameManager);
        SmartBindTo<IGridManager>(m_gridManager);
        SmartBindTo<IBoundsManager>(m_boundsManager);
        SmartBindTo<ITurnManager>(m_turnManager);
        SmartBindTo<ICommandManager>(m_commandManager);
        SmartBindTo<IScoreManager>(m_scoreManager);
    }

    private void SmartBindTo<T>(Component component) where T : class
    {
        if (component == null) return;
        if (!(component is T)) throw new System.Exception ("Component is not type " + typeof(T));

        if (component.gameObject.scene.IsValid ()) {
            Container.Bind<T>().FromInstance(component as T).AsSingle();
        } else {

            Container.Bind<IGridManager>().FromComponentInNewPrefab(component).AsSingle();
        }
    }
}