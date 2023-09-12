using RVO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Vector2 = RVO.Vector2;

namespace GameFramework
{
    public class RVOManager : MonoSingleton<RVOManager>
    {
        public GameObject TestAgent;

        private Dictionary<int, RVOAgent> m_agentMap = new Dictionary<int, RVOAgent>();
        // Start is called before the first frame update
        void Start()
        {
            Simulator.Instance.setTimeStep(0.25f);
            Simulator.Instance.setAgentDefaults(1.5f, 1000, 5.0f, 5.0f, 0.5f, 0.1f, new Vector2(0.0f, 0.0f));

            // add in awake
            Simulator.Instance.processObstacles();
        }


        void CreatAgent(RVO.Vector2 pos)
        {
            int sid = Simulator.Instance.addAgent(pos);
            if (sid >= 0)
            {
                GameObject go = Instantiate(TestAgent, new Vector3(pos.x_, pos.y_, 0), Quaternion.identity);
                RVOAgent ga = go.GetComponent<RVOAgent>();
                Assert.IsNotNull(ga);
                ga.sid = sid;
                m_agentMap.Add(sid, ga);
            }
        }

        void DeleteAgent(RVO.Vector2 pos)
        {
            int agentNo = Simulator.Instance.queryNearAgent(pos, 1.5f);
            if (agentNo == -1 || !m_agentMap.ContainsKey(agentNo))
                return;

            Simulator.Instance.delAgent(agentNo);

            Destroy(m_agentMap[agentNo].gameObject);

            m_agentMap.Remove(agentNo);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                foreach (var item in m_agentMap)
                {
                    item.Value.SetTargetPos(worldpos);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                CreatAgent(new Vector2(worldpos.x, worldpos.y));
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                DeleteAgent(new Vector2(worldpos.x, worldpos.y));
            }

            Simulator.Instance.doStep();
        }

    }
}
