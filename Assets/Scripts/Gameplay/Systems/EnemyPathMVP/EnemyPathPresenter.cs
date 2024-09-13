using System.Linq;
using Gameplay.Enemies;
using Gameplay.Grid;
using UnityEngine;
using Zenject;

namespace Gameplay.Systems.EnemyPathMVP
{
    public class EnemyPathPresenter
    {
        [Inject] private readonly GridManager gridManager;
        [Inject] private readonly EnemyManager enemyManager;
    
        private readonly EnemyPathView view;
        private readonly EnemyPathModel model;

        public EnemyPathPresenter(EnemyPathView view, EnemyPathModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void Activate()
        {
            gridManager.ElementAdded += UpdatePath;
            gridManager.ElementRemoved += UpdatePath;
            UpdatePath();
        }

        private void UpdatePath()
        {
            var path = gridManager.GetPath(enemyManager.StartPos, enemyManager.EndPos).Select(position => position + Vector3.up * .3f);
            view.DisplayPath(path.ToArray());
        }

        public void Deactivate()
        {
            gridManager.ElementAdded -= UpdatePath;
            gridManager.ElementRemoved -= UpdatePath;
            view.ClearPath();
        }
    }
}