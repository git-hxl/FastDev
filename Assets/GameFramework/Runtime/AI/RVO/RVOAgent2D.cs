using System;
using UnityEngine;
using RVO;
using Random = System.Random;
using Vector2 = RVO.Vector2;

namespace GameFramework
{
    public class RVOAgent2D : MonoBehaviour
    {
        public Vector2 TargetPos { get; private set; }

        public bool IsStop { get; private set; } = true;

        [HideInInspector] public int sid = -1;

        /** Random number generator. */
        private Random m_random = new Random();

        // Update is called once per frame
        void Update()
        {
            if (sid >= 0)
            {
                Vector2 pos = Simulator.Instance.getAgentPosition(sid);
                Vector2 vel = Simulator.Instance.getAgentPrefVelocity(sid);
                transform.position = new Vector3(pos.x(), pos.y(), transform.position.z);
                if (Math.Abs(vel.x()) > 0.01f && Math.Abs(vel.y()) > 0.01f)
                    transform.up = new Vector3(vel.x(), vel.y(), 0).normalized;
            }

            if (IsStop)
            {
                Simulator.Instance.setAgentPrefVelocity(sid, new Vector2(0, 0));
                return;
            }

            Vector2 goalVector = TargetPos - Simulator.Instance.getAgentPosition(sid);

            if (RVOMath.absSq(goalVector) > 1.0f)
            {
                goalVector = RVOMath.normalize(goalVector);
            }

            Simulator.Instance.setAgentPrefVelocity(sid, goalVector);

            /* Perturb a little to avoid deadlocks due to perfect symmetry. */
            float angle = (float)m_random.NextDouble() * 2.0f * (float)Math.PI;
            float dist = (float)m_random.NextDouble() * 0.0001f;

            Simulator.Instance.setAgentPrefVelocity(sid, Simulator.Instance.getAgentPrefVelocity(sid) +
                                                         dist *
                                                         new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
        }

        public void SetTargetPos(Vector3 pos)
        {
            TargetPos = new Vector2(pos.x, pos.y);
            IsStop = false;
        }

        public void Stop()
        {
            IsStop = true;
        }
    }
}
