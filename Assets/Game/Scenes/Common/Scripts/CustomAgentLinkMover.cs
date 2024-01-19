using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Game.Scenes.Common.Scripts
{
    /// <summary>
    /// Move an agent when traversing a OffMeshLink given specific animated methods
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class CustomAgentLinkMover : MonoBehaviour
    {
        public OffMeshLinkMoveMethod m_Method = OffMeshLinkMoveMethod.Parabola;
        public AnimationCurve curve;
        
        [SerializeField] private float parabolaHeight = 2.0f;
        [SerializeField] private float parabolaDuration = .5f;

        public delegate void LinkEvent();

        public LinkEvent OnLinkStart;
        public LinkEvent OnLinkEnd;

        IEnumerator Start()
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.autoTraverseOffMeshLink = false;
            while (true)
            {
                if (agent.isOnOffMeshLink)
                {
                    OnLinkStart?.Invoke();
                    if (m_Method == OffMeshLinkMoveMethod.NormalSpeed)
                        yield return StartCoroutine(NormalSpeed(agent));
                    else if (m_Method == OffMeshLinkMoveMethod.Parabola)
                        yield return StartCoroutine(Parabola(agent, parabolaHeight, parabolaDuration));
                    else if (m_Method == OffMeshLinkMoveMethod.Curve)
                        yield return StartCoroutine(Curve(agent, parabolaDuration));
                    agent.CompleteOffMeshLink();
                    OnLinkEnd?.Invoke();
                }

                yield return null;
            }
        }

        IEnumerator NormalSpeed(NavMeshAgent agent)
        {
            OffMeshLinkData data = agent.currentOffMeshLinkData;
            Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
            while (agent.transform.position != endPos)
            {
                agent.transform.position =
                    Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
                yield return null;
            }
        }

        IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
        {
            OffMeshLinkData data = agent.currentOffMeshLinkData;
            Vector3 startPos = agent.transform.position;
            Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
            float normalizedTime = 0.0f;
            while (normalizedTime < 1.0f)
            {
                float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }
        }

        IEnumerator Curve(NavMeshAgent agent, float duration)
        {
            OffMeshLinkData data = agent.currentOffMeshLinkData;
            
            Vector3 startPos = agent.transform.position;
            Vector3 endPos = data.endPos + (Vector3.up * agent.baseOffset);
            
            float normalizedTime = 0.0f;
            while (normalizedTime < 1.0f)
            {
                float yOffset = curve.Evaluate(normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }
        }
        
        public enum OffMeshLinkMoveMethod
        {
            Teleport,
            NormalSpeed,
            Parabola,
            Curve
        }
    }
}