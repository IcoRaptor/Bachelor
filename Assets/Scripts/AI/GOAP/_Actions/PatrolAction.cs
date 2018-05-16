namespace AI.GOAP
{
    public class PatrolAction : GoToAction
    {
        #region Variables

        private int _currentPoint = 0;
        private int _maxPatrolPoints;

        #endregion

        public override bool CheckContext()
        {
            return true;
        }

        public override void Activate(AIModule module)
        {
            _maxPatrolPoints = module.Memory.PatrolPoints.Length;

            if (_maxPatrolPoints > 0)
                _target = module.Memory.PatrolPoints[_currentPoint++];

            base.Activate(module);
        }

        public override void Update(AIModule module)
        {
            if (!_target)
                return;

            var pos = module.gameObject.transform.position;
            var dist = (pos - _target.position).sqrMagnitude;

            if (dist > _MAX_DIST)
                return;

            if (_currentPoint >= _maxPatrolPoints)
            {
                _complete = true;
                return;
            }

            _target = module.Memory.PatrolPoints[_currentPoint++];
            base.Activate(module);
        }

        public override BaseAction Copy()
        {
            var action = new PatrolAction();
            Setup(action);

            return action;
        }
    }
}