using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameObject ColorManager;
        [SerializeField] private GameObject GameController;
        [SerializeField] private GameObject Field;
        [SerializeField] private GameObject CellAnimationController;
        
        [SerializeField] private GameObject cellPref;
        [SerializeField] private GameObject cellAnimationPref;
        public override void InstallBindings()
        {
            Container.Bind<ColorManager>().FromComponentOn(ColorManager).AsSingle();
            Container.Bind<GameController>().FromComponentOn(GameController).AsSingle();
            Container.Bind<Field>().FromComponentOn(Field).AsSingle();
            Container.Bind<CellAnimationController>().FromComponentOn(CellAnimationController).AsSingle();
            
            Container.BindFactory<Cell, CellFactory>().FromComponentInNewPrefab(cellPref);
            Container.BindFactory<CellAnimation, CellAnimationFactory>().FromComponentInNewPrefab(cellAnimationPref);
        }
    }
}

    