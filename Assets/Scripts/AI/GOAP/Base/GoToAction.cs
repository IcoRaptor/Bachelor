using UnityEngine;

namespace AI.GOAP
{
    public abstract class GoToAction : BaseAction
    {
        #region Variables

        private const float _MAX_DIST = 0.005f;

        protected Transform _target;

        #endregion

        public override void Activate(AIModule module)
        {
            base.Activate(module);

            module.Board.NextNavigationPoint = _target;
            module.Board.ChangeDestination = true;
        }

        public override void Update(AIModule module)
        {
            if (!_target)
                return;

            var pos = module.gameObject.transform.position;
            var dist = (pos - _target.position).sqrMagnitude;

            if (dist < _MAX_DIST)
                _complete = true;
        }

        public override void Deactivate(AIModule module)
        {
            module.Board.NextNavigationPoint = null;
            module.Board.ChangeDestination = true;

            base.Deactivate(module);
        }
    }
}