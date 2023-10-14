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
        /// <summary>
        /// °ë¾¶
        /// </summary>
        public float Radius = 0.5f;
        /// <summary>
        /// ËÙ¶È
        /// </summary>
        public float Speed = 0.1f;

        public int MaxAgents = 10000;

        public bool Is3D;

        private Dictionary<int, RVOAgent2D> m_agentMap = new Dictionary<int, RVOAgent2D>();
        private Dictionary<int, RVOAgent3D> m_agentMap3D = new Dictionary<int, RVOAgent3D>();
        // Start is called before the first frame update
        void Start()
        {
            Simulator.Instance.setTimeStep(0.25f);
            Simulator.Instance.setAgentDefaults(Radius * 2 + 0.1f, 10000,5f, 5f, Radius, Speed, new Vector2(0.0f, 0.0f));

            // add in awake
            Simulator.Instance.processObstacles();
        }


        public void CreatAgent2D(Vector2 pos)
        {
            int sid = Simulator.Instance.addAgent(pos);
            if (sid >= 0)
            {
                GameObject go = Instantiate(TestAgent, new Vector3(pos.x_, pos.y_, 0), Quaternion.identity);
                RVOAgent2D ga = go.GetComponent<RVOAgent2D>();
                Assert.IsNotNull(ga);
                ga.sid = sid;
                m_agentMap.Add(sid, ga);
            }
        }

        public void CreatAgent3D(Vector3 pos)
        {
            Vector2 rvoPos = new Vector2(pos.x, pos.z);
            int sid = Simulator.Instance.addAgent(rvoPos);
            if (sid >= 0)
            {
                pos.y = 0;

                GameObject go = Instantiate(TestAgent, pos, Quaternion.identity);
                RVOAgent3D ga = go.GetComponent<RVOAgent3D>();
                Assert.IsNotNull(ga);
                ga.sid = sid;
                m_agentMap3D.Add(sid, ga);
            }
        }

        public void DeleteAgent(Vector2 pos)
        {
            int agentNo = Simulator.Instance.queryNearAgent(pos, 1.5f);
            if (agentNo == -1 || !m_agentMap.ContainsKey(agentNo))
                return;

            Simulator.Instance.delAgent(agentNo);

            Destroy(m_agentMap[agentNo].gameObject);

            m_agentMap.Remove(agentNo);
        }

        public void DeleteAgent3D(Vector3 pos)
        {
            Vector2 pos2D = new Vector2(pos.x, pos.z);
            int agentNo = Simulator.Instance.queryNearAgent(pos2D, 1.5f);
            if (agentNo == -1 || !m_agentMap3D.ContainsKey(agentNo))
                return;

            Simulator.Instance.delAgent(agentNo);

            Destroy(m_agentMap3D[agentNo].gameObject);

            m_agentMap3D.Remove(agentNo);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Is3D)
                {
                    Vector3 inputPos = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(inputPos);
                    RaycastHit raycastHit;
                    Vector3 worldpos = Vector3.zero;
                    if (Physics.Raycast(ray, out raycastHit, 100))
                    {
                        worldpos = raycastHit.point;
                    }

                    foreach (var item in m_agentMap3D)
                    {
                        item.Value.SetTargetPos(worldpos);
                    }
                }
                else
                {
                    Vector3 inputPos = Input.mousePosition;
                    Vector3 worldpos = Camera.main.ScreenToWorldPoint(inputPos);
                    foreach (var item in m_agentMap)
                    {
                        item.Value.SetTargetPos(worldpos);
                    }
                }


            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (Is3D)
                {
                    Vector3 inputPos = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(inputPos);
                    RaycastHit raycastHit;
                    Vector3 worldpos = Vector3.zero;
                    if (Physics.Raycast(ray, out raycastHit, 100))
                    {
                        worldpos = raycastHit.point;
                    }

                    CreatAgent3D(worldpos);
                }
                else
                {
                    Vector3 inputPos = Input.mousePosition;
                    Vector3 worldpos = Camera.main.ScreenToWorldPoint(inputPos);
                    CreatAgent2D(new Vector2(worldpos.x, worldpos.y));
                }

            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if (Is3D)
                {
                    Vector3 inputPos = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(inputPos);
                    RaycastHit raycastHit;
                    Vector3 worldpos = Vector3.zero;
                    if (Physics.Raycast(ray, out raycastHit, 100))
                    {
                        worldpos = raycastHit.point;
                    }

                    DeleteAgent3D(worldpos);
                }
                else
                {

                    Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    DeleteAgent(new Vector2(worldpos.x, worldpos.y));
                }

            }

            Simulator.Instance.doStep();
        }

    }
}
